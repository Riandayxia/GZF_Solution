using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;

namespace SuHui.Framework.DynamicCalculate.Service
{
    public class CalculateCompilerException : Exception
    {
        private StringWriter _ErrorInfo = new StringWriter();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCollection"></param>
        internal CalculateCompilerException(CompilerErrorCollection errorCollection)
        {
            foreach (CompilerError error in errorCollection)
            {
                this._ErrorInfo.WriteLine(error.ErrorText);
            }
        }

        public string Messege
        {
            get
            {
                return this._ErrorInfo.ToString();
            }
        }
    }
}
