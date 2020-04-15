using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLANG_DOT_NET
{
    /// <summary>
    ///   Statement is what you Execute for it's Effect
    /// </summary>
    public abstract class Stmt
    {
        public abstract bool Execute(RUNTIME_CONTEXT con);
    }

    /// <summary>
    /// Implementation of Print Statement
    /// </summary>

    public class PrintStatement : Stmt
    {
        /// <summary>
        ///   At this point of time , Print will 
        ///   spit the value of an Expression on the screen.
        /// </summary>
        private Exp _ex;
        /// <summary>
        ///    Ctor just stores the expression passed as parameter
        /// </summary>
        /// <param name="ex"></param>
        public PrintStatement(Exp ex)
        {
            _ex = ex;
        }

        /// <summary>
        ///    Execute method Evaluates the expression and
        ///    spits the value to the console using 
        ///    Console.Write statement.
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public override bool Execute(RUNTIME_CONTEXT con)
        {
            double a = _ex.Evaluate(con);
            Console.Write(a.ToString());
            return true;
        }
    }


    /// <summary>
    ///  Implementation of  PrintLine Statement
    /// </summary>
    public class PrintLineStatement : Stmt
    {
        private Exp _ex;

        public PrintLineStatement(Exp ex)
        {
            _ex = ex;
        }
        /// <summary>
        ///    Here we are calling Console.WriteLine to emit
        ///    an additional new line .
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public override bool Execute(RUNTIME_CONTEXT con)
        {
            double a = _ex.Evaluate(con);
            Console.WriteLine(a.ToString());
            return true;
        }
    }
}
