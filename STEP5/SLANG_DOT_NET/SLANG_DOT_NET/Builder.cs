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

namespace SLANG_DOT_NET
{
    /// <summary>
    ///    Base class for all the Builder
    ///    
    /// 
    /// 
    ///            AbstractBuilder
    ///                     TModuleBuilder
    ///                     ProcedureBuilder
    /// </summary>
    public class AbstractBuilder
    {

    }
    /// <summary>
    ///      A Builder for Creating a Module
    /// </summary>
    class TModuleBuilder : AbstractBuilder
    {
        /// <summary>
        ///     Array of Procs 
        /// </summary>
        private ArrayList procs;
        /// <summary>
        ///    Array of Function Prototypes
        ///    not much use as of now...
        /// </summary>
        private ArrayList protos=null;

        /// <summary>
        ///     Ctor does not do much
        /// </summary>
        public TModuleBuilder()
        {
            procs = new ArrayList();
            protos = null;
        }
        
        /// <summary>
        ///     Add Procedure
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Add(Procedure p)
        {
            procs.Add(p);
            return true;
        }

        /// <summary>
        ///      Create Program 
        /// </summary>
        /// <returns></returns>
        public TModule GetProgram()
        {
            return new TModule(procs);
        }

        ///
        ///
        ///
        public Procedure GetProc(string name)
        {
            foreach (Procedure p in procs)
            {
                if (p.Name.Equals(name))
                {
                    return p;
                }

            }

            return null;

        }

    }

    /// <summary>
    ///    
    /// </summary>
    public class ProcedureBuilder : AbstractBuilder
    {
        /// <summary>
        ///    Procedure name ..now it is hard coded
        ///    to MAIN
        /// </summary>
        private string proc_name = "";
        /// <summary>
        ///    Compilation context for type analysis
        /// </summary>
        COMPILATION_CONTEXT ctx = null;
        /// <summary>
        ///    Procedure does not take any argument..
        /// </summary>
        ArrayList m_formals = null;
        /// <summary>
        ///    Array of Statements
        /// </summary>
        ArrayList m_stmts = new ArrayList();
        /// <summary>
        ///    Return Type of the procedure
        /// </summary>
        TYPE_INFO inf = TYPE_INFO.TYPE_ILLEGAL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="_ctx"></param>

        public ProcedureBuilder(string name, COMPILATION_CONTEXT _ctx)
        {
            ctx = _ctx;
            proc_name = name;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddLocal(SYMBOL_INFO info)
        {
            ctx.TABLE.Add(info);
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public TYPE_INFO TypeCheck(Exp e)
        {
            return e.TypeCheck(ctx);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        public void AddStatement(Stmt st)
        {
            m_stmts.Add(st);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        public SYMBOL_INFO GetSymbol(string strname)
        {

            return ctx.TABLE.Get(strname);

        }

        /// <summary>
        ///   Check the function Prototype
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckProto(string name)
        {
            return true;

        }
        /// <summary>
        /// 
        /// </summary>
        public TYPE_INFO TYPE
        {
            get
            {
                return inf;
            }

            set
            {
                inf = value;

            }


        }
        /// <summary>
        /// 
        /// </summary>
        public SymbolTable TABLE
        {
            get
            {
                return ctx.TABLE;
            }
        }

        public COMPILATION_CONTEXT Context
        {
            get
            {
                return ctx;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return proc_name;
            }

            set
            {
                proc_name = value;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Procedure GetProcedure()
        {
            Procedure ret = new Procedure(proc_name, 
                    m_stmts, ctx.TABLE, inf);

            return ret;
        }





    }


}
