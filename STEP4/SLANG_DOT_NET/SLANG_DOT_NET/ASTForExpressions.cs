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

   }
}
