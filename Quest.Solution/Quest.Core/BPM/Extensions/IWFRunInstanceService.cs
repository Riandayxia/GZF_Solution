using Quest.Core.Models.Base;
using Quest.Core.Models.BPM;
using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.BPM
{
    /// <summary>
    /// 流程运行 核心业务契约
    /// </summary>
    public partial interface IWFRunInstanceService 
    {
        #region 属性

        #endregion

        #region 公共方法

        /// <summary>
        /// 根据指定参数启动流程 
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="wfModeId">流程模型Id</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        OperationResult Execute(String mainId, Guid wfModeId, User user);

        /// <summary>
        /// 流程任务处理
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="task">任务信息</param>
        /// <param name="user">当前用户</param>
        /// <returns>返回操作结果</returns>
        OperationResult Task(String mainId, WFTask task, User user);

        /// <summary>
        /// 流程任务处理
        /// </summary>
        /// <param name="mainId">实体唯一标识Id</param>
        /// <param name="stepId">流程步骤Id</param>
        /// <param name="user">当前用户</param>
        /// <returns>返回操作结果</returns>
        OperationResult Task(String mainId, Guid stepId, User user);
        
        /// <summary>
        /// 获取指定流程Id的首步骤的表单信息
        /// </summary>
        /// <returns>返回操作结果</returns>
        OperationResult ProcessFirstStep(Guid wfId);

        #endregion
    }
}
