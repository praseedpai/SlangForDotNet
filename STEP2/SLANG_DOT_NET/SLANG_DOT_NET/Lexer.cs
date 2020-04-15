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

namespace SLANG_DOT_NET
{
    /// <summary>
    ///   Enumeration for Tokens
    /// </summary>
    public enum TOKEN
    {
        ILLEGAL_TOKEN = -1, // Not a Token
        TOK_PLUS = 1, // '+'
        TOK_MUL, // '*'
        TOK_DIV, // '/'
        TOK_SUB, // '-'
        TOK_OPAREN, // '('
        TOK_CPAREN, // ')'
        TOK_DOUBLE, // '('
        TOK_NULL // End of string
    }

    //////////////////////////////////////////////////////////
    //
    // A naive Lexical analyzer which looks for operators , Parenthesis
    // and number. All numbers are treated as IEEE doubles. Only numbers
    // without decimals can be entered. Feel free to modify the code
    // to accomodate LONG and Double values
    public class Lexer
    {
        String IExpr; // Expression string
        int index; // index into a character
        int length; // Length of the string
        double number; // Last grabbed number from the stream
        /////////////////////////////////////////////
        //
        // Ctor
        //
        //
        public Lexer(String Expr)
        {
            IExpr = Expr;
            length = IExpr.Length;
            index = 0;
        }
        /////////////////////////////////////////////////////
        // Grab the next token from the stream
        //
        //
        //
        //
        public TOKEN GetToken()
        {
            TOKEN tok = TOKEN.ILLEGAL_TOKEN;
            ////////////////////////////////////////////////////////////
            //
            // Skip the white space
            //
            while (index < length &&
            (IExpr[index] == ' ' || IExpr[index] == '\t'))
                index++;
            //////////////////////////////////////////////
            //
            // End of string ? return NULL;
            //
            if (index == length)
                return TOKEN.TOK_NULL;
            /////////////////////////////////////////////////
            //
            //
            //
            switch (IExpr[index])
            {
                case '+':
                    tok = TOKEN.TOK_PLUS;
                    index++;
                    break;
                case '-':
                    tok = TOKEN.TOK_SUB;
                    index++;
                    break;
                case '/':
                    tok = TOKEN.TOK_DIV;
                    index++;
                    break;
                case '*':
                    tok = TOKEN.TOK_MUL;
                    index++;
                    break;
                case '(':
                    tok = TOKEN.TOK_OPAREN;
                    index++;
                    break;
                case ')':
                    tok = TOKEN.TOK_CPAREN;
                    index++;
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        String str = "";
                        while (index < length &&
                        (IExpr[index] == '0' ||
                        IExpr[index] == '1' ||
                        IExpr[index] == '2' ||
                        IExpr[index] == '3' ||
                        IExpr[index] == '4' ||
                        IExpr[index] == '5' ||
                        IExpr[index] == '6' ||
                        IExpr[index] == '7' ||
                        IExpr[index] == '8' ||
                        IExpr[index] == '9'))
                        {
                            str += Convert.ToString(IExpr[index]);
                            index++;
                        }
                        number = Convert.ToDouble(str);
                        tok = TOKEN.TOK_DOUBLE;
                    }
                    break;
                default:
                    Console.WriteLine("Error While Analyzing Tokens");
                    throw new Exception();
            }
            return tok;
        }
        public double GetNumber() { return number; }
    }
}
