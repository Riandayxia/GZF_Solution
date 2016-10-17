using SuHui.Framework;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq;
using SuHui.Core;
using SuHui.Core.Compiler;
using System.Collections.Generic;
using System;
using SuHui.Core.Models.Compiler;

namespace SuHui.WebSite.Controllers.Compiler
{
    public partial class UDControllerController : BaseController
    {
        [Import]
        public IDBColumnService DBColumnService { get; set; }

        /// <summary>
        /// 动态编译
        /// </summary>
        /// <returns></returns>
        //public virtual ActionResult Auto()
        //{
        //    List<DBTable> table = DBTableService.Entities.ToList();
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
            Guid tableId = SuHuiRequest.GetGuid("tableId");
            IQueryable<UDController> items = UDControllerService.Entities.Where(c => c.TableId == tableId);
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }
    }
}
