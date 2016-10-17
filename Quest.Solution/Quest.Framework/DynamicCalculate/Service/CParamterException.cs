using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.DynamicCalculate.Service
{
    public class CParamterException : Exception
    {
        /// <summary>
		/// 错误的参数对象
		/// </summary>
        public IList<CParamter> ErrorParamters;
		
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorCollection"></param>
        internal CParamterException(IList<CParamter> errorCollection)
		{
			
		}
    }
}
