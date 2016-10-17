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
    /// 流程实例 控制层
    /// </summary>
    [MenuDetail(Title = "流程实例", MType = MenuType.Menu, Icon = "lcsl", Use = MenuUse.PC)]
    public partial class WFRunInstanceController
    {
        #region 属性

        #endregion

        #region 视图功能

        /// <summary>
        /// 运行流程
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayWF()
        {
            String mainId = QuestRequest.Get("mainId");
            Guid mId = QuestRequest.GetGuid("mId");
            User user = new User()
            {
                Id = "00000000-0000-0000-0001-000000000001".GetGuid()
            };
            OperationResult or = WFRunInstanceService.Execute(mainId, mId, user);
            return this.JsonFormat(or);
        }
        /// <summary>
        /// 运行流程
        /// </summary>
        /// <returns></returns>
        public ActionResult WFTask()
        {
            String mainId = QuestRequest.Get("mainId");
            Guid stepId = QuestRequest.GetGuid("stepId");
            User user = new User()
            {
                Id = "00000000-0000-0000-0001-000000000001".GetGuid()
            };
            OperationResult or = WFRunInstanceService.Task(mainId, stepId, user);
            return this.JsonFormat(or);
        }


        /// <summary>
        /// 获取流程第一步骤
        /// 用于启动该流程前打开表单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFirstStep()
        {
            Guid flowId = QuestRequest.GetGuid("flowId");
            OperationResult or = WFRunInstanceService.ProcessFirstStep(flowId);
            return this.JsonFormat(or);
        }

        #endregion
    }
}
