using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

namespace SuHui.Framework.DynamicCalculate.Service
{
    /// <summary>
    /// 可扩展公式解析的接口
    /// </summary>
    public interface IFormulaCodeParse
    {
        /// <summary>
        /// 公式类名(通过参数名来分解)
        /// </summary>
        string RegexParamFormulaNameString
        {
            get;
        }

        /// <summary>
        /// 公式和参数名称匹配的正则表达式
        /// </summary>
        string RegexParamNameString
        {
            get;
        }

        /// <summary>
        /// 有引用的程序集名称
        /// </summary>
        StringCollection CompilerAssemblys
        {
            get;
            set;
        }

        /// <summary>
        /// 解析公式
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        string GetCompilerCode(string code, IList<CParamter> parameterList);
    }
}
