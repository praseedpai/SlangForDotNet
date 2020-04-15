using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLANG_DOT_NET
{
    /// <summary>
    ///   A Context is necessary for Variable scope...will be used later  
    /// </summary>
    public class COMPILATION_CONTEXT
    {
        /// <summary>
        ///    Symbol Table for this context
        /// </summary>
        private SymbolTable m_dt;

        /// <summary>
        ///    Create an instance of Symbol Table
        /// </summary>
        public COMPILATION_CONTEXT()
        {
            m_dt = new SymbolTable();
        }

        /// <summary>
        ///    Property to retrieve Table
        /// </summary>
        public SymbolTable TABLE
        {

            get
            {
                return m_dt;
            }

            set
            {
                m_dt = value;
            }
        }



    }

    /// <summary>
    ///   A Context is necessary for Variable scope...will be used later  
    /// </summary>
   public class RUNTIME_CONTEXT
    {
        /// <summary>
        ///    Symbol Table for this context
        /// </summary>
        private SymbolTable m_dt;
     

        /// <summary>
        ///    Create an instance of Symbol Table
        /// </summary>
        public RUNTIME_CONTEXT()
        {
            m_dt = new SymbolTable();
           
        }

      

        /// <summary>
        ///    Property to retrieve Table
        /// </summary>
        public SymbolTable TABLE
        {

            get
            {
                return m_dt;
            }

            set
            {
                m_dt = value;
            }
        }



    }

}
