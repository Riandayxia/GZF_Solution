using Quest.Framework;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq;
using Quest.Core;
using System.Collections.Generic;
using System;
using Quest.Core.Models.BPM;
using Quest.Core.BPM;

namespace Quest.WebSite.Controllers.BPM
{
    public partial class CDControllerController : BaseController
    {
        [Import]
        public ICDColumnService CDColumnService { get; set; }

        /// <summary>
        /// 动态编译
        /// </summary>
        /// <returns></returns>
        //public virtual ActionResult Auto()
        //{
        //    List<DBTable> table = CDTableService.Entities.ToList();
        //    List<DBColumn> columns = UDControllerService.Entities.ToList();

        //    // 初始化动态数据
        //    CoreInitializer.Initialize(table, columns);
        //    OperationResult or = new OperationResult(OperationResultType.Success);
        //    return this.JsonFormat(or);
        //}
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetAll()
        {
            Guid tableId = QuestRequest.GetGuid("tableId");
            IQueryable<CDController> items = CDControllerService.Entities.Where(c => c.TableId == tableId);
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }
    }
}
