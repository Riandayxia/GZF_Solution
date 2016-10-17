using Quest.Core.BPM;
using Quest.Core.Models.Base;
using Quest.Core.Models.BPM;
using Quest.Framework;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Process
{
    [Export]
    internal partial class WorkflowApplication
    {
        #region 实例
        /// <summary>
        /// 实例化流程应用
        /// </summary>
        public WorkflowApplication()
        {
            #region 注册MEF
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            var thisAssembly = new DirectoryCatalog(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll");
            aggregateCatalog.Catalogs.Add(thisAssembly);
            CompositionContainer container = new CompositionContainer(aggregateCatalog);
            container.ComposeParts(this);
            #endregion
        }

        /// <summary>
        /// 实例化流程应用
        /// </summary>
        /// <param name="mainId">主体Id</param>
        /// <param name="designId">设计Id</param>
        /// <param name="user">当前用户</param>
        public WorkflowApplication(String mainId, Guid designId, User user)
            : this()
        {
            this.MainId = mainId;
            this.DesignId = designId;
            this.NowUser = user;
        }

        /// <summary>
        /// 实例化流程应用
        /// </summary>
        /// <param name="user">主体Id</param>
        /// <param name="user">当前用户</param>
        public WorkflowApplication(String mainId, User user)
            : this()
        {
            this.MainId = mainId;
            this.NowUser = user;
        }
        #endregion

        #region 受保护的属性

        /// <summary>
        /// 获取或设置 流程模型 数据访问对象
        /// </summary>
        [Import]
        protected IWFModelService WFModelService { get; set; }

        /// <summary>
        /// 获取或设置 流程运行步骤 数据访问对象
        /// </summary>
        [Import]
        protected IWFRunStepService WFRunStepService { get; set; }

        /// <summary>
        /// 获取或设置 流程运行 数据访问对象
        /// </summary>
        [Import]
        protected IWFRunInstanceService WFRunInstanceService { get; set; }

        #endregion

        #region 属性

        /// <summary>
        /// 创建步骤
        /// </summary>
        public Action<WFTaskArgs> CreateStep { get; set; }

        /// <summary>
        /// 流程加载完成
        /// </summary>
        public Action<WFTaskArgs> WFComplete { get; set; }

        /// <summary>
        /// 主体id
        /// </summary>
        public String MainId { get; set; }

        /// <summary>
        /// 流程设计Id
        /// </summary>
        public Guid DesignId { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public User NowUser { get; set; }

        #endregion

        #region 公共方法

        /// <summary>
        /// 根据指定参数启动流程 
        /// 根据指定步骤Id查询数据为空，则创建一个新流程
        /// </summary>
        /// <returns>返回操作结果</returns>
        public OperationResult Execute()
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            String msg = String.Empty;

            if (this.DesignId.IsNullOrEmpty())
            {
                msg = "流程设计Id为空.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            if (this.MainId.IsNullOrEmpty())
            {
                msg = "流程无主题依赖.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            if (this.NowUser.IsNullOrEmpty())
            {
                msg = "流程启动者不能为空.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            // 声明流程实例
            WFInfo wfInfo = new WFInfo();
            // 获取当前流程运行实例
            List<WFRunInstance> instances = WFRunInstanceService.Entities.Where(c => c.MainId == this.MainId).ToList();
            WFRunInstance runInstance = instances.FirstOrDefault();
            // 如果流程实例不存在,这创建流程实例
            if (runInstance.IsNullOrEmpty())
            {
                or = WFModelService.GetByKey(this.DesignId);
                WFModel design = or.AppendData as WFModel;
                if (!or.IsNullOrEmpty())
                {
                    // 实例当前流程设计
                    wfInfo = this.GetWFInstall(design.DesignJSON);
                    runInstance = this.CreateInstance(design.DesignJSON, this.MainId, this.NowUser.Id);
                }
                else
                {
                    msg = "流程模型未找到！";
                    return new OperationResult(OperationResultType.QueryNull, msg);
                }
            }
            else
            {
                // 实例当前流程设计
                wfInfo = this.GetWFInstall(runInstance.DesignJSON);
            }

            // 获取指定实体对应运行步骤信息
            List<WFRunStep> rSteps = WFRunStepService.Entities.Where(c => c.InstanceId == runInstance.Id).OrderByDescending(c => c.Sort).ToList();
            // 获得当前运行步骤
            WFRunStep rStep = rSteps.FirstOrDefault();
            Boolean isFirst = false;
            // 判断步骤是否为空,如果为空则为第一次启动流程,并创建一个新步骤信息
            if (rStep.IsNullOrEmpty())
            {
                // 流程当前步骤
                Step step = wfInfo.Steps.Where(c => c.Id == wfInfo.FirstStepID).FirstOrDefault();
                rStep = new WFRunStep
                {
                    ParentId = Guid.Empty,
                    SId = step.Id,
                    FormId = step.FormId.GetGuid(),
                    Name = step.Name,
                    FormUrl = step.FormUrl,
                    InstanceId = runInstance.Id,
                    SenderId = this.NowUser.Id,
                    SenderTime = DateTime.Now,
                    Status = 2,
                    Sort = 1
                };
                isFirst = true;
            }

            // 获取当前步骤下一步路线
            List<Line> lines = wfInfo.Lines.Where(c => c.FromID == rStep.SId).ToList();
            // 判断当前步骤是否为空
            if (lines.IsNullOrEmpty() || lines.Count == 0)
            {
                msg = "流程设计中连线存在问题.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }

            // 声明下一步骤默认处理者Id和名称
            String rId = String.Empty;

            foreach (Line line in lines)
            {
                Boolean isTrue = false;
                // 判断连线是否满足规则
                if (!line.Expression.IsNullOrEmpty())
                {
                    isTrue = LineWhere(line.Expression, wfInfo.DBTableName);
                }

                if (!line.TSql.IsNullOrEmpty())
                {
                    //or = WFModelService.Context.
                    SqlParameter s = new SqlParameter("@mainId", SqlDbType.UniqueIdentifier) { Value = "00000000-0000-0000-0000-000000000001".GetGuid() };
                    Int32 result = WFModelService.DB.SqlQuery<Int32>(line.TSql, s).FirstOrDefault();
                    isTrue = result > 0 ? true : false;

                    //if (or.ResultType == OperationResultType.Success)
                    //{
                    //    or.AppendData
                    //    isTrue = true;
                    //}
                }
                //Boolean isTrue = true;
                if (isTrue)
                {
                    // 连线下一步骤
                    Step toStep = wfInfo.Steps.Where(c => c.Id == line.ToID).FirstOrDefault();

                    String id = toStep.DefaultUser;
                    rId = rId.IsNullOrEmpty() ? id : rId + ";" + id;

                    WFRunStep item = new WFRunStep
                    {
                        Id = Guid.NewGuid(),
                        ParentId = rStep.SId,
                        SId = toStep.Id,
                        Name = toStep.Name,
                        FormUrl = toStep.FormUrl,
                        InstanceId = runInstance.Id,
                        FormId = toStep.FormId.GetGuid(),
                        SenderId = NowUser.Id,
                        SenderTime = DateTime.Now,
                        ReceiveId = id,
                        Status = 0,
                        Sort = (rSteps.Count == 0 ? 1 : rSteps.Count) + 1
                    };
                    rSteps.Add(item);
                    //or = this.Insert(item);

                    if (!CreateStep.IsNullOrEmpty())
                    {
                        WFTaskArgs args = new WFTaskArgs(item, wfInfo, runInstance, NowUser);
                        CreateStep(args);
                    }
                }
            }

            // 第一次创建流程
            if (isFirst)
            {
                rStep.ReceiveId = rId;
                rSteps.Add(rStep);

                if (!CreateStep.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                    CreateStep(args);
                }
            }
            or = WFRunStepService.AddOrUpdate(c => new { c.Id }, rSteps);
            if (or.ResultType == OperationResultType.Success)
            {
                or = new OperationResult(OperationResultType.Success, "流程启动成功", true);
                if (!WFComplete.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                    WFComplete(args);
                }
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "流程启动失败", false);
            }

            return or;
        }

        /// <summary>
        /// 流程任务处理
        /// </summary>
        /// <param name="stepId">流程步骤Id</param>
        /// <returns>返回操作结果</returns>
        public OperationResult ActionTask(Guid stepId)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            String msg = String.Empty;

            if (this.NowUser.IsNullOrEmpty())
            {
                msg = "流程启动者未空.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }

            if (this.MainId.IsNullOrEmpty())
            {
                msg = "流程无主题依赖.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            // 获取当前流程运行实例
            List<WFRunInstance> instances = WFRunInstanceService.Entities.Where(c => c.MainId == MainId).ToList();
            WFRunInstance runInstance = instances.FirstOrDefault();
            if (runInstance.IsNullOrEmpty())
            {
                msg = "该流程不存在!";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }

            // 当前流程实例对应步骤信息
            List<WFRunStep> rSteps = WFRunStepService.Entities.Where(c => c.InstanceId == runInstance.Id).OrderByDescending(c => c.Sort).ToList();

            // 上去步骤
            WFRunStep rStep = rSteps.FirstOrDefault(c => c.Id == stepId && c.Status == 0);
            if (rStep.IsNullOrEmpty())
            {
                msg = "该步骤已过期或无效!";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            // 获取当前实例
            WFInfo wfInfo = this.GetWFInstall(runInstance.DesignJSON);
            List<Line> lines = wfInfo.Lines.Where(c => c.FromID == rStep.SId).ToList();

            // 声明下一步骤默认处理者Id和名称
            String rId = String.Empty;
            foreach (Line line in lines)
            {
                Step toStep = wfInfo.Steps.Where(c => c.Id == line.ToID).FirstOrDefault();
                String id = toStep.DefaultUser;
                rId = rId.IsNullOrEmpty() ? id : rId + ";" + id;

                WFRunStep item = new WFRunStep
                {
                    Id = Guid.NewGuid(),
                    ParentId = rStep.Id,
                    SId = toStep.Id,
                    Name = toStep.Name,
                    FormUrl = toStep.FormUrl,
                    FormId = toStep.FormId.GetGuid(),
                    InstanceId = runInstance.Id,
                    SenderId = NowUser.Id,
                    SenderTime = DateTime.Now,
                    ReceiveTime = DateTime.Now,
                    ReceiveId = id,
                    Status = 0,
                    Sort = (rSteps.Count == 0 ? 1 : rSteps.Count) + 1
                };
                rSteps.Add(item);
                if (!CreateStep.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(item, wfInfo, runInstance, NowUser);
                    CreateStep(args);
                }
                //tasks.Add(this.CreateTask(item, user, mainId, wfInfo.Name));
            }
            // 标记当前步骤完成
            rStep.Status = 2;
            rSteps.Add(rStep);

            if (!CreateStep.IsNullOrEmpty())
            {
                WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                CreateStep(args);
            }
            or = WFRunStepService.AddOrUpdate(c => new { c.Id }, rSteps);
            if (or.ResultType == OperationResultType.Success)
            {
                or = new OperationResult(OperationResultType.Success, "任务处理完成", true);
                if (!WFComplete.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                    WFComplete(args);
                }
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "任务处理失败", false);
            }
            return or;
        }

        /// <summary>
        /// 获取指定流程Id的首步骤的表单信息
        /// </summary>
        /// <returns>返回操作结果</returns>
        public OperationResult ProcessFirstStep(Guid wfId)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            or = WFModelService.GetByKey(wfId);
            if (or.ResultType == OperationResultType.Success)
            {
                try
                {
                    WFModel flow = or.AppendData as WFModel;
                    WFInfo wfInfo = this.GetWFInstall(flow.DesignJSON);
                    Step step = wfInfo.Steps.Where(c => c.Id == wfInfo.FirstStepID).FirstOrDefault();
                    or = new OperationResult(OperationResultType.Success, "加载成功", step);
                }
                catch (Exception e)
                {
                    or = new OperationResult(OperationResultType.Error, e.Message);
                }
            }
            return or;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建流程
        /// 第一次启动流程
        /// </summary>
        /// <param name="designId">流程设计Id</param>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns>放回流程实例</returns>
        private WFRunInstance CreateInstance(String dJson, String mainId, Guid userId)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);

            // 获取当前流程运行实例
            WFRunInstance runInstance = new WFRunInstance
            {
                MainId = mainId,
                UserId = userId,
                DesignJSON = dJson
            };
            WFRunInstanceService.Insert(runInstance, false);

            return runInstance;
        }

        /// <summary>
        /// 获取指定流程设计的流程实例
        /// </summary>
        /// <param name="dJson">流程设计Json</param>
        /// <returns>返回流程实例</returns>
        private WFInfo GetWFInstall(String dJson)
        {
            WFInfo install = JsonHelper.DecodeObject<WFInfo>(dJson);
            install.Id = Guid.NewGuid();

            //得到第一步
            List<Guid> firstStepIDList = new List<Guid>();
            foreach (var step in install.Steps)
            {
                if (install.Lines.Where(p => p.ToID == step.Id).Count() == 0)
                {
                    install.FirstStepID = step.Id;
                }
            }

            return install;
        }

        /// <summary>
        /// 判断连线条件
        /// </summary>
        /// <param name="fg">条件表达式</param>
        /// <param name="tName">数据表名称</param>
        /// <returns>返回true、false</returns>
        private Boolean LineWhere(FilterGroup fg, String tName)
        {
            Boolean exist = false;

            String BinPaht = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            //设置需要编译的语言类型
            CodeDomProvider _p = CodeDomProvider.CreateProvider("C#");
            //编译参数对象
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.Composition.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Core.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Model.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Core.Data.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\Quest.Framework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.dll");
            cp.ReferencedAssemblies.Add(BinPaht + @"\EntityFramework.SqlServer.dll");
            //parameter.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/Quest.Framework.dll");
            //parameter.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/Quest.Component.Data.dll");
            //parameter.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/Quest.Core.Data.dll");
            //parameter.ReferencedAssemblies.Add(System.AppDomain.CurrentDomain.RelativeSearchPath + @"/Quest.Core.Models.dll");
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;

            String strCode = GenerateCode(tName, fg);
            // dll编译
            CompilerResults cr = _p.CompileAssemblyFromSource(cp, strCode);

            if (cr.Errors.HasErrors)
            {
                Console.WriteLine("编译错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    Console.WriteLine(err.ErrorText);
                }
            }
            else
            {
                // 通过反射，调用LineService的实例
                Assembly objAssembly = cr.CompiledAssembly;
                dynamic obj = objAssembly.CreateInstance("Quest.Core.WF.LineService");
                exist = obj.Exist() > 0;
            }
            return exist;
        }

        /// <summary>
        /// 获取类型强制转换方法表达式
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>方法表达式</returns>
        private String GetTypeText(String type)
        {
            String str = String.Empty;
            switch (type.ToLower())
            {
                case "guid":
                    str = ".GetGuid()";
                    break;
                case "int32":
                case "int":
                    str = ".GetInt32()";
                    break;
                case "string":
                    str = ".GetString()";
                    break;
                case "double":
                case "decimal":
                    str = ".GetDouble()";
                    break;
                case "datetime":
                    str = ".GetDateTime()";
                    break;
                case "bool":
                case "boolean":
                    str = ".GetBoolean()";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 获得操作符查询文本
        /// </summary>
        /// <param name="op">操作</param>
        /// <returns>操作查询文本</returns>
        private String GetOperatorQueryText(String op)
        {
            switch (op.ToLower())
            {
                case "add":
                    return " + ";
                case "bitwiseand":
                    return " & ";
                case "bitwisenot":
                    return " ~ ";
                case "bitwiseor":
                    return " | ";
                case "bitwisexor":
                    return " ^ ";
                case "divide":
                    return " / ";
                case "equal":
                    return " == ";
                case "greater":
                    return " > ";
                case "greaterorequal":
                    return " >= ";
                case "isnull":
                    return " is null ";
                case "isnotnull":
                    return " is not null ";
                case "less":
                    return " < ";
                case "lessorequal":
                    return " <= ";
                case "like":
                    return " like ";
                case "startwith":
                    return " like ";
                case "endwith":
                    return " like ";
                case "modulo":
                    return " % ";
                case "multiply":
                    return " * ";
                case "notequal":
                    return " <> ";
                case "subtract":
                    return " - ";
                case "and":
                    return " and ";
                case "or":
                    return " or ";
                case "in":
                    return " in ";
                case "notin":
                    return " not in ";
                default:
                    return " = ";
            }
        }

        /// <summary>
        /// 翻译规则
        /// 获取规则Lambad表达式
        /// 该方法为递归
        /// </summary>
        /// <param name="rule">规则对象</param>
        /// <returns>返回规则Lambad表达式</returns>
        private String TranslateRule(FilterRule rule)
        {
            StringBuilder bulider = new StringBuilder();
            // 设置字段
            bulider.Append("c." + rule.Field);

            // 未完成，还有其他操作类型未判断
            //操作符
            if (rule.Op == "like")
            {
                bulider.Append(".Contains(" + rule.Field.ToLower() + ")");
            }
            else
            {
                bulider.Append(GetOperatorQueryText(rule.Op));
                //赋值
                bulider.Append(rule.Field.ToLower());
            }

            return bulider.ToString();
        }

        /// <summary>
        /// 翻译规则
        /// 获取规则Lambad表达式
        /// </summary>
        /// <param name="group">规则数据</param>
        /// <returns>返回规则Lambad表达式</returns>
        private String TranslateGroup(FilterGroup group)
        {
            StringBuilder bulider = new StringBuilder();
            var appended = false;
            if (group.Rules != null)
            {
                foreach (var rule in group.Rules)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.Op));

                    bulider.Append(TranslateRule(rule));
                    appended = true;
                }
            }
            if (group.Groups != null)
            {
                foreach (var subgroup in group.Groups)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.Op));
                    bulider.Append(TranslateGroup(subgroup));
                    appended = true;
                }
            }
            return bulider.ToString();
        }

        /// <summary>
        /// 翻译规则
        /// 获取规则字段声明
        /// </summary>
        /// <param name="group">规则数据</param>
        /// <returns>返回字段声明表达式</returns>
        private String TranslateVariable(FilterGroup group)
        {

            StringBuilder bulider = new StringBuilder();
            if (group.Rules != null)
            {
                foreach (var rule in group.Rules)
                {
                    bulider.Append(rule.Type + " " + rule.Field.ToLower() + " = \"" + rule.Value + "\"" + GetTypeText(rule.Type) + ";");
                    bulider.Append(Environment.NewLine);
                }
            }
            if (group.Groups != null)
            {
                foreach (var subgroup in group.Groups)
                {
                    bulider.Append(TranslateVariable(subgroup));
                }
            }
            return bulider.ToString();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="dbTableName">数据表</param>
        /// <param name="fg">表达式</param>
        /// <returns></returns>
        private String GenerateCode(String dbTableName, FilterGroup fg)
        {

            String vStr = TranslateVariable(fg);
            String str = TranslateGroup(fg);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using System.ComponentModel.Composition.Hosting;");
            sb.AppendLine("using DynamicCodeGenerate.Core;");
            sb.AppendLine("using DynamicCodeGenerate.Model;");
            sb.AppendLine("using Quest.Framework;");
            sb.AppendLine("namespace Quest.Core.WF");
            sb.AppendLine("{");
            sb.AppendLine("    [Export]");
            sb.AppendLine("    public class LineService");
            sb.AppendLine("    {");
            sb.AppendLine("        public LineService()");
            sb.AppendLine("        {");
            sb.AppendLine("             AggregateCatalog aggregateCatalog = new AggregateCatalog();");
            sb.AppendLine("             var thisAssembly = new DirectoryCatalog(AppDomain.CurrentDomain.RelativeSearchPath, \"*.dll\");");
            sb.AppendLine("             aggregateCatalog.Catalogs.Add(thisAssembly);");
            sb.AppendLine("             CompositionContainer container = new CompositionContainer(aggregateCatalog);");
            sb.AppendLine("             container.ComposeParts(this);");
            sb.AppendLine("        }");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("        [Import]");
            sb.AppendLine(String.Format("         public I{0}Service {0}Service {1}", dbTableName, "{ get; set; }"));
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("        public Object Exist()");
            sb.AppendLine("        {");
            sb.AppendLine("             " + vStr);
            sb.AppendLine("             var count=" + dbTableName + "Service.Entities.Count(c=>" + str + ");");
            sb.AppendLine("             return count;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            String code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        #endregion

        #region 对象

        /// <summary>
        /// 任务参数
        /// </summary>
        public class WFTaskArgs
        {
            public WFTaskArgs() { }

            /// <summary>
            /// 实例化任务参数
            /// </summary>
            /// <param name="rStep">运行步骤</param>
            /// <param name="wfInfo">流程实例信息</param>
            /// <param name="runInstance">流程运行实例</param>
            /// <param name="user">当今节点用户</param>
            public WFTaskArgs(WFRunStep rStep, WFInfo wfInfo, WFRunInstance runInstance, User user) :
                this()
            {
                RStep = rStep;
                Info = wfInfo;
                RunInstance = runInstance;
                NowUser = user;
            }

            /// <summary>
            /// 运行步骤
            /// </summary>
            public WFRunStep RStep { get; internal set; }

            /// <summary>
            /// 流程实例信息
            /// </summary>
            public WFInfo Info { get; internal set; }

            /// <summary>
            /// 流程运行实例
            /// </summary>
            public WFRunInstance RunInstance { get; internal set; }

            /// <summary>
            /// 当今节点用户
            /// </summary>
            public User NowUser { get; internal set; }

        }

        #endregion
    }
}
