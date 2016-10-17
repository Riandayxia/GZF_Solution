using Quest.Core.Models.Base;
using Quest.Core.Models.BPM;
using Quest.Core.Process;
using Quest.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.BPM.Impl
{
    /// <summary>
    /// 流程运行 核心业务契约
    /// </summary>
    internal partial class WFRunInstanceService
    {
        #region 受保护的属性

        /// <summary>
        /// 获取或设置 任务信息 数据访问对象
        /// </summary>
        [Import]
        protected IWFModelService WFModelService { get; set; }
        /// <summary>
        /// 获取或设置 任务信息 数据访问对象
        /// </summary>
        [Import]
        protected IWFTaskService WFTaskService { get; set; }
        /// <summary>
        /// 获取或设置 表单信息 数据访问对象
        /// </summary>
        [Import]
        protected IWFFormService WFFormService { get; set; }

        /// <summary>
        /// 获取或设置 数据表 数据访问对象
        /// </summary>
        [Import]
        protected ICDTableService DBTableService { get; set; }

        /// <summary>
        /// 任务集合
        /// </summary>
        private List<WFTask> tasks { get; set; }

        #endregion

        #region 公共属性


        #endregion

        #region 公共方法

        /// <summary>
        /// 根据指定参数启动流程 
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="projectId">项目Id</param>
        /// <param name="cName">控制器名称</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        public OperationResult Execute(String mainId, Guid wfModeId, User user)
        {
            String msg = String.Empty;
            OperationResult or = new OperationResult(OperationResultType.Error);
            WFModel design = WFModelService.Entities.Where(c => c.Id == wfModeId).FirstOrDefault();
            if (design.IsNullOrEmpty())
            {
                msg = "流程设计不存在或已过期.";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }

            // 根据指定参数启动流程 
            WorkflowApplication instance = new WorkflowApplication(mainId, design.Id, user);
            // 任务准备方法
            instance.CreateStep = CreateTask;
            instance.WFComplete = Complete;
            tasks = new List<WFTask>();
            or = instance.Execute();
            return or;
        }

        /// <summary>
        /// 获取指定流程Id的首步骤的表单信息
        /// </summary>
        /// <returns>返回操作结果</returns>
        public OperationResult ProcessFirstStep(Guid wfId)
        {
            // 根据指定参数启动流程 
            WorkflowApplication instance = new WorkflowApplication();
            OperationResult or = instance.ProcessFirstStep(wfId);
            if (or.ResultType == OperationResultType.Success)
            {
                Step step = or.AppendData as Step;
                or = WFFormService.GetByKey(step.FormId.GetGuid());
            }
            return or;
        }

        /// <summary>
        /// 流程任务处理
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="task">任务信息</param>
        /// <param name="user">当前用户</param>
        /// <returns>返回操作结果</returns>
        public OperationResult Task(String mainId, WFTask task, User user)
        {
            String msg = String.Empty;
            OperationResult or = new OperationResult(OperationResultType.Error);
            if (task.IsNullOrEmpty())
            {
                msg = "任务信息无效或已过期!";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            // 修改任务信息
            if (WFTaskService.Update(task).Equals(0))
            {
                msg = "任务处理失败!";
                return new OperationResult(OperationResultType.QueryNull, msg);
            }
            return Task(mainId, task.StepId, user);
        }

        /// <summary>
        /// 流程任务处理
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="stepId">流程步骤Id</param>
        /// <param name="user">当前用户</param>
        /// <returns>返回操作结果</returns>
        public OperationResult Task(String mainId, Guid stepId, User user)
        {
            OperationResult or = new OperationResult(OperationResultType.Error);
            WorkflowApplication instance = new WorkflowApplication(mainId, user);
            // 任务准备方法
            instance.CreateStep = CreateTask;
            instance.WFComplete = Complete;
            tasks = new List<WFTask>();
            or = instance.ActionTask(stepId);
            return or;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="rStep">任务参数</param>
        /// <returns>返回任务信息</returns>
        void CreateTask(Quest.Core.Process.WorkflowApplication.WFTaskArgs args)
        {
            WFRunStep rStep = args.RStep;
            User user = args.NowUser;
            WFInfo info = args.Info;
            WFRunInstance ri = args.RunInstance;

            WFTask task = new WFTask
            {
                ParentId = Guid.Empty,
                InstanceId = rStep.InstanceId,
                MainId = ri.MainId,
                StepId = rStep.Id,
                StepName = rStep.Name,
                Title = user.LoginName + "的" + info.Name,
                Type = 0,
                FormUrl = rStep.FormUrl,
                FormId = rStep.FormId,
                SenderId = user.Id,
                ReceiveId = rStep.ReceiveId,
                Status = rStep.Status,
                Sort = rStep.Sort
            };
            this.tasks.Add(task);
        }

        /// <summary>
        /// 操作完成
        /// </summary>
        /// <param name="args"></param>
        void Complete(Quest.Core.Process.WorkflowApplication.WFTaskArgs args)
        {
            WFTaskService.AddOrUpdate(c => new { c.InstanceId, c.StepId }, tasks.ToArray());
        }

        #endregion
    }
}
