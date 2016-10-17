using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;
using SuHui.Framework.DynamicCalculate.Service;

namespace SuHui.Framework.DynamicCalculate.Implement
{
    internal class CalculateCodeGenerate
    {
        #region 属性

        /// <summary>
        /// C#代码编译器
        /// </summary>
        private static CodeDomProvider cDom = CodeDomProvider.CreateProvider("C#");

        #endregion

        /// <summary>
        /// 动态编译计算的代码
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="paramters"></param>
        internal static CompilerResults CompilerCalculateCode(string templateName, IList<CParamter> paramters, IFormulaCodeParse expendParse)
        {
            //设置编译参数
            CompilerParameters mCompilerParams = new CompilerParameters();
            mCompilerParams.GenerateInMemory = true;
            //mCompilerParams.OutputAssembly = "DynamicCalculateClass";
            mCompilerParams.Evidence = AppDomain.CurrentDomain.Evidence;
            mCompilerParams.CompilerOptions = "/optimize";
            mCompilerParams.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/System.dll");
            mCompilerParams.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/System.Data.dll");
            mCompilerParams.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/System.Xml.dll");
            mCompilerParams.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/SuHui.Framework.dll");

            //有扩展的程序集
            if (expendParse.CompilerAssemblys != null)
            {
                foreach (string assemblyName in expendParse.CompilerAssemblys)
                {
                    mCompilerParams.ReferencedAssemblies.Add(assemblyName);
                }
            }

            mCompilerParams.GenerateExecutable = false;

            CodeCompileUnit templateUnit = BuildCodeUnit(templateName, paramters, expendParse);

            //动态编译模板类
            CompilerResults templateRet = cDom.CompileAssemblyFromDom(mCompilerParams, templateUnit);

            //编译失败
            if (templateRet.Errors.HasErrors)
            {
                throw new CalculateCompilerException(templateRet.Errors);
            }
            else
            {
                return templateRet;
            }
        }

        #region 私有方法

        /// <summary>
        /// 构造代码
        /// </summary>
        /// <returns></returns>
        private static CodeCompileUnit BuildCodeUnit(string templateName, IList<CParamter> paramters, IFormulaCodeParse expendParse)
        {
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(BuildFormulaNamespace(templateName, paramters, expendParse));
            codeUnit.Namespaces.Add(BuildTemplateNamespace(templateName, paramters, expendParse));

            return codeUnit;
        }

        /// <summary>
        /// 构造公式的名称空间树
        /// </summary>
        /// <returns></returns>
        private static CodeNamespace BuildFormulaNamespace(string templateName, IList<CParamter> paramters, IFormulaCodeParse expendParse)
        {
            CodeNamespace nameSpace = new CodeNamespace(CommonHelper.FormulaNameSpace);
            AddImportNamespaces(nameSpace);

            string fieldStr = "_ParamList";
            string propertyStr = "ParamList";
            string paramStr = "paramList";
            string methodStr = "Calculate";

            foreach (CParamter par in paramters)
            {
                //参数中存在公式
                if (par.FormulaStr != null && par.FormulaStr.Trim().Length > 0)
                {
                    //定义类
                    CodeTypeDeclaration mClass = new CodeTypeDeclaration(CommonHelper.GetFormulaClassStr(templateName, par.Key, expendParse));
                    mClass.BaseTypes.Add(typeof(IFormulaCalculate));

                    //构造函数
                    CodeConstructor mConstruct = new CodeConstructor();
                    mConstruct.Attributes = MemberAttributes.Public;
                    mConstruct.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IList<CParamter>), paramStr));
                    string conCodeStr = string.Format(@"this.{0} = {1}", fieldStr, paramStr);
                    mConstruct.Statements.Add(new CodeSnippetExpression(conCodeStr));

                    //定义字段
                    CodeMemberField paramList = new CodeMemberField();
                    paramList.Name = fieldStr;
                    paramList.Type = new CodeTypeReference(typeof(IList<CParamter>));
                    paramList.Attributes = MemberAttributes.Private;

                    //定义属性
                    CodeMemberProperty paramListProt = new CodeMemberProperty();
                    paramListProt.Name = propertyStr;
                    paramListProt.Type = new CodeTypeReference(typeof(IList<CParamter>));
                    paramListProt.Attributes = MemberAttributes.Public;
                    paramListProt.GetStatements.Add(new CodeSnippetExpression(string.Format("return this.{0}", fieldStr)));

                    //计算方法
                    CodeMemberMethod calMethod = new CodeMemberMethod();
                    calMethod.Name = methodStr;
                    calMethod.Attributes = MemberAttributes.Public;
                    calMethod.ReturnType = new CodeTypeReference(typeof(Object));

                    //解析公式
                    string strCode = expendParse.GetCompilerCode(par.FormulaStr, par.RefParams);

                    CodeSnippetExpression codeExpress = new CodeSnippetExpression(strCode);
                    calMethod.Statements.Add(codeExpress);

                    mClass.Members.Add(paramListProt);
                    mClass.Members.Add(mConstruct);
                    mClass.Members.Add(paramList);
                    mClass.Members.Add(calMethod);

                    nameSpace.Types.Add(mClass);
                }
            }

            return nameSpace;
        }

        /// <summary>
        /// 构造模板类代码的DOM
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="paramters">模板中的所有参数</param>
        /// <returns></returns>
        private static CodeNamespace BuildTemplateNamespace(string templateName, IList<CParamter> paramters, IFormulaCodeParse expendParse)
        {
            //添加名称空间
            CodeNamespace nameSpace = new CodeNamespace(CommonHelper.TemplateNameSpace);
            AddImportNamespaces(nameSpace);

            //定义类
            CodeTypeDeclaration mClass = new CodeTypeDeclaration(CommonHelper.GetTemplateClassStr(templateName));
            mClass.BaseTypes.Add(typeof(BaseCTemplate));

            //构造函数
            CodeConstructor mConstruct = new CodeConstructor();
            mConstruct.Attributes = MemberAttributes.Public;
            mConstruct.Parameters.Add(new CodeParameterDeclarationExpression(typeof(String), "name"));
            mConstruct.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IList<CParamter>), "paramters"));
            mConstruct.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("name"));

            //构造函数内的代码
            StringBuilder strBuild = new StringBuilder();
            int i = 0;
            foreach (CParamter par in paramters)
            {
                string paramStr = CommonHelper.GetParamterStr(par.Key, expendParse);

                string code =
                    string.Format(
                        @"SuHui.Framework.DynamicCalculate.Implement.IFormulaCalculate formula{0} = new {2}(paramters[{1}].RefParams);
									paramters[{1}].Formula = formula{0};
									this._Paramters.Add(paramters[{1}]);",
                        paramStr, i, CommonHelper.GetFormulaClassStr(templateName, par.Key, expendParse));

                i++;
                strBuild.Append(code);
            }
            CodeSnippetExpression mExpress = new CodeSnippetExpression(strBuild.ToString());
            mConstruct.Statements.Add(mExpress);
            mClass.Members.Add(mConstruct);

            //添加类
            nameSpace.Types.Add(mClass);

            return nameSpace;
        }

        /// <summary>
        /// 引入名称空间
        /// </summary>
        /// <param name="codeNamespace"></param>
        private static void AddImportNamespaces(CodeNamespace codeNamespace)
        {
            //引用的名称空间
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("SuHui.Framework.DynamicCalculate.Implement"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("SuHui.Framework.DynamicCalculate.Service"));
            codeNamespace.Imports.Add(new CodeNamespaceImport(CommonHelper.FormulaNameSpace));
            codeNamespace.Imports.Add(new CodeNamespaceImport(CommonHelper.TemplateNameSpace));
        }

        #endregion
    }
}
