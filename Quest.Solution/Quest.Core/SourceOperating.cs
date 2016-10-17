using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;
using Quest.Framework;
using Quest.Core.Models.BPM;

namespace Quest.Core
{
    /// <summary>
    /// C#源码操作，主要包括动态生成dll，动态加载dll
    /// </summary>
    public class SourceOperating
    {
        private static String BinPaht = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
        /// <summary>
        /// 初始化自定义表相应的代码
        /// </summary>
        /// <param name="tables">数据表</param>
        /// <param name="columns">数据列</param>
        public static void Initialize(List<CDTable> tables, List<CDColumn> columns)
        {
            ModelCompiler(tables, columns);
            CoreCompiler(tables, columns);
            ControllerCompiler(tables, columns);
        }

        /// <summary>
        /// 编译控制器代码
        /// </summary>
        /// <param name="tables">数据表</param>
        /// <param name="columns">数据列</param>
        public static void ControllerCompiler(List<CDTable> tables, List<CDColumn> columns)
        {
            String fName = "Controller";
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
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.WebSite.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Core.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Core.Data.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Core.Models.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Framework.dll");
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
            String strcontent = GenerateControllerCode(tables, columns);
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }

        /// <summary>
        /// 编译业务核心代码
        /// </summary>
        /// <param name="tables">数据表</param>
        /// <param name="columns">数据列</param>
        public static void CoreCompiler(List<CDTable> tables, List<CDColumn> columns)
        {
            String fName = "Core";
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
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Core.Data.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Framework.dll");
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
            String strcontent = GenerateCoreCode(tables, columns);
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }

        /// <summary>
        /// 编译对象模型代码
        /// </summary>
        /// <param name="tables">数据表</param>
        /// <param name="columns">数据列</param>
        public static void ModelCompiler(List<CDTable> tables, List<CDColumn> columns)
        {
            String fName = "Model";
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
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Framework.dll");
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
            String strcontent = GenerateModelCode(tables, columns);
            String fileName = OutFile(cscp, strcontent, fName);
            //该句表示直接用cs文件中的代码字符生成
            //  CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(cp, GenerateCode());
            //该句表示直接用cs文件
            CompilerResults cr = complier.CompileAssemblyFromFile(cp, fileName);
        }

        /// <summary>
        /// 创建控制器代码
        /// </summary>
        /// <returns>返回字符串</returns>
        static String GenerateControllerCode(List<CDTable> tables, List<CDColumn> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Web.Mvc;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using DynamicCodeGenerate.Core;");
            sb.AppendLine("using DynamicCodeGenerate.Model;");
            sb.AppendLine("using Quest.Framework;");
            sb.AppendLine("using Quest.Core.Models.Base;");
            sb.AppendLine("using Quest.Core.BPM;");
            sb.AppendLine("using Quest.WebSite;");
            sb.AppendLine("namespace Quest.WebSite.Controllers");
            sb.AppendLine("{");
            tables.ForEach(t =>
            {
                sb.AppendLine("    [Export]");
                sb.AppendLine(String.Format("    public class {0}Controller : BaseController", t.Name));
                sb.AppendLine("    {");
                sb.AppendLine("         [Import]");
                sb.AppendLine(String.Format("         public I{0}Service {0}Service {1}", t.Name, "{ get; set; }"));
                sb.AppendLine("         [Import]");
                sb.AppendLine("         public IWFRunInstanceService WFRunInstanceService { get; set; }");
                sb.AppendLine("         public ActionResult Index()");
                sb.AppendLine("         {");
                sb.AppendLine("             return View();");
                sb.AppendLine("         }");
                // 添加
                sb.AppendLine(String.Format("         public virtual ActionResult Add({0} entity)", t.Name));
                sb.AppendLine("         {");
                sb.AppendLine("             OperationResult or = new OperationResult(OperationResultType.Error);");
                sb.AppendLine("             Guid flowId = QuestRequest.GetGuid(\"flowId\");");
                sb.AppendLine("             if (flowId.IsNullOrEmpty())");
                sb.AppendLine("             {");
                sb.AppendLine(String.Format("                 or = {0}Service.Insert(entity);", t.Name));
                sb.AppendLine("             }");
                sb.AppendLine("             else");
                sb.AppendLine("             {");
                sb.AppendLine("                 User user = CurrentUser.GetUser();");
                sb.AppendLine(String.Format("                 or = {0}Service.Insert(entity, false);", t.Name));
                sb.AppendLine("                 or = WFRunInstanceService.Execute(entity.Id.ToString(), flowId, user);");
                sb.AppendLine("             }");
                sb.AppendLine("             return this.JsonFormat(or);");
                sb.AppendLine("         }");
                // 修改
                sb.AppendLine(String.Format("         public virtual ActionResult Update({0} entity)", t.Name));
                sb.AppendLine("         {");
                sb.AppendLine(String.Format("             OperationResult or = {0}Service.Update(entity);", t.Name));
                sb.AppendLine("             return this.JsonFormat(or);");
                sb.AppendLine("         }");
                // 删除
                sb.AppendLine("         public virtual ActionResult Delete()");
                sb.AppendLine("         {");
                sb.AppendLine(@"            IList<Guid> ids = QuestRequest.GetGuids(""ids"");");
                sb.AppendLine(String.Format("             OperationResult or = {0}Service.Delete(c => ids.Contains(c.Id));", t.Name));
                sb.AppendLine("             return this.JsonFormat(or);");
                sb.AppendLine("         }");
                // 获取所有信息
                sb.AppendLine("         public ActionResult GetAll()");
                sb.AppendLine("         {");
                sb.AppendLine(String.Format("             IQueryable<{0}> items = {0}Service.Entities;", t.Name));
                sb.AppendLine("             OperationResult or = new OperationResult(OperationResultType.Success, String.Empty, items);");
                sb.AppendLine("             return this.JsonFormat(or);");
                sb.AppendLine("         }");
                sb.AppendLine("    }");
            });
            sb.AppendLine("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        /// <summary>
        /// 创建业务核心代码
        /// </summary>
        /// <returns>返回字符串</returns>
        static String GenerateCoreCode(List<CDTable> tables, List<CDColumn> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using Quest.Core.Data;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using DynamicCodeGenerate.Model;");
            sb.AppendLine("namespace DynamicCodeGenerate.Core");
            sb.AppendLine("{");
            tables.ForEach(t =>
            {
                sb.AppendLine(String.Format("    [Export(typeof(I{0}Service))]", t.Name));
                sb.AppendLine(String.Format("    internal partial class {0}Service: RepositoryBase<{0},Guid>, I{0}Service", t.Name));
                sb.AppendLine("    {");
                sb.AppendLine("    }");
                sb.AppendLine(String.Format("    public partial interface I{0}Service:IRepository<{0},Guid>", t.Name));
                sb.AppendLine("    {");
                sb.AppendLine("    }");
            });
            sb.AppendLine("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        /// <summary>
        /// 创建对象模型代码
        /// </summary>
        /// <returns>返回对象模型代码</returns>
        static String GenerateModelCode(List<CDTable> tables, List<CDColumn> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.Runtime.Serialization;");
            sb.AppendLine("using Quest.Framework;");
            sb.AppendLine("namespace DynamicCodeGenerate.Model");
            sb.AppendLine("{");
            tables.ForEach(t =>
            {
                sb.AppendLine("    [DataContract]");
                sb.AppendLine("    [Export(typeof(IEntity))]");
                sb.AppendLine(String.Format("    public class {0} : BaseEntity", t.Name));
                sb.AppendLine("    {");
                columns.Where(c => c.TableId == t.Id).OrderBy(c => c.Name).ToList().ForEach(c =>
                {
                    sb.AppendLine("        [DataMember]");
                    sb.AppendLine(String.Format("        public {0} {1} {2}", SqlTypeToCsharpType.SqlTypeString2CsharpTypeString(c.DBType), c.Name, "{ get; set; }"));
                });
                sb.AppendLine("    }");
                sb.AppendLine(String.Format("    internal partial class {0}Configuration : MappingBase<{0}>", t.Name));
                sb.AppendLine("    {}");
            });
            sb.AppendLine("}");
            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        /// <summary>
        /// 输出文件操作
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="str"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
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
