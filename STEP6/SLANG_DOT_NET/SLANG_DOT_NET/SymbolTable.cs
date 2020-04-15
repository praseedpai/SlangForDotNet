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
