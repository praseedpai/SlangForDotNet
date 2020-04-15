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



        public RDParser(String str)
            : base(str)
        {


        }

        


        /// <summary>
        ///    <Expr>  ::=  <Term> | <Term> { + | - } <Expr>
        ///    
        /// </summary>
        /// <returns></returns>
        public Exp Expr(COMPILATION_CONTEXT ctx)
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
        public Exp Term(COMPILATION_CONTEXT ctx)
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
        public Exp Factor(COMPILATION_CONTEXT ctx)
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

                RetValue = Expr(ctx);  // Recurse

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
        public ArrayList Parse(COMPILATION_CONTEXT ctx)
        {
            GetNext();  // Get the Next Token
            //
            // Parse all the statements
            //
            return StatementList(ctx);
        }
        /// <summary>
        ///  The Grammar is 
        ///  
        ///  <stmtlist> :=  { <statement> }+
        ///
        ///  {<statement> :=  <printstmt> | <printlinestmt>
        ///  <printstmt> :=   print   <expr >;
        ///  <vardeclstmt> := STRING <varname>;  |
        ///                   NUMERIC <varname>; |
        ///                   BOOLEAN <varname>; 
        ///
        /// <printlinestmt>:= printline <expr>;
        ///    
        /// <Expr>  ::=  <Term> | <Term> { + | - } <Expr>
        /// <Term> ::=  <Factor> | <Factor>  {*|/} <Term>
        /// <Factor>::=  <number> | ( <expr> ) | {+|-} <factor>
        ///              <variable> | TRUE | FALSE
        ///              
        ///       
        /// 
        /// </summary>
        /// <returns></returns>
        private ArrayList StatementList(COMPILATION_CONTEXT ctx)
        {
            ArrayList arr = new ArrayList();
            while (Current_Token != TOKEN.TOK_NULL)
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
        private Stmt Statement(COMPILATION_CONTEXT ctx)
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
        private Stmt ParsePrintStatement(COMPILATION_CONTEXT ctx)
        {
            GetNext();
            Exp a = Expr(ctx);

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
        private Stmt ParsePrintLNStatement(COMPILATION_CONTEXT ctx)
        {
            GetNext();
            Exp a = Expr(ctx);

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

        public Stmt ParseVariableDeclStatement(COMPILATION_CONTEXT ctx)
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
        public Stmt ParseAssignmentStatement(COMPILATION_CONTEXT ctx)
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
            Exp exp = Expr(ctx);

            //------------ Do the type analysis ...

            if (exp.TypeCheck(ctx) != s.Type)
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


    }
}

