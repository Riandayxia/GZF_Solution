using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuHui.Framework.DynamicCalculate.Service;

namespace SuHui.Framework.DynamicCalculate.Implement
{
    public abstract class BaseCTemplate : ICTemplate
    {
        /// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name"></param>
        public BaseCTemplate(string key)
		{
            this._Key = key;
		}

        /// <summary>
        /// 参数名称
        /// </summary>
        private string _Key;
        public string Key
        {
            get
            {
                return this._Key;
            }
        }

        /// <summary>
        /// 参数集合
        /// </summary>
        protected IList<CParamter> _Paramters = new List<CParamter>();

        #region ICTemplate Members

        public void CalcaluteParamters(ref IList<CParamter> paramters)
        {
            IList<CParamter> errorArr = new List<CParamter>();

            Hashtable hsParams = new Hashtable();
            foreach (object item in this._Paramters)
            {
                CParamter pObj = (CParamter)item;
                hsParams.Add(pObj.Key, pObj);
            }

            //赋值
            foreach (CParamter item in paramters)
            {
                CParamter pObj = (CParamter)item;

                //内部参数对象赋值
                if (hsParams.ContainsKey(pObj.Key))
                {
                    item.PValue = pObj.PValue;
                }
                else
                {
                    errorArr.Add(pObj);
                }
            }

            //有不存在的参数
            if (errorArr.Count > 0)
            {
                throw new CParamterException(errorArr);
            }

            foreach (CParamter item in paramters)
            {
                CParamter param = (CParamter)hsParams[item.Key];
                item.PValue = param.PValue;
            }
        }

        #endregion
    }
}
