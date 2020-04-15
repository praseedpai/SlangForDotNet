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
using SLANG_DOT_NET;

namespace CallSLANG
{
    class Program
    {
        static void TestFirstScript()
        {
            string a = "PRINTLINE 2*10;" + "\r\n" + "PRINTLINE 10;\r\n PRINT 2*10;\r\n";
            RDParser p = new RDParser(a);
            ArrayList arr = p.Parse();
            foreach (object obj in arr)
            {
                Stmt s = obj as Stmt;
                s.Execute(null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static void TestSecondScript()
        {
            string a = "PRINTLINE -2*10;" + "\r\n" + "PRINTLINE -10*-1;\r\n PRINT 2*10;\r\n";
            RDParser p = new RDParser(a);
            ArrayList arr = p.Parse();
            foreach (object obj in arr)
            {
                Stmt s = obj as Stmt;
                s.Execute(null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           // TestFirstScript();
            TestSecondScript();
            Console.Read();
        }
    }
}
