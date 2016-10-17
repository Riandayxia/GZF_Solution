//------------------------------------------------------------------------------
// <copyright file="IFlowModelService.cs">
//        Copyright(c)2013 QuestCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：Quest.Core
//        生成时间：2016-08-09 10:11
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
using Quest.Core.BPM;

namespace Quest.WebSite.Controllers.BPM
{
    /// <summary>
    /// 流程模型 控制层
    /// </summary>
    [MenuDetail(Title = "流程模型", MType = MenuType.Menu, Icon = "lcgl", Use = MenuUse.PC)]
    public partial class WFModelController
    {
        #region 属性

        #endregion

        #region 视图功能

        /// <summary>
        /// 保存流程
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveFlow()
        {
            String json = QuestRequest.Get("wfjson");
            WFModel info = JsonHelper.DecodeObject<WFModel>(json);
            info.DesignJSON = json;
            if (info.Id.IsNullOrEmpty()) info.Id = Guid.NewGuid();

            List<WFModel> items = new List<WFModel> { info };
            OperationResult or = WFModelService.AddOrUpdate(c => new { c.Id }, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取流程模板数据
        /// 分页方法
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            Int32 page = QuestRequest.GetInt("page");
            Int32 pageSize = QuestRequest.GetInt("limit");
            IQueryable<WFModel> items = WFModelService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取指定Id 的流程信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetById()
        {
            Guid id = QuestRequest.GetGuid("folwId");
            OperationResult or = WFModelService.GetByKey(id);
            return this.JsonFormat(or);
        }
        #endregion
    }
}
