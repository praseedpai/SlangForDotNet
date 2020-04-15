using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SLANG_DOT_NET
{
    /// <summary>
    ///   Symbol Table for Parsing and Type Analysis
    /// </summary>
    public class SymbolTable
    {
        /// <summary>
        ///    private data structure
        /// </summary>
        private System.Collections.Hashtable dt = new Hashtable();
        /// <summary>
        ///    Add a symbol to Symbol Table
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Add(SYMBOL_INFO s)
        {
            dt[s.SymbolName] = s;
            return true;
        }

        /// <summary>
        ///    Retrieve the Symbol
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SYMBOL_INFO Get(string name)
        {
            return dt[name] as SYMBOL_INFO;
        }

        /// <summary>
        ///    Assign to the Symbol Table
        /// </summary>
        /// <param name="var"></param>
        /// <param name="value"></param>
        public void Assign(Variable var, SYMBOL_INFO value)
        {
            value.SymbolName = var.GetName();
            dt[var.GetName()] = value;

        }
        /// <summary>
        ///   Assign to a variable
        /// </summary>
        /// <param name="var"></param>
        /// <param name="value"></param>
        public void Assign(string var, SYMBOL_INFO value)
        {
            dt[var] = value;
        }

    }

}
