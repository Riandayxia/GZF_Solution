using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;

namespace SuHui.WebSite
{
    /// <summary>
    /// C#源码操作，主要包括动态生成dll，动态加载dll
    /// </summary>
    public class SourceOperating
    {
        public static Assembly assembly { get; private set; }

        public static void SourceCompiler()
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider cscp = new CSharpCodeProvider();

            // 2.ICodeComplier
            ICodeCompiler objICodeCompiler = cscp.CreateCompiler();

            // 3.CompilerParameters
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Runtime.Serialization.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.Composition.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath + @"\SuHui.Framework.dll");
            cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath + @"\EntityFramework.dll");
            cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath + @"\EntityFramework.SqlServer.dll");
            cp.GenerateExecutable = false;//设置是dll还是exe
            cp.GenerateInMemory = false;//是否写入内存
            cp.TreatWarningsAsErrors = false;//不将编译警告作为错误
            cp.CompilerOptions = "/optimize";
            string asmname = String.Format(@"{0}\{1}.dll",
                AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,
                "HelloWorld");

            cp.OutputAssembly = asmname;//设置输出的程序集
            // 4.CompilerResults
            String strcontent = GenerateCode();
            String fileName = OutFile(cscp, strcontent);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromFile(cp, fileName);
            if (cr.Errors.Count < 1)
            {
                // 加载
                //Assembly asm = Assembly.Load(asmname);
                //调用
                //dosomething();
            }
        }
        static String GenerateCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.ComponentModel.Composition;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.ComponentModel.DataAnnotations;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Runtime.Serialization;");
            sb.Append(Environment.NewLine);
            sb.Append("using SuHui.Framework;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCodeGenerate");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    [DataContract]");
            sb.Append(Environment.NewLine);
            sb.Append("    [Export(typeof(IEntity))]");
            sb.Append(Environment.NewLine);
            sb.Append("    public class HelloWorld ");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("        [DataMember]");
            sb.Append(Environment.NewLine);
            sb.Append("        [StringLength(128)]");
            sb.Append(Environment.NewLine);
            sb.Append("        public String ZD4 {get; set; }");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("    internal partial class HelloWorldConfiguration : MappingBase<HelloWorld>");
            sb.Append(Environment.NewLine);
            sb.Append("    {}");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        static String OutFile(CodeDomProvider provider, String str)
        {
            String sourceFile;
            //也可以构建该类，生成结构
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            if (provider.FileExtension[0] == '.')
            {
                sourceFile = "HelloWorld" + provider.FileExtension;
            }
            else
            {
                sourceFile = "HelloWorld." + provider.FileExtension;
            }
            sourceFile = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath + @"\" + sourceFile;
            // Create a TextWriter to a StreamWriter to an output file.
            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(sourceFile, false), "    ");
            // Generate source code using the code provider.
            tw.WriteLine(str);
            provider.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());
            // Close the output file.
            tw.Close();

            return sourceFile;
        }
    }
}
