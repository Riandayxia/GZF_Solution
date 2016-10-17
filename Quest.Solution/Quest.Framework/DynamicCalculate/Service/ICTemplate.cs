using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.DynamicCalculate.Service
{
    public interface ICTemplate
    {
        /// <summary>
        /// 计算参数
        /// </summary>
        /// <param name="paramters"></param>
        void CalcaluteParamters(ref IList<CParamter> paramters);
    }
}
