using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace SuHui.Framework.DynamicCalculate.Service
{
    class FormulaCodeParse : IFormulaCodeParse
    {
        #region IFormulaCodeParse Members

        private string _RegexParamFormulaNameString = @"(\[(?<pref>\d+)\]!)?\[(?<body>(.*?))\]";
        public string RegexParamFormulaNameString
        {
            get { return this._RegexParamFormulaNameString; }
        }

        public string _RegexParamNameString = @"(\[\d+\]!)?\[(.*?)\]";
        public string RegexParamNameString
        {
            get { return this._RegexParamNameString; }
        }

        private StringCollection _CompilerAssemblys;
        public StringCollection CompilerAssemblys
        {
            get
            {
                return this._CompilerAssemblys;
            }
            set
            {
                this._CompilerAssemblys = value;
            }
        }

        public string GetCompilerCode(string code, IList<CParamter> parameterList)
        {
            ArrayList parameters = new ArrayList();
            parameters.Clear();
            int len = code.Length;
            string compilerCode = "";
            string parameter2, parameterName;
            string parentSign;

            int pos2 = 0;
            int pos1 = 0;
            int pos = 0;
            int pos3 = 0;
            int count = 0;

            while (true)
            {
                pos1 = code.IndexOf("[", pos);
                if (pos1 < 0) break;
                pos2 = code.IndexOf("]", pos1 + 1);
                if (pos2 < 0) break;
                pos3 = pos2;
                parameterName = "[" + code.Substring(pos1 + 1, pos2 - pos1 - 1) + "]";

                if ((pos2 + 3) < len)
                {
                    parentSign = code.Substring(pos2 + 1, 2);
                    if (parentSign == "![")
                    {
                        pos2 = pos2 + 3;
                        pos3 = code.IndexOf("]", pos2);
                        if (pos2 < 0) break;
                        parameter2 = code.Substring(pos2, pos3 - pos2);
                        parameterName += "![" + code.Substring(pos2, pos3 - pos2) + "]";

                    }
                }
                int pIndex = parameters.IndexOf(parameterName);

                if (pIndex < 0)
                {
                    pIndex = count;
                    parameters.Add(parameterName);
                    ++count;
                }

                DataTypeEnum typeEnum = parameterList[pIndex].PType;
                // 判断类型
                switch (typeEnum)
                {
                    //整数
                    case DataTypeEnum.Interger:
                        compilerCode = compilerCode + code.Substring(pos, pos1 - pos) + "Convert.ToInt64(_ParamList[" + pIndex.ToString() + "].PValue)";
                        break;
                    //字符
                    case DataTypeEnum.String:
                        compilerCode = compilerCode + code.Substring(pos, pos1 - pos) + "Convert.ToString(_ParamList[" + pIndex.ToString() + "].PValue)";
                        break;
                    default:
                        compilerCode = compilerCode + code.Substring(pos, pos1 - pos) + "Convert.ToDouble(_ParamList[" + pIndex.ToString() + "].PValue)";
                        break;

                }

                pos = pos3 + 1;
            }

            compilerCode = compilerCode + code.Substring(pos);
            return compilerCode;
        }

        #endregion
    }
}
