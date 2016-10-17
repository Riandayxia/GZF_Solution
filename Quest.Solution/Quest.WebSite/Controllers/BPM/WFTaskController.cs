//------------------------------------------------------------------------------
// <copyright file="IWFRunInstanceService.cs">
//        Copyright(c)2013 QuestCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：Quest.Core
//        生成时间：2016-08-09 15:44
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Quest.Core;
using Quest.Framework;
using Quest.Framework.MVC;
using Quest.Core.Models.BPM;
using Quest.Core.Models.Base;
using Quest.Core.BPM;

namespace Quest.WebSite.Controllers.BPM
{
    /// <summary>
    /// 任务管理 控制层
    /// </summary>
    [MenuDetail(Title = "任务管理", MType = MenuType.Menu, Icon = "rwgl", Use = MenuUse.PC)]
    public partial class WFTaskController
    {
        #region 属性

        /// <summary>
        /// 获取或设置 流程实例数据访问对象
        /// </summary>
        [Import]
        public IWFRunInstanceService WFRunInstanceService { get; set; }
        
        #endregion

        #region 视图功能

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetAll()
        {
            IQueryable<WFTask> items = WFTaskService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 运行流程
        /// </summary>
        /// <returns></returns>
        public ActionResult Process()
        {
            String mainId = QuestRequest.Get("MainId");
            Guid stepId = QuestRequest.GetGuid("StepId");
            User user = new User()
            {
                Id = "00000000-0000-0000-0001-000000000001".GetGuid()
            };
            OperationResult or = WFRunInstanceService.Task(mainId, stepId, user);
            return this.JsonFormat(or);
        }

        #endregion
    }
}
