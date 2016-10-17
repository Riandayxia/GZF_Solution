using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;

namespace SuHui.Framework
{
    /// <summary>
    /// C#源码操作，主要包括动态生成dll，动态加载dll
    /// </summary>
    public class SourceOperating
    {
        private static String BinPaht = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
        public static void Initialize()
        {
            ModelCompiler("Model");
            CoreCompiler("Core");
            ControllerCompiler("Controller");
        }
        public static void ControllerCompiler(String fName)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider cscp = new CSharpCodeProvider();

            // 2.CSharpCodeProvider
            CSharpCodeProvider complier = new CSharpCodeProvider();

            // 3.CompilerParameters
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Web.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.Configuration.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.Composition.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Core.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Model.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Newtonsoft.Json.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\System.Web.Helpers.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\System.Web.Mvc.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.WebSite.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.Core.Data.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.Framework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.SqlServer.dll");
            cp.GenerateExecutable = false;//设置是dll还是exe
            cp.GenerateInMemory = false;//是否写入内存
            cp.TreatWarningsAsErrors = false;//不将编译警告作为错误
            cp.CompilerOptions = "/optimize";
            String asmname = String.Format(@"{0}\{1}.dll",
                BinPaht,
                fName);
            if (System.IO.File.Exists(Path.GetFullPath(asmname)))
            {
                File.Delete(Path.GetFullPath(asmname));  
            }
            cp.OutputAssembly = asmname;//设置输出的程序集
            // 4.CompilerResults
            String strcontent = GenerateControllerCode();
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }
        public static void CoreCompiler(String fName)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider cscp = new CSharpCodeProvider();

            // 2.CSharpCodeProvider
            CSharpCodeProvider complier = new CSharpCodeProvider();

            // 3.CompilerParameters
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.Composition.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Model.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.Core.Data.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.Framework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.SqlServer.dll");
            cp.GenerateExecutable = false;//设置是dll还是exe
            cp.GenerateInMemory = false;//是否写入内存
            cp.TreatWarningsAsErrors = false;//不将编译警告作为错误
            cp.CompilerOptions = "/optimize";
            String asmname = String.Format(@"{0}\{1}.dll",
                BinPaht,
                fName);

            if (System.IO.File.Exists(Path.GetFullPath(asmname)))
            {
                File.Delete(Path.GetFullPath(asmname));
            }
            cp.OutputAssembly = asmname;//设置输出的程序集
            // 4.CompilerResults
            String strcontent = GenerateCoreCode();
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }
        public static void ModelCompiler(String fName)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider cscp = new CSharpCodeProvider();

            // 2.CSharpCodeProvider
            CSharpCodeProvider complier = new CSharpCodeProvider();

            // 3.CompilerParameters
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Runtime.Serialization.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.Composition.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\SuHui.Framework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.SqlServer.dll");
            cp.GenerateExecutable = false;//设置是dll还是exe
            cp.GenerateInMemory = false;//是否写入内存
            cp.TreatWarningsAsErrors = false;//不将编译警告作为错误
            cp.CompilerOptions = "/optimize";
            String asmname = String.Format(@"{0}\{1}.dll",
                BinPaht,
                fName);

            if (System.IO.File.Exists(Path.GetFullPath(asmname)))
            {
                File.Delete(Path.GetFullPath(asmname));
            }
            cp.OutputAssembly = asmname;//设置输出的程序集
            // 4.CompilerResults
            String strcontent = GenerateModelCode();
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }
        static String GenerateControllerCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Linq;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Web.Mvc;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Collections.Generic;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.ComponentModel.Composition;");
            sb.Append(Environment.NewLine);
            sb.Append("using SuHui.Framework;");
            sb.Append(Environment.NewLine);
            sb.Append("using DynamicCodeGenerate.Core;");
            sb.Append(Environment.NewLine);
            sb.Append("using DynamicCodeGenerate.Model;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace SuHui.Auto.Application.Controllers");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    [Export]");
            sb.Append(Environment.NewLine);
            sb.Append("    public class HelloWorldController : BaseController");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("         [Import]");
            sb.Append(Environment.NewLine);
            sb.Append("         public IHelloWorldService HelloWorldService { get; set; }");
            sb.Append(Environment.NewLine);
            sb.Append("         public ActionResult Index()");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);
            sb.Append("             return View();");
            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            // 添加
            sb.Append("         public virtual ActionResult Add(HelloWorld entity)");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);
            sb.Append("             OperationResult or = HelloWorldService.Insert(entity);");
            sb.Append(Environment.NewLine);
            sb.Append("             return this.JsonFormat(or);");
            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            // 修改
            sb.Append("         public virtual ActionResult Update(HelloWorld entity)");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);
            sb.Append("             OperationResult or = HelloWorldService.Update(entity);");
            sb.Append(Environment.NewLine);
            sb.Append("             return this.JsonFormat(or);");
            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            // 删除
            sb.Append("         public virtual ActionResult Delete()");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);
            sb.Append(@"            IList<Guid> ids = SuHuiRequest.GetGuids(""ids"");");
            sb.Append(Environment.NewLine);
            sb.Append("             OperationResult or = HelloWorldService.Delete(c => ids.Contains(c.Id));");
            sb.Append(Environment.NewLine);
            sb.Append("             return this.JsonFormat(or);");
            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            // 获取所有信息
            sb.Append("         public ActionResult GetAll()");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);
            sb.Append("             IQueryable<HelloWorld> items = HelloWorldService.Entities;");
            sb.Append(Environment.NewLine);
            sb.Append("             OperationResult or = new OperationResult(OperationResultType.Success, String.Empty, items);");
            sb.Append(Environment.NewLine);
            sb.Append("             return this.JsonFormat(or);");
            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }
        static String GenerateCoreCode()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("using SuHui.Core.Data;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.ComponentModel.Composition;");
            sb.Append(Environment.NewLine);
            sb.Append("using DynamicCodeGenerate.Model;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCodeGenerate.Core");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    [Export(typeof(IHelloWorldService))]");
            sb.Append(Environment.NewLine);
            sb.Append("    internal partial class HelloWorldService: RepositoryBase<HelloWorld,Guid>, IHelloWorldService");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("    public partial interface IHelloWorldService:IRepository<HelloWorld,Guid>");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }
        static String GenerateModelCode()
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
            sb.Append("namespace DynamicCodeGenerate.Model");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    [DataContract]");
            sb.Append(Environment.NewLine);
            sb.Append("    [Export(typeof(IEntity))]");
            sb.Append(Environment.NewLine);
            sb.Append("    public class HelloWorld  : BaseEntity");
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

        static String OutFile(CodeDomProvider provider, String str, String fName)
        {
            String sourceFile;
            //也可以构建该类，生成结构
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            if (provider.FileExtension[0] == '.')
            {
                sourceFile = fName + "." + provider.FileExtension;
            }
            else
            {
                sourceFile = fName + "." + provider.FileExtension;
            }
            String modelsDir = BinPaht + @"\Codes\";
            if (!System.IO.Directory.Exists(modelsDir))
            {
                //文件夹不存在则创建该文件夹  
                System.IO.Directory.CreateDirectory(modelsDir);
            }
            sourceFile = modelsDir + sourceFile;
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
