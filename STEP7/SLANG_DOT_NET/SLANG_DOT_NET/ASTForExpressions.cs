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
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;


namespace SLANG_DOT_NET
{
   
   
    /// <summary>
    ///     In  this Step , we add two more methods to the Exp class
    ///     TypeCheck => To do Type analysis
    ///     get_type  => Type of this node
    /// </summary>
    public abstract class Exp
    {
       public abstract SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont);
       public abstract TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont);
       public abstract TYPE_INFO get_type();
       /// <summary>
       ///   Added in the STEP 5 for .NET IL code generation
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont); 
    }
   /// <summary>
   ///    Node for Boolean Constant ( TRUE, FALSE }
   ///    Value
   /// </summary>
   public class BooleanConstant : Exp
   {
       /// <summary>
       ///     Info Field
       /// </summary>
       private SYMBOL_INFO info;
       /// <summary>
       ///    Ctor
       /// </summary>
       /// <param name="pvalue"></param>

       public BooleanConstant(bool pvalue)
       {
           info = new SYMBOL_INFO();
           info.SymbolName = null;
           info.bol_val = pvalue;
           info.Type = TYPE_INFO.TYPE_BOOL;
       }
       /// <summary>
       ///    Evaluation of boolean will given the value
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           return info;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           return info.Type;
       }
      
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return info.Type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           // Retrieve the IL Code generator and Emit 
           //    LDC_I4 => Load Constant Integer 4
           // We are planning to use a 32 bit long for Boolean 
           // True or False
           cont.CodeOutput.Emit(OpCodes.Ldc_I4, (info.bol_val) ? 1 : 0);
           return true;
       }

   }
   /// <summary>
   ///     
   /// </summary>
   public class NumericConstant : Exp
   {
       /// <summary>
       ///    Info field
       /// </summary>
       private SYMBOL_INFO info;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="pvalue"></param>
       public NumericConstant(double pvalue)
       {
           info = new SYMBOL_INFO();
           info.SymbolName = null;
           info.dbl_val = pvalue;
           info.Type = TYPE_INFO.TYPE_NUMERIC;

       }

       /// <summary>
       ///    Evaluation of boolean will given the value
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           return info;
       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           return info.Type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return info.Type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           // Emit LDC_R8 => Load Constant Real 8
           // IEEE 754 floating Point
           // 
           // cont.CodeOutput will return ILGenerator of the 
           // current method...
           cont.CodeOutput.Emit(OpCodes.Ldc_R8, info.dbl_val);
           return true;
       }

   }


   /// <summary>
   ///   To Store Literal string enclosed   
   ///   in Quotes  
   /// </summary>
  public class StringLiteral : Exp
   {
       /// <summary>
       ///  info field
       /// </summary>
       private SYMBOL_INFO info;
       /// <summary>
       ///   Ctor
       /// </summary>
       /// <param name="pvalue"></param>
       public StringLiteral(string pvalue)
       {
           info = new SYMBOL_INFO();
           info.SymbolName = null;
           info.str_val = pvalue;
           info.Type = TYPE_INFO.TYPE_STRING;
       }

       /// <summary>
       ///    Evaluation of boolean will given the value
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           return info;
       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           return info.Type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return info.Type;
       }
       /// <summary>
       ///    
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           //
           // For string emit 
           //     LDSTR => Load String 
           //
           cont.CodeOutput.Emit(OpCodes.Ldstr , info.str_val);
           return true;

       }
       
   }

   /// <summary>
   ///    Node to store Variables
   ///    The data types supported are
   ///      NUMERIC
   ///      STRING
   ///      BOOLEAN
   ///    The node store only the variable name , the 
   ///    associated data will be found in the 
   ///    Symbol Table attached to the 
   ///      COMPILATION_CONTEXT 
   ///    
   /// </summary>

   public class Variable : Exp
   {
       private string m_name;  // Var name
       TYPE_INFO _type;        // Type 
       /// <summary>
       ///     this Ctor just stores the variable name
       /// </summary>
       /// <param name="inf"></param>
       public Variable(SYMBOL_INFO inf)
       {
           m_name = inf.SymbolName;

       }
       /// <summary>
       ///     Creates a new symbol and puts into the symbol table
       ///     and stores the key ( variable name ) 
       /// </summary>
       /// <param name="st"></param>
       /// <param name="name"></param>
       /// <param name="_value"></param>
       public Variable(COMPILATION_CONTEXT st, String name, double _value)
       {
           SYMBOL_INFO s = new SYMBOL_INFO();
           s.SymbolName = name;
           s.Type = TYPE_INFO.TYPE_NUMERIC;
           s.dbl_val = _value;
           st.TABLE.Add(s);
           m_name = name;
       }
       /// <summary>
       ///     Creates a new symbol and puts into the symbol table
       ///     and stores the key ( variable name ) 
       /// </summary>
       /// <param name="st"></param>
       /// <param name="name"></param>
       /// <param name="_value"></param>
       public Variable(COMPILATION_CONTEXT st, String name, bool _value)
       {
           SYMBOL_INFO s = new SYMBOL_INFO();
           s.SymbolName = name;
           s.Type = TYPE_INFO.TYPE_BOOL;
           s.bol_val = _value;
           st.TABLE.Add(s);
           m_name = name;
       }
       /// <summary>
       ///     Creates a new symbol and puts into the symbol table
       ///     and stores the key ( variable name ) 
       /// </summary>
       /// <param name="st"></param>
       /// <param name="name"></param>
       /// <param name="_value"></param>
       public Variable(COMPILATION_CONTEXT st, String name, string _value)
       {
           SYMBOL_INFO s = new SYMBOL_INFO();
           s.SymbolName = name;
           s.Type = TYPE_INFO.TYPE_STRING;
           s.str_val = _value;
           st.TABLE.Add(s);
           m_name = name;
       }

       /// <summary>
       ///    Retrieves the name of the Variable ( method version )
       /// </summary>
       /// <returns></returns>

       public string GetName()
       {
           return m_name;
       }

       /// <summary>
       ///   Retrieves the name of the Variable ( property version )
       /// </summary>
       /// <returns></returns>
       public string Name
       {
           get
           {
               return m_name;
           }

           set
           {
               m_name = value;
           }
       }

       /// <summary>
       ///    To Evaluate a variable , we just need to do a lookup
       ///    in the Symbol table ( of RUNTIME_CONTEXT ) 
       /// </summary>
       /// <param name="st"></param>
       /// <param name="glb"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {

           if (cont.TABLE == null)
           {
               return null;
           }
           else
           {
               SYMBOL_INFO a = cont.TABLE.Get(m_name);
               return a;
           }

       }

       /// <summary>
       ///     Look it up in the Symbol Table and 
       ///     return the type
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {

           if (cont.TABLE == null)
           {
               return TYPE_INFO.TYPE_ILLEGAL;
           }
           else
           {
               SYMBOL_INFO a = cont.TABLE.Get(m_name);
               if (a != null)
               {
                   _type = a.Type;
                   return _type;

               }


               return TYPE_INFO.TYPE_ILLEGAL;

           }

       }

       /// <summary>
       ///     this should only be called after the TypeCheck method
       ///     has been invoked on AST 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return _type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           // Retrieve the Symbol information from the 
           // Symbol Table. Symbol name is the key here..
           //
           SYMBOL_INFO info = cont.TABLE.Get(m_name);
           //
           // Give the Position to retrieve the Local Variable
           // Builder.
           //
           LocalBuilder lb = cont.GetLocal(info.loc_position);
           //
           // LDLOC => Load Local... we need to give
           // a Local Builder as parameter
           //
           cont.CodeOutput.Emit(OpCodes.Ldloc, lb);
           return true;
       }

   }

   /// <summary>
   ///    the node to represent Binary + 
   /// </summary>

   public class BinaryPlus : Exp
   {
       /// <summary>
       ///  Plus has got a left expression (exp1 )
       ///  and a right expression...
       ///  and a Associated type information
       /// </summary>
       private Exp exp1, exp2;
       TYPE_INFO _type;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="e1"></param>
       /// <param name="e2"></param>
       public BinaryPlus(Exp e1, Exp e2)
       {
           exp1 = e1; exp2 = e2;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>

       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           SYMBOL_INFO eval_right = exp2.Evaluate(cont);

           if (eval_left.Type == TYPE_INFO.TYPE_STRING &&
               eval_right.Type == TYPE_INFO.TYPE_STRING)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.str_val = eval_left.str_val + eval_right.str_val;
               ret_val.Type = TYPE_INFO.TYPE_STRING;
               ret_val.SymbolName = "";
               return ret_val;
           }
           else if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
               eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = eval_left.dbl_val + eval_right.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);
           TYPE_INFO eval_right = exp2.TypeCheck(cont);

           if (eval_left == eval_right && eval_left != TYPE_INFO.TYPE_BOOL)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }


    

       public override TYPE_INFO get_type()
       {
           return _type;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
          
           // Compile the Left Expression
           exp1.Compile(cont);
           //
           // Compile the Right Expression
           exp2.Compile(cont);
           //
           // Emit Add instruction
           //
           if (_type == TYPE_INFO.TYPE_NUMERIC)
           {
               cont.CodeOutput.Emit(OpCodes.Add);
           }
           else
           {
               // This is a string type..we need to call
               // Concat method..
                              
               Type[] str2 = {
                                 typeof(string),
                                 typeof(string)
                             };

               cont.CodeOutput.Emit(OpCodes.Call,
                   typeof(String).GetMethod("Concat", str2));

            }
           return true;
       }

   }

   /// <summary>
   /// 
   /// </summary>

   class BinaryMinus : Exp
   {
       private Exp exp1, exp2;
       TYPE_INFO _type;

       public BinaryMinus(Exp e1, Exp e2)
       {
           exp1 = e1; exp2 = e2;
       }


       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           SYMBOL_INFO eval_right = exp2.Evaluate(cont);

           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
               eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = eval_left.dbl_val - eval_right.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);
           TYPE_INFO eval_right = exp2.TypeCheck(cont);

           if (eval_left == eval_right && eval_left == TYPE_INFO.TYPE_NUMERIC)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }

       public override TYPE_INFO get_type()
       {
           return _type;
       }
       /// <summary>
       ///   Similar to Add
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           exp1.Compile(cont);
           exp2.Compile(cont);
           cont.CodeOutput.Emit(OpCodes.Sub);
           return true;
       }

       

   }

   ///
   /// <summary>
   /// 
   /// </summary>

   class Mul : Exp
   {
       private Exp exp1, exp2;
       TYPE_INFO _type;

       public Mul(Exp e1, Exp e2)
       {
           exp1 = e1; exp2 = e2;
       }




       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           SYMBOL_INFO eval_right = exp2.Evaluate(cont);

           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
               eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = eval_left.dbl_val * eval_right.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);
           TYPE_INFO eval_right = exp2.TypeCheck(cont);

           if (eval_left == eval_right && eval_left == TYPE_INFO.TYPE_NUMERIC)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }

       public override TYPE_INFO get_type()
       {
           return _type;
       }


       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           exp1.Compile(cont);
           exp2.Compile(cont);
           cont.CodeOutput.Emit(OpCodes.Mul);
           return true;
       }

   }
   /// <summary>
   /// 
   /// </summary>

   class Div : Exp
   {
       private Exp exp1, exp2;
       TYPE_INFO _type;

       public Div(Exp e1, Exp e2)
       {
           exp1 = e1; exp2 = e2;
       }


       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           SYMBOL_INFO eval_right = exp2.Evaluate(cont);

           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
               eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = eval_left.dbl_val / eval_right.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);
           TYPE_INFO eval_right = exp2.TypeCheck(cont);

           if (eval_left == eval_right && eval_left == TYPE_INFO.TYPE_NUMERIC)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }

       public override TYPE_INFO get_type()
       {
           return _type;
       }

       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           exp1.Compile(cont);
           exp2.Compile(cont);
           cont.CodeOutput.Emit(OpCodes.Div);
           return true;
       }

   }

   /// <summary>
   ///    the node to represent Unary + 
   /// </summary>

   class UnaryPlus : Exp
   {
       /// <summary>
       ///  Plus has got a right expression (exp1 )
       ///  and a Associated type information
       /// </summary>
       private Exp exp1;
       TYPE_INFO _type;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="e1"></param>
       /// <param name="e2"></param>
       public UnaryPlus(Exp e1)
       {
           exp1 = e1; 
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>

       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = eval_left.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);
         

           if (eval_left == TYPE_INFO.TYPE_NUMERIC)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }




       public override TYPE_INFO get_type()
       {
           return _type;
       }

       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           // Compile The Expression and do not do 
           // anything...else
           //
           exp1.Compile(cont);
           return true;
       }

   }

   /// <summary>
   ///    the node to represent Unary - 
   /// </summary>

   class UnaryMinus : Exp
   {
       /// <summary>
       ///  Plus has got a right expression (exp1 )
       ///  and a Associated type information
       /// </summary>
       private Exp exp1;
       TYPE_INFO _type;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="e1"></param>
       /// <param name="e2"></param>
       public UnaryMinus(Exp e1)
       {
           exp1 = e1; 
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>

       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = exp1.Evaluate(cont);
           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.dbl_val = -eval_left.dbl_val;
               ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
               ret_val.SymbolName = "";
               return ret_val;

           }
           else
           {
               throw new Exception("Type mismatch");
           }

       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="local"></param>
       /// <param name="global"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = exp1.TypeCheck(cont);


           if (eval_left == TYPE_INFO.TYPE_NUMERIC)
           {
               _type = eval_left;
               return _type;
           }
           else
           {
               throw new Exception("Type mismatch failure");

           }
       }




       public override TYPE_INFO get_type()
       {
           return _type;
       }

       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           // Compile the expression 
           exp1.Compile(cont);
           //
           // Negate the value on the top of the 
           // stack
           //
           cont.CodeOutput.Emit(OpCodes.Neg);
           return true;
       }

   }
   /// <summary>
   /// 
   /// </summary>
   public class RelationExp : Exp
   {
       /// <summary>
       ///   Which Operator
       /// </summary>
       RELATION_OPERATOR m_op;
       /// <summary>
       ///     Left and Right Expression
       /// </summary>
       private Exp ex1, ex2;
       /// <summary>
       ///   Type of this node
       /// </summary>
       TYPE_INFO _type;
       ///
       /// Operand Types .. if operands are string
       /// we need to generate call to String.Compare 
       /// method...
       /// 
       TYPE_INFO _optype; 
       /// <summary>
       /// 
       /// </summary>
       /// <param name="op"></param>
       /// <param name="e1"></param>
       /// <param name="e2"></param>
       public RelationExp(RELATION_OPERATOR op, Exp e1, Exp e2)
       {
           m_op = op;
           ex1 = e1;
           ex2 = e2;

       }
       /// <summary>
       ///    The logic of this method is obvious...
       ///    Evaluate the Left and Right Expression...
       ///    Query the Type of the expressions and perform
       ///    appropriate action
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = ex1.Evaluate(cont);
           SYMBOL_INFO eval_right = ex2.Evaluate(cont);

           SYMBOL_INFO ret_val = new SYMBOL_INFO();
           if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
               eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
           {

               ret_val.Type = TYPE_INFO.TYPE_BOOL;
               ret_val.SymbolName = "";

               if (m_op == RELATION_OPERATOR.TOK_EQ)
                   ret_val.bol_val = eval_left.dbl_val == eval_right.dbl_val;
               else if (m_op == RELATION_OPERATOR.TOK_NEQ)
                   ret_val.bol_val = eval_left.dbl_val != eval_right.dbl_val;
               else if (m_op == RELATION_OPERATOR.TOK_GT)
                   ret_val.bol_val = eval_left.dbl_val > eval_right.dbl_val;
               else if (m_op == RELATION_OPERATOR.TOK_GTE)
                   ret_val.bol_val = eval_left.dbl_val >= eval_right.dbl_val;
               else if (m_op == RELATION_OPERATOR.TOK_LTE)
                   ret_val.bol_val = eval_left.dbl_val <= eval_right.dbl_val;
               else if (m_op == RELATION_OPERATOR.TOK_LT)
                   ret_val.bol_val = eval_left.dbl_val < eval_right.dbl_val;


               return ret_val;

           }
           else if (eval_left.Type == TYPE_INFO.TYPE_STRING &&
               eval_right.Type == TYPE_INFO.TYPE_STRING)
           {

               ret_val.Type = TYPE_INFO.TYPE_BOOL;
               ret_val.SymbolName = "";

               if (m_op == RELATION_OPERATOR.TOK_EQ)
               {
                   ret_val.bol_val = ( String.Compare(
                          eval_left.str_val,
                          eval_right.str_val) == 0 ) ? true:false;

               }
               else if (m_op == RELATION_OPERATOR.TOK_NEQ)
               {
                   ret_val.bol_val = String.Compare(
                         eval_left.str_val,
                         eval_right.str_val) != 0;
                   
               }
               else
               {
                   ret_val.bol_val = false;

               }


               return ret_val;

           }
           if (eval_left.Type == TYPE_INFO.TYPE_BOOL &&
               eval_right.Type == TYPE_INFO.TYPE_BOOL)
           {

               ret_val.Type = TYPE_INFO.TYPE_BOOL;
               ret_val.SymbolName = "";

               if (m_op == RELATION_OPERATOR.TOK_EQ)
                   ret_val.bol_val = eval_left.bol_val == eval_right.bol_val;
               else if (m_op == RELATION_OPERATOR.TOK_NEQ)
                   ret_val.bol_val = eval_left.bol_val != eval_right.bol_val;
               else
               {
                   ret_val.bol_val = false;

               }
               return ret_val;

           }
           return null;
       }
       /// <summary>
       ///     Recursively check the type and bubble up the type
       ///     information to the top...
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = ex1.TypeCheck(cont);
           TYPE_INFO eval_right = ex2.TypeCheck(cont);

           if (eval_left != eval_right)
           {
               throw new Exception("Wrong Type in expression");
           }

           if (eval_left == TYPE_INFO.TYPE_STRING &&
                (!(m_op == RELATION_OPERATOR.TOK_EQ ||
                  m_op == RELATION_OPERATOR.TOK_NEQ)))
           {
               throw new Exception("Only == amd != supported for string type ");
           }

            if (eval_left == TYPE_INFO.TYPE_BOOL &&
                (!(m_op == RELATION_OPERATOR.TOK_EQ ||
                  m_op == RELATION_OPERATOR.TOK_NEQ)))
           {
               throw new Exception("Only == amd != supported for boolean type ");
           }
           // store the operand type as well
            _optype = eval_left; 
           _type = TYPE_INFO.TYPE_BOOL;
           return _type;
       
       

       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       private bool CompileStringRelOp(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           //
           // Compile the Left Expression
           ex1.Compile(cont);
           //
           // Compile the Right Expression
           ex2.Compile(cont);

           // This is a string type..we need to call
           // Compare method..

           Type[] str2 = {
                                 typeof(string),
                                 typeof(string)
                      };
           
               cont.CodeOutput.Emit(OpCodes.Call,
               typeof(String).GetMethod("Compare", str2));

               if (m_op == RELATION_OPERATOR.TOK_EQ)
               {
                   cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
                   cont.CodeOutput.Emit(OpCodes.Ceq);
               }
               else
               {
                   //
                   // This logic is bit convoluted...
                   // String.Compare will give 0 , 1 or -1
                   // First we will check whether the stack value
                   // is zero..
                   // This will put 1 on stack ..if value was zero
                   // after string.Compare
                   // Once again check against zero ...it is equivalent
                   // to negation

                   cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
                   cont.CodeOutput.Emit(OpCodes.Ceq);
                   cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
                   cont.CodeOutput.Emit(OpCodes.Ceq);
                 

               }
          
           
              return true;
       }
      

       /// <summary>
       ///      Compile the Relational Expression...
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           if (_optype == TYPE_INFO.TYPE_STRING)
           {
               return CompileStringRelOp(cont);
           }
         
           //
           // Compile the Left Expression
           ex1.Compile(cont);
           //
           // Compile the Right Expression
           ex2.Compile(cont);


           if (m_op == RELATION_OPERATOR.TOK_EQ)
               cont.CodeOutput.Emit(OpCodes.Ceq);
           else if (m_op == RELATION_OPERATOR.TOK_GT)
               cont.CodeOutput.Emit(OpCodes.Cgt);
           else if (m_op == RELATION_OPERATOR.TOK_LT)
               cont.CodeOutput.Emit(OpCodes.Clt);
           else if (m_op == RELATION_OPERATOR.TOK_NEQ)
           {
               // There is no IL instruction for !=
               // We check for the equivality of the 
               // top two values on the stack ...
               // This will put 0 ( FALSE ) or 1 (TRUE)
               // on the top of stack...
               // Load zero and check once again
               // Check == once again...

               cont.CodeOutput.Emit(OpCodes.Ceq);
               cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
               cont.CodeOutput.Emit(OpCodes.Ceq);

           }
           else if (m_op == RELATION_OPERATOR.TOK_GTE)
           {

               // There is no IL instruction for >=
               // We check for the <  of the 
               // top two values on the stack ...
               // This will put 0 ( FALSE ) or 1 (TRUE)
               // on the top of stack...
               // Load Zero and 
               // Check == once again...

               cont.CodeOutput.Emit(OpCodes.Clt);
               cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
               cont.CodeOutput.Emit(OpCodes.Ceq);

           }
           else if (m_op == RELATION_OPERATOR.TOK_LTE)
           {
               // There is no IL instruction for <=
               // We check for the >  of the 
               // top two values on the stack ...
               // This will put 0 ( FALSE ) or 1 (TRUE)
               // on the top of stack...
               // Load Zero and 
               // Check == once again...

               cont.CodeOutput.Emit(OpCodes.Cgt);
               cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
               cont.CodeOutput.Emit(OpCodes.Ceq);

           }



           return true;

       }

       public override TYPE_INFO get_type()
       {
           return _type;
       }
   }
   /// <summary>
   ///     Logical Expression...
   /// </summary>
   class LogicalExp : Exp
   {
       /// <summary>
       ///    && ( AND ) , || ( OR )
       /// </summary>
       TOKEN m_op;
       /// <summary>
       ///   Operands
       /// </summary>
       private Exp ex1, ex2;
       /// <summary>
       ///     Type of the node...
       /// </summary>
       TYPE_INFO _type;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="op"></param>
       /// <param name="e1"></param>
       /// <param name="e2"></param>
       public LogicalExp(TOKEN op, Exp e1, Exp e2)
       {
           m_op = op;
           ex1 = e1;
           ex2 = e2;

       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = ex1.Evaluate(cont);
           SYMBOL_INFO eval_right = ex2.Evaluate(cont);

           if (eval_left.Type == TYPE_INFO.TYPE_BOOL &&
               eval_right.Type == TYPE_INFO.TYPE_BOOL)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.Type = TYPE_INFO.TYPE_BOOL;
               ret_val.SymbolName = "";

               if (m_op == TOKEN.TOK_AND)
                   ret_val.bol_val = ( eval_left.bol_val && eval_right.bol_val);
               else if (m_op == TOKEN.TOK_OR)
                   ret_val.bol_val = (eval_left.bol_val || eval_right.bol_val);
               else
               {
                   return null;

               }
               return ret_val;

           }

           return null;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = ex1.TypeCheck(cont);
           TYPE_INFO eval_right = ex2.TypeCheck(cont);

           // The Types should be Boolean...
           // Logical Operators only make sense
           // with Boolean Types

           if (eval_left == eval_right && 
               eval_left == TYPE_INFO.TYPE_BOOL  )
           {
               _type = TYPE_INFO.TYPE_BOOL;
               return _type;
           }
           else
           {
               throw new Exception("Wrong Type in expression");

           }
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           ex1.Compile(cont);
           ex2.Compile(cont);
           if (m_op == TOKEN.TOK_AND)
               cont.CodeOutput.Emit(OpCodes.And);
           else if (m_op == TOKEN.TOK_OR)
               cont.CodeOutput.Emit(OpCodes.Or);


           return true;
       }


       public override TYPE_INFO get_type()
       {
           return _type;
       }


   }


   /// <summary>
   ///     Logical !
   /// </summary>
   class LogicalNot : Exp
   {

       private Exp ex1;
       TYPE_INFO _type;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="op"></param>
       /// <param name="e1"></param>

       public LogicalNot(Exp e1)
       {
           
           ex1 = e1;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           SYMBOL_INFO eval_left = ex1.Evaluate(cont);
          

           if (eval_left.Type == TYPE_INFO.TYPE_BOOL)
           {
               SYMBOL_INFO ret_val = new SYMBOL_INFO();
               ret_val.Type = TYPE_INFO.TYPE_BOOL;
               ret_val.SymbolName = "";
               ret_val.bol_val = !eval_left.bol_val;
               return ret_val;
           }                  
           else
           {
                   return null;

           }
             

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           TYPE_INFO eval_left = ex1.TypeCheck(cont);
         

           if (
               eval_left == TYPE_INFO.TYPE_BOOL)
           {
               _type = TYPE_INFO.TYPE_BOOL;
               return _type;
           }
           else
           {
               throw new Exception("Wrong Type in expression");

           }
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {
           ex1.Compile(cont);

           // Check whether top of the stack is 1 ( TRUE )
           // Check Whether the previous operation was successful
           // Functionally equivalent to Logical Not
           //
           // Case Top of Stack is 1 (TRUE )
           // ------------------------------
           // Top of Stack =>    [ 1 ]
           // LDC_I4 =>  [ 1 1 ] 
           // CEQ    =>  [ 1 ]
           // LDC_I4 =>  [ 1 0 ]
           // CEQ    =>  [ 0 ]
           //
           // Case Top of Stack is 0 (FALSE)
           // -----------------------------
           // Top of Stack =>    [ 0 ]
           // LDC_I4 =>  [ 0 1 ] 
           // CEQ    =>  [ 0 ]
           // LDC_I4 =>  [ 0 0 ]
           // CEQ    =>  [ 1 ]
           cont.CodeOutput.Emit(OpCodes.Ldc_I4, 1);
           cont.CodeOutput.Emit(OpCodes.Ceq);
           cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
           cont.CodeOutput.Emit(OpCodes.Ceq);
           
           return true;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return _type;
       }


   }
    /// <summary>
    ///    The node to model Function Call 
    ///    in the Expression hierarchy...
    /// </summary>
   class CallExp : Exp
   {
       /// <summary>
       ///    Procedure Object
       /// </summary>
       Procedure m_proc;
       /// <summary>
       ///   ArrayList of Actuals
       /// </summary>
       ArrayList m_actuals;
       /// <summary>
       ///    procedure name ...
       /// </summary>
       string _procname;
       /// <summary>
       ///    Is it  a Recursive Call ?
       /// </summary>
       bool _isrecurse;

       /// <summary>
       ///    Return type of the Function
       /// </summary>
       TYPE_INFO _type;
       /// <summary>
       ///    Ctor to be called when we make a ordinary
       ///    subroutine call
       /// </summary>
       /// <param name="proc"></param>
       /// <param name="actuals"></param>
       public CallExp(Procedure proc, ArrayList actuals)
       {
           m_proc = proc;
           m_actuals = actuals;
       }
       /// <summary>
       ///    Ctor to implement Recursive sub routine
       /// </summary>
       /// <param name="name"></param>
       /// <param name="recurse"></param>
       /// <param name="actuals"></param>
       public CallExp(string name, bool recurse, ArrayList actuals)
       {
           _procname = name;
           if (recurse)
               _isrecurse = true;

           m_actuals = actuals;
           //
           // For a recursive call Procedure Address will be null
           // During the interpretation time we will resolve the 
           // call by look up...
           //    m_proc = cont.GetProgram().Find(_procname);
           // This is a hack for implementing one pass compiler
           m_proc = null;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
       {
           if (m_proc != null)
           {
               //
               // This is a Ordinary Function Call
               //
               //
               RUNTIME_CONTEXT ctx = new RUNTIME_CONTEXT(cont.GetProgram());

               ArrayList lst = new ArrayList();

               foreach (Exp ex in m_actuals)
               {
                   lst.Add(ex.Evaluate(cont));
               }

               return m_proc.Execute(ctx, lst);

           }
           else
           {
               // Recursive function call...by the time we 
               // reach here..whole program has already been 
               // parsed. Lookup the Function name table and 
               // resolve the Address
               //
               //
               m_proc = cont.GetProgram().Find(_procname);
               RUNTIME_CONTEXT ctx = new RUNTIME_CONTEXT(cont.GetProgram());
               ArrayList lst = new ArrayList();

               foreach (Exp ex in m_actuals)
               {
                   lst.Add(ex.Evaluate(cont));
               }

               return m_proc.Execute(ctx, lst);


           }
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
       {
           if (m_proc != null)
           {
               _type = m_proc.TypeCheck(cont);

           }

           return _type;

       }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public override TYPE_INFO get_type()
       {
           return _type;
       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="cont"></param>
       /// <returns></returns>
       public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
       {

           if (m_proc == null)
           {
               // if it is  a recursive call..
               // resolve the address...
               m_proc = cont.GetProgram().Find(_procname);
           }

           string name = m_proc.Name;


           TModule str = cont.GetProgram();
           MethodBuilder bld = str._get_entry_point(name);

           foreach (Exp ex in m_actuals)
           {
               ex.Compile(cont);
           }
           cont.CodeOutput.Emit(OpCodes.Call, bld);
           return true;
       }



   }
}

