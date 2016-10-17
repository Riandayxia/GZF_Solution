using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using SuHui.Framework.DynamicCalculate.Implement;

namespace SuHui.Framework.DynamicCalculate.Service
{
    public class CTemplateFactory
    {
        /// <summary>
        /// 根据模板名称构造模板对象
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static ICTemplate BuildTemplate(string templateKey, IList<CParamter> paramters)
        {
            //动态编译
            IFormulaCodeParse expendParse = new FormulaCodeParse();

            return BuildTemplateByDefinedParse(templateKey, paramters, expendParse);
        }

        /// <summary>
        /// 根据模板名称构造模板对象
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static ICTemplate BuildTemplateByDefinedParse(string templateKey, IList<CParamter> paramters, IFormulaCodeParse expendParse)
        {
            #region 参数检测

            //无参数的时候
            if (templateKey == null || templateKey.Trim().Length == 0 ||
                paramters == null || paramters.Count == 0 || expendParse == null)
            {
                return null;
            }

            bool isHasFormula = false;
            Hashtable hsNames = new Hashtable();
            IList<CParamter> repeatNames = new List<CParamter>();
            foreach (CParamter obj in paramters)
            {
                //参数中存在公式
                if (obj.FormulaStr != null && obj.FormulaStr.Trim().Length > 0)
                {
                    isHasFormula = true;
                    break;
                }

                //存在参数重名
                if (hsNames.ContainsKey(obj.Key))
                {
                    repeatNames.Add(obj);
                }
                hsNames.Add(obj.Key, null);
            }

            //抛出重名的异常信息
            if (repeatNames.Count > 0)
            {
                throw new CParamterException(repeatNames);
            }

            //无公式的时候
            if (!isHasFormula)
            {
                return null;
            }

            #endregion

            CommonHelper.DealRefParamList(ref paramters, expendParse);

            //动态编译
            CompilerResults com = CalculateCodeGenerate.CompilerCalculateCode(templateKey, paramters, expendParse);

            //获取模板对象
            string className = CommonHelper.GetTemplateClassStr(templateKey);
            ICTemplate temp = (ICTemplate)com.CompiledAssembly.CreateInstance(CommonHelper.TemplateNameSpace + "." + className,
                                                                             true, BindingFlags.CreateInstance, null,
                                                                             new object[] { templateKey, paramters }, null, null);
            return temp;
        }
    }
}
