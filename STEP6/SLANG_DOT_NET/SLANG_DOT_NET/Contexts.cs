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
        ///    
        /// </summary>
        private TModule _prog = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

		public TModule GetProgram()
		{
             return _prog;  

		}

        /// <summary>
        ///    Create an instance of Symbol Table
        /// </summary>
        public RUNTIME_CONTEXT(TModule  Pgrm)
        {
            m_dt = new SymbolTable();
            _prog = Pgrm;
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
   ///      DNET_EXECUTABLE_GENERATION_CONTEXT is for generating 
   ///      CLR executable
   /// </summary>
   public class DNET_EXECUTABLE_GENERATION_CONTEXT
   {
       /// <summary>
       ///     ILGenerator Object
       /// </summary>
       private ILGenerator ILout;
       /// <summary>
       ///    Auto (Local) Variable support
       ///    Stores the index return by DefineLocal
       ///    method of MethodBuilder
       /// </summary>
       private ArrayList variables = new ArrayList();
       /// <summary>
       ///    Symbol Table for storing Types and 
       ///    doing the type analysis
       /// </summary>
       SymbolTable m_tab = new SymbolTable();
       /// <summary>
       ///    CLR Reflection.Emit.MethodBuilder
       /// </summary>
       MethodBuilder _methinfo = null;
       /// <summary>
       ///     CLR Type Builder ( useful for creating
       ///     classes in the run time
       /// </summary>
       TypeBuilder _bld=null;

       /// <summary>
       ///    Procedure to compiled
       /// </summary>
       Procedure _proc = null;
       /// <summary>
       ///    Program to be compiled...
       ///    
       /// </summary>
       TModule _program;
      
       /// <summary>
       /// 
       /// </summary>
       /// <param name="prog"></param>
       /// <param name="p"></param>
       /// <param name="bld"></param>
       public DNET_EXECUTABLE_GENERATION_CONTEXT(TModule  program ,
                                                 Procedure proc,
                                                 TypeBuilder bld)
       {
           // All the code in the Source module is compiled
           // into this procedure
           _proc = proc;
           //
           //  TModule Object
           //
           _program = program;
           //
           // Handle to the type (MainClass )
           //
           _bld = bld;
           //
           //  The method does not take any procedure
           System.Type[] s = null;
           // Return type is void
           System.Type ret_type = null;
           //
           //  public static void Main()
           //
           _methinfo = _bld.DefineMethod("Main", 
               MethodAttributes.Public | MethodAttributes.Static, 
               ret_type, s);
           //
           // We have created the Method Prologue
           // Get the handle to the code generator
           //
           //
           ILout = _methinfo.GetILGenerator();

       }

       /// <summary>
       /// 
       /// </summary>
       public string MethodName
       {
           get
           {
               return _proc.Name ;
           }
       }
       /// <summary>
       /// 
       /// </summary>
       public MethodBuilder MethodHandle
       {

           get
           {
               return _methinfo;
           }


       }
       
       /// <summary>
       /// 
       /// </summary>
       public TypeBuilder TYPEBUILDER
       {

           get
           {

               return _bld;
           }

       }

       /// <summary>
       /// 
       /// </summary>
       public SymbolTable TABLE
       {

           get
           {
               return m_tab;
           }

       }
       /// <summary>
       /// 
       /// </summary>
       public ILGenerator CodeOutput
       {
           get
           {
               return ILout;
           }

       }

       /// <summary>
       ///      
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public int DeclareLocal(System.Type type)
       {
           // It is possible to create Local ( auto )
           // Variables by Calling DeclareLocl method 
           // of ILGenerator... this returns an integer
           // We store this integer value in the variables
           // collection...
           LocalBuilder lb = ILout.DeclareLocal(type);
           // Now add the integer value associated with the 
           // variable to variables collection...
           return variables.Add(lb);
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
       public LocalBuilder GetLocal(int s)
       {

           return variables[s] as LocalBuilder;

       }



   }

}
