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
    public enum TOKEN
    {
        ILLEGAL_TOKEN = -1, // Not a Token
        TOK_PLUS = 1, // '+'
        TOK_MUL, // '*'
        TOK_DIV, // '/'
        TOK_SUB, // '-'
        TOK_OPAREN, // '('
        TOK_CPAREN, // ')'
        TOK_DOUBLE, // 'number'
        TOK_NULL, // End of string
        TOK_PRINT, // Print Statement
        TOK_PRINTLN, // PrintLine
        TOK_UNQUOTED_STRING,
        TOK_SEMI // ; 
    }

    /// <summary>
    ///     Keyword Table Entry
    /// </summary>
    /// 
    public struct ValueTable
    {
        public TOKEN tok;          // Token id
        public String Value;       // Token string  
        public ValueTable(TOKEN tok, String Value)
        {
            this.tok = tok;
            this.Value = Value;

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Lexer
    {

        private string _exp;
        private int _index;
        private int _length_string;
        private double _curr_num;
        private ValueTable[] _val = null;
        private string last_str;

        public Lexer(string exp)
        {
            _exp = exp;
            _length_string = exp.Length;
            _index = 0;

            _val = new ValueTable[2];
            _val[0] = new ValueTable(TOKEN.TOK_PRINT, "PRINT");
            _val[1] = new ValueTable(TOKEN.TOK_PRINTLN, "PRINTLINE");
        }


        public double Number
        {
            get { return _curr_num; }
        }

        public double GetNumber()
        {
            return _curr_num;
        }

        public TOKEN GetToken()
        {
        re_start: /// Label
            TOKEN tok = TOKEN.ILLEGAL_TOKEN;

            //// Skipping white spaces
            while ((_index < _length_string)
                && (_exp[_index] == ' ' || _exp[_index] == '\t'))
            {
                _index++;
            }

            /// Enf Of Expression
            if (_index == _length_string)
            {
                return TOKEN.TOK_NULL;
            }



            switch (_exp[_index])
            {
                case '\r':
                case '\n':
                    _index++;
                    goto re_start;
                case '+':
                    tok = TOKEN.TOK_PLUS;
                    _index++;
                    break;
                case '-':
                    tok = TOKEN.TOK_SUB;
                    _index++;
                    break;
                case '/':
                    tok = TOKEN.TOK_DIV;
                    _index++;
                    break;
                case '*':
                    tok = TOKEN.TOK_MUL;
                    _index++;
                    break;
                case '(':
                    tok = TOKEN.TOK_OPAREN;
                    _index++;
                    break;
                case ')':
                    tok = TOKEN.TOK_CPAREN;
                    _index++;
                    break;
                case ';':
                    tok = TOKEN.TOK_SEMI;
                    _index++;
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
                        string str = "";
                        while ((_index < _length_string)
                            && (_exp[_index] == '0' ||
                            _exp[_index] == '1' ||
                            _exp[_index] == '2' ||
                            _exp[_index] == '3' ||
                            _exp[_index] == '4' ||
                            _exp[_index] == '5' ||
                            _exp[_index] == '6' ||
                            _exp[_index] == '7' ||
                            _exp[_index] == '8' ||
                            _exp[_index] == '9'))
                        {
                            str += Convert.ToString(_exp[_index]);
                            _index++;
                        }
                        _curr_num = Convert.ToDouble(str);
                        tok = TOKEN.TOK_DOUBLE;

                    }
                    break;
                default:
                    {
                        if (char.IsLetter(_exp[_index]))
                        {

                            String tem = Convert.ToString(_exp[_index]);
                            _index++;
                            while (_index < _length_string && (char.IsLetterOrDigit(_exp[_index]) ||
                            _exp[_index] == '_'))
                            {
                                tem += _exp[_index];
                                _index++;
                            }

                            tem = tem.ToUpper();

                            for (int i = 0; i < this._val.Length; ++i)
                            {
                                if (_val[i].Value.CompareTo(tem) == 0)
                                    return _val[i].tok;

                            }


                            this.last_str = tem;



                            return TOKEN.TOK_UNQUOTED_STRING;



                        }
                        else
                        {
                            Console.WriteLine("Error");
                            throw new Exception();
                        }

                    }
            }
            return tok;
        }
    }
}
