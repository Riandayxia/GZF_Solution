using SuHui.Core.Data.Repositories.BPM;
using SuHui.Core.Data.Repositories.HRManagement;
using SuHui.Core.Models.BPM;
using SuHui.Core.Models.HRManagement;
using SuHui.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace SuHui.Core.WF
{
    [Export]
    public class WorkflowApplication
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
        /// 获取或设置 流程信息 数据访问对象
        /// </summary>
        [Import]
        protected IWFDesignRepository WFDesignRepository { get; set; }

        /// <summary>
        /// 获取或设置 流程信息 数据访问对象
        /// </summary>
        [Import]
        protected IUserRepository UserRepository { get; set; }

        /// <summary>
        /// 获取或设置 流程运行步骤 数据访问对象
        /// </summary>
        [Import]
        protected IWFRunStepRepository WFRunStepRepository { get; set; }

        /// <summary>
        /// 获取或设置 流程运行 数据访问对象
        /// </summary>
        [Import]
        protected IWFRunInstanceRepository WFRunInstanceRepository { get; set; }


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
                msg = "流程启动者未空.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            // 声明流程实例
            WFInfo wfInfo = new WFInfo();
            // 获取当前流程运行实例
            List<WFRunInstance> instances = WFRunInstanceRepository.Entities.Where(c => c.MainId == this.MainId).ToList();
            WFRunInstance runInstance = instances.FirstOrDefault();
            // 如果流程实例不存在,这创建流程实例
            if (runInstance.IsNullOrEmpty())
            {
                WFDesign design = WFDesignRepository.GetByKey(this.DesignId);
                // 实例当前流程设计
                wfInfo = this.GetWFInstall(design.DesignJSON);
                runInstance = this.CreateInstance(design.DesignJSON, this.MainId, this.NowUser.Id);
            }
            else
            {
                // 实例当前流程设计
                wfInfo = this.GetWFInstall(runInstance.DesignJSON);
            }

            // 获取指定实体对应运行步骤信息
            List<WFRunStep> rSteps = WFRunStepRepository.Entities.Where(c => c.InstanceId == runInstance.Id).OrderByDescending(c => c.Sort).ToList();
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
                    Name = step.Name,
                    FormUrl=step.FormUrl,
                    InstanceId = runInstance.Id,
                    SenderId = this.NowUser.Id,
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
                    SenderId = NowUser.Id,
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
            if (WFRunStepRepository.AddOrUpdate(c => new { c.Id }, rSteps.ToArray()).Equals(0))
            {
                or = new OperationResult(OperationResultType.QueryNull, "流程启动失败", false);
            }
            else
            {
                or = new OperationResult(OperationResultType.Success, "流程启动成功", true);
                if (!WFComplete.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                    WFComplete(args);
                }
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
            List<WFRunInstance> instances = WFRunInstanceRepository.Entities.Where(c => c.MainId == MainId).ToList();
            WFRunInstance runInstance = instances.FirstOrDefault();
            if (runInstance.IsNullOrEmpty())
            {
                msg = "该流程不存在!";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }

            // 当前流程实例对应步骤信息
            List<WFRunStep> rSteps = WFRunStepRepository.Entities.Where(c => c.InstanceId == runInstance.Id).OrderByDescending(c => c.Sort).ToList();

            // 上去步骤
            WFRunStep rStep = rSteps.FirstOrDefault(c => c.SId == stepId && c.Status == 0);
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
                    InstanceId = runInstance.Id,
                    SenderId = NowUser.Id,
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
            if (WFRunStepRepository.AddOrUpdate(c => new { c.Id }, rSteps.ToArray()).Equals(0))
            {
                or = new OperationResult(OperationResultType.QueryNull, "任务处理失败", false);
            }
            else
            {
                or = new OperationResult(OperationResultType.Success, "任务处理完成", true);
                if (!WFComplete.IsNullOrEmpty())
                {
                    WFTaskArgs args = new WFTaskArgs(rStep, wfInfo, runInstance, NowUser);
                    WFComplete(args);
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
                Id = Guid.NewGuid(),
                MainId = mainId,
                UserId = userId,
                DesignJSON = dJson
            };
            WFRunInstanceRepository.Insert(runInstance);

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
