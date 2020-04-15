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
using System.Collections;
using System.IO;
using SLANG_DOT_NET;

namespace CallSLANG
{
    class Program
    {
      
        /// <summary>
        ///    Driver routine to call the program script
        /// </summary>
        static void TestFileScript(string filename)
        {
            
            if (filename == null)
                    return;


            // -------------- Read the contents from the file

            StreamReader sr = new StreamReader(filename);
            string programs2 = sr.ReadToEnd();
            

            //---------------- Creates the Parser Object
            // With Program text as argument 
            RDParser pars = null;
            pars = new RDParser(programs2);

            // Create a Compilation Context 
            //
            //
            COMPILATION_CONTEXT ctx = new COMPILATION_CONTEXT();

            //
            // Call the top level Parsing Routine with 
            // Compilation Context as the Argument
            //
            ArrayList stmts = pars.Parse(ctx);

            //
            // if we have reached here , the parse process 
            // is successful... Create a Run time context and 
            // Call Execute statements of each statement...
            //

            RUNTIME_CONTEXT f = new RUNTIME_CONTEXT();
            foreach(Object obj in stmts )
            {
                Stmt s = obj as Stmt;
                s.Execute(f);
            }
                        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
         

            if (args == null ||
                 args.Length != 1)
            {
                Console.WriteLine("CallSlang <scriptname>\n");
                return;

            }
            TestFileScript(args[0]);
            //------------- Wait for the Key Press
            Console.Read();
            
        }
    }
}
