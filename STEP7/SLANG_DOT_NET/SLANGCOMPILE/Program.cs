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
using System.IO;
using SLANG_DOT_NET;
namespace SLANGCOMPILE
{
    class Caller
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
            sr.Close();
            sr.Dispose(); 

            //---------------- Creates the Parser Object
            // With Program text as argument 
            RDParser pars = null;
            pars = new RDParser(programs2);
            TModule p = null;
            p = pars.DoParse();

            if (p == null)
            {
                Console.WriteLine("Parse Process Failed ");
                return;
            }
            //
            //  Now that Parse is Successul...
            //  Create an Executable...!
            //
            if (p.CreateExecutable("First.exe"))
            {
                Console.WriteLine("Creation of Executable is successul");
                return;
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
                Console.WriteLine("SLANGCOMPILE <scriptname>\n");
                return;

            }
            TestFileScript(args[0]);
            //------------- Wait for the Key Press
            Console.Read();

        }
    }
}
