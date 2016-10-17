using Quest.Framework;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq;
using Quest.Core;
using Quest.Core.BPM;
using System.Collections.Generic;
using System;
using Quest.Core.Models.BPM;
using Quest.Framework.MVC;
using Quest.Framework.ExtJs;

namespace Quest.WebSite.Controllers.BPM
{
    [MenuDetail(Title = "自定义表格", MType = MenuType.Menu, Icon = "zdybg", Use = MenuUse.PC)]
    public partial class CDTableController : BaseController
    {
        [Import]
        public ICDColumnService CDColumnService { get; set; }

        /// <summary>
        /// 动态编译
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Auto()
        {
            List<CDTable> table = CDTableService.Entities.ToList();
            List<CDColumn> columns = CDColumnService.Entities.ToList();

            // 初始化动态数据
            CoreInitializer.Initialize(table, columns);
            OperationResult or = new OperationResult(OperationResultType.Success);
            return this.JsonFormat(or);
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetAll()
        {
            IQueryable<CDTable> items = CDTableService.Entities;
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取自定义数据表数据
        /// </summary>
        /// <returns>返回下拉数据结构</returns>
        public ActionResult GetCombox()
        {
            List<ExtCombox<CDTable, Guid>> items = CDTableService.Entities
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
    }
}
