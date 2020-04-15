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
    ///    class for Exception Handling 
    /// </summary>
    public class CParserException : Exception
    {
        private int ErrorCode;
        private String ErrorString;
        private int Lexical_Offset;
        /// <summary>
        ///   Ctor
        /// </summary>
        /// <param name="pErrorCode"></param>
        /// <param name="pErrorString"></param>
        /// <param name="pLexical_Offset"></param>

        public CParserException(int pErrorCode,
            String pErrorString,
            int pLexical_Offset)
        {
            ErrorCode = pErrorCode;
            ErrorString = pErrorString;
            Lexical_Offset = pLexical_Offset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetErrorCode()
        {
            return ErrorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String GetErrorString()
        {
            return ErrorString;
        }
        /// <summary>
        ///   
        /// </summary>
        /// <returns></returns>

        public int GetLexicalOffset()
        {
            return Lexical_Offset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lex"></param>

        public void SetLexicalOffset(int lex)
        {
            Lexical_Offset = lex;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStr"></param>

        public void SetErrorString(String pStr)
        {
            ErrorString = pStr;
        }


    }

    /// <summary>
    ///    Error for semntic erros
    /// </summary>
    /// 
    public class CSemanticErrorLog
    {
        /// <summary>
        ///     
        /// </summary>
        static int ErrorCount = 0;
        static ArrayList lst = new ArrayList();
        /// <summary>
        ///    Ctor
        /// </summary>
        static CSemanticErrorLog()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static void Cleanup()
        {
            lst.Clear();
            ErrorCount = 0;
        }
        /// <summary>
        ///    Get Logged data as a String 
        /// </summary>
        /// <returns></returns>
        public static String GetLog()
        {


            String str = "Logged data by the user and processing status" + "\r\n";
            str += "--------------------------------------\r\n";

            int xt = lst.Count;

            if (xt == 0)
            {
                str += "NIL" + "\r\n";

            }
            else
            {

                for (int i = 0; i < xt; ++i)
                {
                    str = str + lst[i].ToString() + "\r\n";
                }
            }
            str += "--------------------------------------\r\n";
            return str;
        }
        /// <summary>
        ///    Add a Line to Log
        /// </summary>
        /// <param name="str"></param>
        public static void AddLine(String str)
        {
            lst.Add(str.Substring(0));
            ErrorCount++;
        }
        /// <summary>
        ///   Add From a Script   
        /// </summary>
        /// <param name="str"></param>

        public static void AddFromUser(String str)
        {
            lst.Add(str.Substring(0));
            ErrorCount++;

        }



    }
    /// <summary>
    ///    
    /// </summary>
    public class CSyntaxErrorLog
    {

        /// <summary>
        ///   instance variables
        /// </summary>
        static int ErrorCount = 0;
        static ArrayList lst = new ArrayList();
        /// <summary>
        ///    Ctor
        /// </summary>
        static CSyntaxErrorLog()
        {

        }


        public static void Cleanup()
        {
            lst.Clear();
            ErrorCount = 0;
        }
        /// <summary>
        ///    Add a Line from script
        /// </summary>
        /// <param name="str"></param>

        public static void AddLine(String str)
        {
            lst.Add(str.Substring(0));
            ErrorCount++;

        }

        /// <summary>
        ///    Get Logged data as a String 
        /// </summary>
        /// <returns></returns>
        public static String GetLog()
        {

            String str = "Syntax Error" + "\r\n";
            str += "--------------------------------------\r\n";

            int xt = lst.Count;

            if (xt == 0)
            {
                str += "NIL" + "\r\n";

            }
            else
            {

                for (int i = 0; i < xt; ++i)
                {
                    str = str + lst[i].ToString() + "\r\n";
                }
            }
            str += "--------------------------------------\r\n";
            return str;
        }
    }
}
