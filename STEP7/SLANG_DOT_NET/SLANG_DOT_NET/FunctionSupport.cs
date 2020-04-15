using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SLANG_DOT_NET
{
    /// <summary>
    ///      FUNCTION info
    /// </summary>
    class FUNCTION_INFO
    {
        public TYPE_INFO _ret_value;
        public string _name;
        public ArrayList _typeinfo;

        public FUNCTION_INFO(string name, TYPE_INFO ret_value,
            ArrayList formals)
        {

            _ret_value = ret_value;
            _typeinfo = formals;
            _name = name;
        }
    }
    /// <summary>
    ///    Frame for recursive calls
    /// </summary>
    class FRAME
    {
        private SymbolTable tab;
        public FRAME()
        {
            tab = new SymbolTable();
        }

        public SymbolTable TABLE
        {
            get
            {
                return tab;

            }

        }

    }

}
