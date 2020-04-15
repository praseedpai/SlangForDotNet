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
    ///    A bunch of statement is called a Compilation
    ///    unit at this point of time... STEP 5
    ///    In future , a Collection of Procedures will be
    ///    called a Compilation unit
    ///    
    ///    Added in the STEP 5
    /// </summary>
    public abstract class CompilationUnit
    {
        public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont);
        public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
    }

    /// <summary>
    ///    Abstract base class for Procedure
    ///    All the statements in a Program ( Compilation unit )
    ///    will be compiled into a PROC 
    /// </summary>
    public abstract class PROC
    {
        public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont);
        public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);


    }

    /// <summary>
	///     A CodeModule is a Compilation Unit ..
    ///     At this point of time ..it is just a bunch
    ///     of statements... 
	/// </summary>
    public class TModule : CompilationUnit
    {
        /// <summary>
        ///    A Program is a collection of Procedures...
        ///    Now , we support only global function...
        /// </summary>
        private ArrayList m_procs=null;
        /// <summary>
        ///    List of Compiled Procedures....
        ///    At this point of time..only one procedure
        ///    will be there....
        /// </summary>
        private ArrayList compiled_procs = null;
        /// <summary>
        ///    class to generate IL executable... 
        /// </summary>

        private ExeGenerator _exe = null;

        /// <summary>
        ///    Ctor for the Program ...
        /// </summary>
        /// <param name="procedures"></param>

        public TModule(ArrayList procs)
        {
            m_procs = procs;
            
        }

        /// <summary>
        ///      
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateExecutable(string name)
        {
            //
            // Create an instance of Exe Generator
            // ExeGenerator takes a TModule and 
            // exe name as the Parameter...
            _exe = new ExeGenerator(this,name);
            // Compile The module...
            Compile(null);
            // Save the Executable...
            _exe.Save();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
            compiled_procs = new ArrayList();
            foreach (Procedure p in m_procs)
            {
                DNET_EXECUTABLE_GENERATION_CONTEXT con = new DNET_EXECUTABLE_GENERATION_CONTEXT(this,p, _exe.type_bulder);
                compiled_procs.Add(con);
                p.Compile(con);

            }
            return true;

        }

        public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
        {
            Procedure p = Find("Main");

            if (p != null)
            {

                return p.Execute(cont);
            }

            return null;

        }

        public MethodBuilder _get_entry_point(string _funcname)
        {
            foreach (DNET_EXECUTABLE_GENERATION_CONTEXT u in compiled_procs)
            {
                if (u.MethodName.Equals(_funcname))
                {
                    return u.MethodHandle;
                }

            }

            return null;


        }

        public Procedure Find(string str)
        {
            foreach (Procedure p in m_procs)
            {
                string pname = p.Name;

                if (pname.ToUpper().CompareTo(str.ToUpper()) == 0)
                    return p;

            }

            return null;

        }


    }

    /// <summary>
    ///     A Procedure which returns an Exit Code...
    ///     It defaults to 0 in this step...!
    /// </summary>
    public class Procedure : PROC
    {
        /// <summary>
        ///    Procedure name ..which defaults to Main 
        ///    in the type MainClass
        /// </summary>
        public string m_name;
        /// <summary>
        ///    Formal parameters...
        /// </summary>
        public ArrayList m_formals = null;
        /// <summary>
        ///     List of statements which comprises the Procedure
        /// </summary>
        public ArrayList m_statements = null;
        /// <summary>
        ///     Local variables
        /// </summary>
        public SymbolTable m_locals = null;
        /// <summary>
        ///        return_value.... a hard coded zero at this
        ///        point of time..
        /// </summary>
        public SYMBOL_INFO return_value = null;
        /// <summary>
        ///       TYPE_INFO => TYPE_NUMERIC
        /// </summary>
        public TYPE_INFO _type = TYPE_INFO.TYPE_ILLEGAL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="formals"></param>
        /// <param name="stats"></param>
        /// <param name="locals"></param>
        /// <param name="type"></param>

        public Procedure(string name, 
                         ArrayList stats, 
                         SymbolTable locals, 
                         TYPE_INFO type)
        {
            m_name = name;
            m_formals = null;
            m_statements = stats;
            m_locals = locals;
            _type = type;
        }
        /// <summary>
        /// 
        /// </summary>
        public TYPE_INFO TYPE
        {

            get
            {
                return _type;

            }

        }
        /// <summary>
        ///     Null at this point of time...
        /// </summary>
        public ArrayList FORMALS
        {
            get
            {
                return m_formals;
            }

        }

        public string Name
        {
            set
            {

                Name = value;
            }

            get
            {
                return m_name;
            }

        }

        public SYMBOL_INFO ReturnValue()
        {
            return return_value;
        }

        public TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
        {

            return TYPE_INFO.TYPE_NUMERIC;
        }


        public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
        {
                    

            foreach (Stmt e1 in m_statements)
            {
                e1.Compile(cont);
            }
           
            cont.CodeOutput.Emit(OpCodes.Ret);
            return true;

        }


        public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
        {
                     
            foreach (Stmt stmt in m_statements)
                      stmt.Execute(cont);
            
            return null;

        }
    }
}
