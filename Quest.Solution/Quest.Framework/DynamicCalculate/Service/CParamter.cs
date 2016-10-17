using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuHui.Framework.DynamicCalculate.Implement;

namespace SuHui.Framework.DynamicCalculate.Service
{
    /// <summary>
    /// 待计算的参数
    /// </summary>
    public class CParamter
    {
        #region 私有属性

        /// <summary>
        /// 参数值
        /// </summary>
        private object _PValue;

        /// <summary>
        /// 是否已经调用公式
        /// </summary>
        private bool _IsCalculate = false;

        /// <summary>
        /// 公式对象
        /// </summary>
        private IFormulaCalculate _Formula;

        #endregion

        #region 共有属性

        /// <summary>
        /// 参数的KEY
        /// </summary>
        public string Key;

        /// <summary>
        /// 参数值
        /// </summary>
        public virtual object PValue
        {
            get
            {
                if (this._Formula != null)
                {
                    //没有调用公式的时候
                    if (!this._IsCalculate)
                    {
                        this._IsCalculate = true;
                        this._PValue = this._Formula.Calculate();
                    }
                }

                return this._PValue;
            }
            set
            {
                this._PValue = value;
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum PType;

        /// <summary>
        /// 计算公式
        /// </summary>
        public String FormulaStr;

        /// <summary>
        /// 引用的参数集合
        /// </summary>
        public IList<CParamter> RefParams = new List<CParamter>();

        /// <summary>
        /// 公式对象
        /// </summary>
        public IFormulaCalculate Formula
        {
            get 
            {
                return this._Formula;
            }
            set 
            {
                this._Formula = value;
            }
        }

        #endregion
    }
}
