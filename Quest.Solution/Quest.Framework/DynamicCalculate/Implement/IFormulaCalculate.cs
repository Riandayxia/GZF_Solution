using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuHui.Framework.DynamicCalculate.Service;

namespace SuHui.Framework.DynamicCalculate.Implement
{
    public interface IFormulaCalculate
    {
        IList<CParamter> ParamList
        {
            get;
        }

        /// <summary>
        /// 公式计算
        /// </summary>
        /// <returns></returns>
        object Calculate();
    }
}
