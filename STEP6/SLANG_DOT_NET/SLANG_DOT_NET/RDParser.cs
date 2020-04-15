////////////////////////////////////////////////////////
//
//  This software is released as per the clauses of MIT License
//
// 
//  The MIT License
//
//  Copyright (c) 2010, Praseed Pai K.T.
//                      http://praseedp.blogspot.com
//                      praseedp@yahoo.com  
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SLANG_DOT_NET
{
    /// <summary>
    /// 
    /// </summary>
    public class RDParser : Lexer
    {

        /// <summary>
        ///    The Final outcome of the parser is a group of 
        ///    functions.
        /// </summary>
        TModuleBuilder prog = null;
        /// <summary>
        ///    
        /// </summary>
        /// <param name="str"></param>

        public RDParser(String str)
            : base(str)
        {
            prog = new TModuleBuilder(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Exp BExpr(ProcedureBuilder pb)
        {
            TOKEN l_token;
            Exp RetValue = LExpr(pb);
            while (Current_Token == TOKEN.TOK_AND || Current_Token == TOKEN.TOK_OR)
            {
                l_token = Current_Token;
                Current_Token = GetNext();
                Exp e2 = LExpr(pb);
                RetValue = new LogicalExp(l_token, RetValue, e2);

            }
            return RetValue;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public Exp LExpr(ProcedureBuilder pb)
        {
            TOKEN l_token;
            Exp RetValue = Expr(pb);
            while (Current_Token == TOKEN.TOK_GT ||
                    Current_Token == TOKEN.TOK_LT ||
                    Current_Token == TOKEN.TOK_GTE ||
                    Current_Token == TOKEN.TOK_LTE ||
                    Current_Token == TOKEN.TOK_NEQ ||
                    Current_Token == TOKEN.TOK_EQ)
            {
                l_token = Current_Token;
                Current_Token = GetNext();
                Exp e2 = Expr(pb);
                RELATION_OPERATOR relop = GetRelOp(l_token);
                RetValue = new RelationExp(relop, RetValue, e2);


            }
            return RetValue;

        }

        /// <summary>
        ///    <Expr>  ::=  <Term> | <Term> { + | - } <Expr>
        ///    
        /// </summary>
        /// <returns></returns>
        public Exp Expr(ProcedureBuilder ctx)
        {
            TOKEN l_token;
            Exp RetValue = Term(ctx);
            while (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
            {
                l_token = Current_Token;
                Current_Token = GetToken();
                Exp e1 = Expr(ctx);

                if (l_token == TOKEN.TOK_PLUS)
                    RetValue = new BinaryPlus(RetValue, e1);
                else
                    RetValue = new BinaryMinus(RetValue, e1);
            }

            return RetValue;

        }
        /// <summary>
        /// <Term> ::=  <Factor> | <Factor>  {*|/} <Term>
        /// </summary>
        public Exp Term(ProcedureBuilder ctx)
        {
            TOKEN l_token;
            Exp RetValue = Factor(ctx);

            while (Current_Token == TOKEN.TOK_MUL || Current_Token == TOKEN.TOK_DIV)
            {
                l_token = Current_Token;
                Current_Token = GetToken();


                Exp e1 = Term(ctx);
                if (l_token == TOKEN.TOK_MUL)
                    RetValue = new Mul(RetValue, e1);
                else
                    RetValue = new Div(RetValue, e1);

            }

            return RetValue;
        }

        /// <summary>
        ///     <Factor>::=  <number> | ( <expr> ) | {+|-} <factor>
        ///           <variable> | TRUE | FALSE
        /// </summary>
        public Exp Factor(ProcedureBuilder ctx)
        {
            TOKEN l_token;
            Exp RetValue = null;



            if (Current_Token == TOKEN.TOK_NUMERIC)
            {

                RetValue = new NumericConstant(GetNumber());
                Current_Token = GetToken();

            }
            else if (Current_Token == TOKEN.TOK_STRING)
            {
                RetValue = new StringLiteral(last_str);
                Current_Token = GetToken();
            }
            else if (Current_Token == TOKEN.TOK_BOOL_FALSE ||
                      Current_Token == TOKEN.TOK_BOOL_TRUE)
            {
                RetValue = new BooleanConstant(
                    Current_Token == TOKEN.TOK_BOOL_TRUE ? true : false);
                Current_Token = GetToken();
            }
            else if (Current_Token == TOKEN.TOK_OPAREN)
            {

                Current_Token = GetToken();

                RetValue = BExpr(ctx);  // Recurse

                if (Current_Token != TOKEN.TOK_CPAREN)
                {
                    Console.WriteLine("Missing Closing Parenthesis\n");
                    throw new Exception();

                }
                Current_Token = GetToken();
            }

            else if (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
            {
                l_token = Current_Token;
                Current_Token = GetToken();
                RetValue = Factor(ctx);
                if (l_token == TOKEN.TOK_PLUS)
                    RetValue = new UnaryPlus(RetValue);
                else
                    RetValue = new UnaryMinus(RetValue);
            
            }
            else if (Current_Token == TOKEN.TOK_NOT)
            {
                l_token = Current_Token;
                Current_Token = GetToken();
                RetValue = Factor(ctx);

                RetValue = new LogicalNot(RetValue);
            }
            else if (Current_Token == TOKEN.TOK_UNQUOTED_STRING)
            {
                ///
                ///  Variables 
                ///
                String str = base.last_str;
                SYMBOL_INFO inf = ctx.TABLE.Get(str);

                if (inf == null)
                    throw new Exception("Undefined symbol");

                GetNext();
                RetValue = new Variable(inf);
            }





            else
            {

                Console.WriteLine("Illegal Token");
                throw new Exception();
            }


            return RetValue;

        }

        /// <summary>
        ///   The new Parser entry point
        /// </summary>
        /// <returns></returns>
        public ArrayList Parse(ProcedureBuilder ctx)
        {
            GetNext();  // Get the Next Token
            //
            // Parse all the statements
            //
            return StatementList(ctx);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TModule DoParse()
        {
            try
            {
                ProcedureBuilder p = new ProcedureBuilder("MAIN", new COMPILATION_CONTEXT());
                ArrayList stmts = Parse(p);

                foreach (Stmt s in stmts)
                {
                    p.AddStatement(s);
                }

                Procedure pc = p.GetProcedure();

                prog.Add(pc);
                return prog.GetProgram();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

        /// <summary>
        ///  The Grammar is 
        ///  
        ///  <stmts> :=  { stmt }+
        ///  {stmt}  :=  <vardeclstmt> | 
        ///              <printstmt>|<assignmentstmt>|
        ///              <ifstmt>| <whilestmt> |
        ///              <printlinestmt>
        ///
        ///   <vardeclstmt> ::=  <type>  var_name;
        ///   <printstmt> := PRINT <expr>;
        ///   <assignmentstmt>:= <variable> = value;
        ///   <ifstmt>::= IF  <expr> THEN <stmts> [ ELSE  <stmts> ] ENDIF
        ///   <whilestmt>::=  WHILE  <expr> <stmts> WEND
        ///    <type> := NUMERIC | STRING | BOOLEAN
        ///    
        ///    <expr> ::=  <BExpr>
        ///    <BExpr> ::= <LExpr> LOGIC_OP <BExpr>
        ///    <LExpr> ::= <RExpr> REL_OP   <LExpr>
        ///    <RExpr> ::= <Term> ADD_OP <RExpr>
        ///    <Term>::= <Factor>  MUL_OP <Term>
        ///    <Factor>  ::= <Numeric>  |  
        ///                  <String> | 
        ///                  TRUE | 
        ///                  FALSE | 
        ///                  <variable> | 
        ///                  ‘(‘ <expr> ‘)’ |
        ///                  {+|-|!} <Factor>  
        ///     
        ///     <LOGIC_OP> := '&&'  | ‘||’
        ///     <REL_OP> := '>' |' < '|' >=' |' <=' |' <>' |' =='
        ///     <MUL_OP> :=  '*' |' /'
        ///     <ADD_OP>  :=  '+' |' -'
        /// </summary>
        /// <returns></returns>
        private ArrayList StatementList(ProcedureBuilder ctx)
        {
            ArrayList arr = new ArrayList();
            while (
                    (Current_Token != TOKEN.TOK_ELSE) &&
                    (Current_Token != TOKEN.TOK_ENDIF) &&
                    (Current_Token != TOKEN.TOK_WEND) &&
                    (Current_Token != TOKEN.TOK_NULL )
              )
            {
                Stmt temp = Statement(ctx);
                if (temp != null)
                {
                    arr.Add(temp);
                }
            }
            return arr;
        }

        /// <summary>
        ///    This Routine Queries Statement Type 
        ///    to take the appropriate Branch...
        ///    Currently , only Print and PrintLine statement
        ///    are supported..
        ///    if a line does not start with Print or PrintLine ..
        ///    an exception is thrown
        /// </summary>
        /// <returns></returns>
        private Stmt Statement(ProcedureBuilder ctx)
        {
            Stmt retval = null;
            switch (Current_Token)
            {
                case TOKEN.TOK_VAR_STRING:
                case TOKEN.TOK_VAR_NUMBER:
                case TOKEN.TOK_VAR_BOOL:
                    retval = ParseVariableDeclStatement(ctx);
                    GetNext();
                    return retval;
                case TOKEN.TOK_PRINT:
                    retval = ParsePrintStatement(ctx);
                    GetNext();
                    break;
                case TOKEN.TOK_PRINTLN:
                    retval = ParsePrintLNStatement(ctx);
                    GetNext();
                    break;
                case TOKEN.TOK_UNQUOTED_STRING:
                    retval = ParseAssignmentStatement(ctx);
                    GetNext();
                    return retval;
                case TOKEN.TOK_IF:
                    retval = ParseIfStatement(ctx);
                    GetNext();
                    return retval;

                case TOKEN.TOK_WHILE:
                    retval = ParseWhileStatement(ctx);
                    GetNext();
                    return retval;   
  
                default:
                    throw new Exception("Invalid statement");

            }
            return retval;
        }
        /// <summary>
        ///    Parse the Print Staement .. The grammar is 
        ///    PRINT <expr> ;
        ///    Once you are in this subroutine , we are expecting 
        ///    a valid expression ( which will be compiled ) and a
        ///    semi collon to terminate the line..
        ///    Once Parse Process is successful , we create a PrintStatement
        ///    Object..
        /// </summary>
        /// <returns></returns>
        private Stmt ParsePrintStatement(ProcedureBuilder ctx)
        {
            GetNext();
            Exp a = BExpr(ctx);
            //
            // Do the type analysis ...
            //
            a.TypeCheck(ctx.Context); 

            if (Current_Token != TOKEN.TOK_SEMI)
            {
                throw new Exception("; is expected");
            }
            return new PrintStatement(a);
        }
        /// <summary>
        ///    Parse the PrintLine Staement .. The grammar is 
        ///    PRINTLINE <expr> ;
        ///    Once you are in this subroutine , we are expecting 
        ///    a valid expression ( which will be compiled ) and a
        ///    semi collon to terminate the line..
        ///    Once Parse Process is successful , we create a PrintLineStatement
        ///    Object..
        /// </summary>
        /// <returns></returns>
        private Stmt ParsePrintLNStatement(ProcedureBuilder ctx)
        {
            GetNext();
            Exp a = Expr(ctx);
            a.TypeCheck(ctx.Context); 
            if (Current_Token != TOKEN.TOK_SEMI)
            {
                throw new Exception("; is expected");
            }
            return new PrintLineStatement(a);
        }

        /// <summary>
        ///    Parse Variable declaration statement
        /// </summary>
        /// <param name="type"></param>

        public Stmt ParseVariableDeclStatement(ProcedureBuilder ctx)
        {
            
            //--- Save the Data type 
            TOKEN tok = Current_Token;

            // --- Skip to the next token , the token ought 
            // to be a Variable name ( UnQouted String )
            GetNext();

            if (Current_Token == TOKEN.TOK_UNQUOTED_STRING)
            {
                SYMBOL_INFO symb = new SYMBOL_INFO();
                symb.SymbolName = base.last_str;
                symb.Type = (tok == TOKEN.TOK_VAR_BOOL) ?
                TYPE_INFO.TYPE_BOOL : (tok == TOKEN.TOK_VAR_NUMBER) ?
                TYPE_INFO.TYPE_NUMERIC : TYPE_INFO.TYPE_STRING;

                //---------- Skip to Expect the SemiColon
                
                GetNext();



                if (Current_Token == TOKEN.TOK_SEMI)
                {
                    // ----------- Add to the Symbol Table
                    // for type analysis 
                    ctx.TABLE.Add(symb); 

                    // --------- return the Object of type
                    // --------- VariableDeclStatement
                    // This will just store the Variable name
                    // to be looked up in the above table
                    return new VariableDeclStatement(symb);
                }
                else
                {
                    CSyntaxErrorLog.AddLine("; expected");
                    CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                    throw new CParserException(-100, ", or ; expected", SaveIndex());
                }
            }
            else
            {

                CSyntaxErrorLog.AddLine("invalid variable declaration");
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, ", or ; expected", SaveIndex());
            }








        }
        /// <summary>
        ///    Parse the Assignment Statement 
        ///    <variable> = <expr>
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public Stmt ParseAssignmentStatement(ProcedureBuilder ctx)
        {

            //
            // Retrieve the variable and look it up in 
            // the symbol table ..if not found throw exception
            //
            string variable = base.last_str;
            SYMBOL_INFO s = ctx.TABLE.Get(variable);
            if (s == null)
            {
                CSyntaxErrorLog.AddLine("Variable not found  " + last_str);
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, "Variable not found", SaveIndex());

            }

            //------------ The next token ought to be an assignment
            // expression....

            GetNext();

            if (Current_Token != TOKEN.TOK_ASSIGN)
            {

                CSyntaxErrorLog.AddLine("= expected");
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, "= expected", SaveIndex());

            }

            //-------- Skip the token to start the expression
            // parsing on the RHS
            GetNext();
            Exp exp = BExpr(ctx);

            //------------ Do the type analysis ...

            if (exp.TypeCheck(ctx.Context) != s.Type)
            {
                throw new Exception("Type mismatch in assignment");

            }

            // -------------- End of statement ( ; ) is expected
            if (Current_Token != TOKEN.TOK_SEMI)
            {
                CSyntaxErrorLog.AddLine("; expected");
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, " ; expected", -1);

            }
            // return an instance of AssignmentStatement node..
            //   s => Symbol info associated with variable
            //   exp => to evaluated and assigned to symbol_info
            return new AssignmentStatement(s, exp);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public Stmt ParseIfStatement(ProcedureBuilder pb)
        {
            GetNext();
            ArrayList true_part = null;
            ArrayList false_part = null;
            Exp exp = BExpr(pb);  // Evaluate Expression


            if (pb.TypeCheck(exp) != TYPE_INFO.TYPE_BOOL)
            {
                throw new Exception("Expects a boolean expression");

            }


            if (Current_Token != TOKEN.TOK_THEN)
            {
                CSyntaxErrorLog.AddLine(" Then Expected");
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, "Then Expected", SaveIndex());

            }

            GetNext();

            true_part = StatementList(pb);

            if (Current_Token == TOKEN.TOK_ENDIF)
            {
                return new IfStatement(exp, true_part, false_part);
            }


            if (Current_Token != TOKEN.TOK_ELSE)
            {

                throw new Exception("ELSE expected");
            }

            GetNext();
            false_part = StatementList(pb);

            if (Current_Token != TOKEN.TOK_ENDIF)
            {
                throw new Exception("END IF EXPECTED");

            }

            return new IfStatement(exp, true_part, false_part);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public Stmt ParseWhileStatement(ProcedureBuilder pb)
        {

            GetNext();

            Exp exp = BExpr(pb);
            if (pb.TypeCheck(exp) != TYPE_INFO.TYPE_BOOL)
            {
                throw new Exception("Expects a boolean expression");

            }

            ArrayList body = StatementList(pb);
            if ((Current_Token != TOKEN.TOK_WEND))
            {
                CSyntaxErrorLog.AddLine("Wend Expected");
                CSyntaxErrorLog.AddLine(GetCurrentLine(SaveIndex()));
                throw new CParserException(-100, "Wend Expected", SaveIndex());

            }


            return new WhileStatement(exp, body);

        }
        /// <summary>
        ///    Convert a token to Relational Operator
        /// </summary>
        /// <param name="tok"></param>
        /// <returns></returns>
        private RELATION_OPERATOR GetRelOp(TOKEN tok)
        {
            if (tok == TOKEN.TOK_EQ)
                return RELATION_OPERATOR.TOK_EQ;
            else if (tok == TOKEN.TOK_NEQ)
                return RELATION_OPERATOR.TOK_NEQ;
            else if (tok == TOKEN.TOK_GT)
                return RELATION_OPERATOR.TOK_GT;
            else if (tok == TOKEN.TOK_GTE)
                return RELATION_OPERATOR.TOK_GTE;
            else if (tok == TOKEN.TOK_LT)
                return RELATION_OPERATOR.TOK_LT;
            else
                return RELATION_OPERATOR.TOK_LTE;


        }


    }
}

