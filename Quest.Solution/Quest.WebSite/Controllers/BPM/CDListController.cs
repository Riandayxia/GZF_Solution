//------------------------------------------------------------------------------
// <copyright file="ICDListService.cs">
//        Copyright(c)2013 QuestCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：Quest.Core
//        生成时间：2016-09-01 13:05
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
using Quest.Framework.ExtJs;

namespace Quest.WebSite.Controllers.BPM
{
    /// <summary>
    /// 列表管理 控制层
    /// </summary>
    [MenuDetail(Title = "列表管理", MType = MenuType.Menu, Icon = "lbgl", Use = MenuUse.PC)]
    public partial class CDListController : BaseController
    {
        #region 属性

        /// <summary>
        /// 获取或设置 数据表数据访问对象
        /// </summary>
        [Import]
        public ICDTableService CDTableService { get; set; }

        #endregion

        #region 视图功能

        /// <summary>
        /// 获取自定义数据表数据
        /// </summary>
        /// <returns>返回下拉数据结构</returns>
        public ActionResult GetCombox()
        {
            IQueryable<Guid> tableIds = CDListService.Entities.Select(c => c.DBTableId);
            List<ExtCombox<CDTable, Guid>> items = CDTableService.Entities.Where(c => !tableIds.Contains(c.Id))
                .Select(c => new ExtCombox<CDTable, Guid>
                {
                    Text = c.ByName,
                    Value = c.Id,
                    Tobject = c
                })
                .OrderBy(c => c.Tobject.CreatedTime).ToList();
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            IQueryable<CDList> items = CDListService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 添加 列表信息数据
        /// </summary>
        /// <param name="entity">列表信息对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("添加", "icon_add")]
        public ActionResult AddOrUpdate(CDList entity)
        {
            List<CDList> items = new List<CDList> { entity };
            OperationResult or = CDListService.AddOrUpdate((c => new { c.Id }), items);
            return this.JsonFormat(or);
        }

        #endregion
    }
}
