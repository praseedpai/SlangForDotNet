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
using System.Reflection;
using System.Reflection.Emit;

namespace SLANG_DOT_NET
{
    /// <summary>
    /// 
    /// 	Abstract base Class for Statement
    ///     ( one can use an interface as well !)
    ///
    /// </summary>
    public abstract class Stmt
    {
        public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont);
        //
        // Added in the Step 5 for .net IL compilation
        //
        public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
     
    }
    /// <summary>
    ///   implementation of Print Statement
    /// </summary>
    public class PrintStatement : Stmt
    {

        private Exp exp1;

        public PrintStatement(Exp e)
        {

            exp1 = e;

        }

        public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
        {
            SYMBOL_INFO val = exp1.Evaluate(cont);
            Console.WriteLine((val.Type == TYPE_INFO.TYPE_NUMERIC) ? val.dbl_val.ToString() :
                (val.Type == TYPE_INFO.TYPE_STRING) ? val.str_val : val.bol_val ? "TRUE" : "FALSE");
            return null;

        }

        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
            //
            //  Compile the Expression
            //  The Output will be on the top of stack
            exp1.Compile(cont);
            //
            // Generate Code to Call Console.Write
            //
            System.Type typ = Type.GetType("System.Console");
            Type[] Parameters = new Type[1];

            TYPE_INFO tdata = exp1.get_type();

            if (tdata == TYPE_INFO.TYPE_STRING)
                Parameters[0] = typeof(string);
            else if (tdata == TYPE_INFO.TYPE_NUMERIC)
                Parameters[0] = typeof(double);
            else
                Parameters[0] = typeof(bool);
            cont.CodeOutput.Emit(OpCodes.Call, typ.GetMethod("Write", Parameters));
            return true;
        }

       


    }

    /// <summary>
    ///  Implementation of  PrintLine Statement
    /// </summary>
    public class PrintLineStatement : Stmt 
	{
		
		private Exp exp1;

		public PrintLineStatement(  Exp e ) 
		{
			
			exp1 = e;

		}

		public override SYMBOL_INFO  Execute( RUNTIME_CONTEXT cont  ) 
		{
			SYMBOL_INFO  val = exp1.Evaluate(cont);
			Console.WriteLine((val.Type == TYPE_INFO.TYPE_NUMERIC  ) ? val.dbl_val.ToString()  :
				( val.Type == TYPE_INFO.TYPE_STRING ) ? val.str_val : val.bol_val ? "TRUE" : "FALSE" );
			return null;

		}

        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
            exp1.Compile(cont);
            System.Type typ = Type.GetType("System.Console");
            Type[] Parameters = new Type[1];

            TYPE_INFO tdata = exp1.get_type();

            if (tdata == TYPE_INFO.TYPE_STRING)
                Parameters[0] = typeof(string);
            else if (tdata == TYPE_INFO.TYPE_NUMERIC)
                Parameters[0] = typeof(double);
            else
                Parameters[0] = typeof(bool);
            cont.CodeOutput.Emit(OpCodes.Call, typ.GetMethod("WriteLine", Parameters));
            return true;
        }

	}

    /// <summary>
    ///    Compile the Variable Declaration statements
    /// </summary>
    public class VariableDeclStatement : Stmt
    {
        /// <summary>
        ///    
        /// </summary>
        SYMBOL_INFO m_inf = null;
        Variable var = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inf"></param>
        public VariableDeclStatement(SYMBOL_INFO inf)
        {
            m_inf = inf;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
        {
            cont.TABLE.Add(m_inf);
            var = new Variable(m_inf);
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
            //
            // Retrieve the type from the SYMBOL_INFO
            //
            System.Type type = (m_inf.Type == TYPE_INFO.TYPE_BOOL) ?
                typeof(bool) : (m_inf.Type == TYPE_INFO.TYPE_NUMERIC) ?
                typeof(double) : typeof(string);
            //
            //  Get the offset of the variable
            //
            int s = cont.DeclareLocal(type);
            // Store the offset in the SYMBOL_INFO
            //
            m_inf.loc_position = s;
            //
            // Add the variable into Symbol Table..
            //
            cont.TABLE.Add(m_inf);

            return true;
        }

       

    }

    /// <summary>
    ///   Assignment Statement
    /// </summary>
    public class AssignmentStatement : Stmt
    {
        private Variable variable;
        private Exp exp1;

        public AssignmentStatement(Variable var, Exp e)
        {
            variable = var;
            exp1 = e;

        }

        public AssignmentStatement(SYMBOL_INFO var, Exp e)
        {
            variable = new Variable(var);
            exp1 = e;

        }
        public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
        {
            SYMBOL_INFO val = exp1.Evaluate(cont);
            cont.TABLE.Assign(variable, val);
            return null;
        }

        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
            if (!exp1.Compile(cont))
            {
                throw new Exception("Compilation in error string");
            }
            SYMBOL_INFO info = cont.TABLE.Get(variable.Name);
            LocalBuilder lb = cont.GetLocal(info.loc_position);
            cont.CodeOutput.Emit(OpCodes.Stloc, lb);
            return true;
        }


       
    }

}

