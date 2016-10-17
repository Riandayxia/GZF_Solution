//------------------------------------------------------------------------------
// <copyright file="IWFFormService.cs">
//        Copyright(c)2013 QuestCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：Quest.Core
//        生成时间：2016-08-16 16:37
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
    /// 表单管理 控制层
    /// </summary>
    [MenuDetail(Title = "表单管理", MType = MenuType.Menu, Icon = "bdgl", Use = MenuUse.PC)]
    public partial class WFFormController
    {
        #region 属性

        #endregion

        #region 视图功能

        public ActionResult GetAll()
        {
            Int32 page = QuestRequest.GetInt("page");
            Int32 pageSize = QuestRequest.GetInt("limit");
            IQueryable<WFForm> items = WFFormService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取指定Id表单数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetById()
        {
            Guid Id = QuestRequest.GetGuid("Id");
            OperationResult or = WFFormService.GetByKey(Id);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 添加 表单管理数据
        /// </summary>
        /// <param name="entity">表单管理对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("添加", "icon_add")]
        public ActionResult AddOrUpdate(WFForm entity)
        {
            List<WFForm> forms = new List<WFForm>() { entity };
            OperationResult or = WFFormService.AddOrUpdate((c => new { c.Id }), forms);
            return this.JsonFormat(or);
        }

        #endregion
    }
}
