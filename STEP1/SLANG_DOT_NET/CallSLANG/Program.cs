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
using SLANG_DOT_NET; // include SLANG_DOT_NET assembly

namespace CallSLANG
{
    class Program
    {
        static void Main(string[] args)
        {
            // Abstract Syntax Tree (AST) for 5*10
            Exp e = new BinaryExp(new NumericConstant(5),
                                   new NumericConstant(10),
                                   OPERATOR.MUL);

            //
            // Evaluate the Expression
            //
            //
            Console.WriteLine(e.Evaluate(null));

            // AST for  -(10 + (30 + 50 ) )

            e = new UnaryExp(
                         new BinaryExp(new NumericConstant(10),
                             new BinaryExp(new NumericConstant(30),
                                           new NumericConstant(50),
                                  OPERATOR.PLUS),
                         OPERATOR.PLUS),
                     OPERATOR.MINUS);

            //
            // Evaluate the Expression
            //
            Console.WriteLine(e.Evaluate(null));

            //
            // Pause for a key stroke
            //
            Console.Read();

        }
    }
}
