using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SuHui.Framework.DynamicCalculate.Service;

namespace SuHui.Framework.DynamicCalculate.Implement
{
    /// <summary>
    /// CommonHelper 的摘要说明。
    /// </summary>
    internal class CommonHelper
    {
        #region 获取命名规则

        internal static string FormulaNameSpace = "DynamicCalculate.Formula";
        internal static string TemplateNameSpace = "DynamicCalculate.Template";
        internal static string FormulaClass = "Formula";
        internal static string TemplateClass = "Template";

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        internal static string GetParamterStr(string paramName, IFormulaCodeParse formulaParse)
        {
            Regex reg = new Regex(formulaParse.RegexParamFormulaNameString);
            
            Match m = reg.Match(paramName);

            return
                String.Format("C_{0}_{1}", m.Groups["pref"].Value, m.Groups["body"].Value);
        }

        /// <summary>
        /// 获取公式的类名
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        internal static string GetFormulaClassStr(string templateName, string paramName, IFormulaCodeParse formulaParse)
        {
            Regex reg = new Regex(formulaParse.RegexParamFormulaNameString);

            Match m = reg.Match(paramName);

            return
                String.Format("C_{0}_{1}_{2}_{3}", templateName, FormulaClass, m.Groups["pref"].Value,
                              m.Groups["body"].Value);
        }

        /// <summary>
        /// 获取模板的类名
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        internal static string GetTemplateClassStr(string className)
        {
            return string.Format("C_{0}", className);
        }

        #endregion

        /// <summary>
        /// 预处理参数
        /// </summary>
        /// <param name="paraters"></param>
        /// <returns></returns>
        internal static void DealRefParamList(ref IList<CParamter> paraters, IFormulaCodeParse formulaParse)
        {
            IList<CParamter> arrErrorParams = new List<CParamter>();

            Regex reg = new Regex(formulaParse.RegexParamNameString);

            //加载HS集合
            Hashtable hs = new Hashtable();
            foreach (CParamter item in paraters)
            {
                if (!hs.ContainsKey(item.Key))
                {
                    hs.Add(item.Key, item);
                }
            }

            foreach (DictionaryEntry h in hs)
            {
                CParamter item = (CParamter)h.Value;

                //有公式的时候
                if (item.FormulaStr != null && item.FormulaStr.Trim().Length > 0)
                {
                    MatchCollection col = reg.Matches(item.FormulaStr);

                    foreach (Match match in col)
                    {
                        //参数存在
                        if (hs.ContainsKey(match.Value))
                        {
                            item.RefParams.Add((CParamter)hs[match.Value]);
                        }
                        else
                        {
                            arrErrorParams.Add((CParamter)hs[match.Value]);
                        }
                    }
                }
            }

            //缺少参数
            if (arrErrorParams.Count > 0)
            {
                throw new CParamterException(arrErrorParams);
            }
        }
    }
}
