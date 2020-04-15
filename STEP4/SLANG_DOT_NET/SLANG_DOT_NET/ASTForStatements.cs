using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


       
    }

}

