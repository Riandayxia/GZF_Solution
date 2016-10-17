using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using SuHui.Core;
using SuHui.Framework;
using SuHui.Core.Compiler;
using SuHui.Core.Models.Compiler;

namespace SuHui.WebSite.Controllers.Compiler
{
    public partial class DBColumnController : BaseController
    {
        /// <summary>
        /// 添加 数据
        /// </summary>
        /// <returns>返回操作结果</returns>
        public virtual ActionResult Test()
        {
            IList<Guid> ids = SuHuiRequest.GetGuids("ids");
            OperationResult or = DBColumnService.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取指定表的字段信息
        /// </summary>
        /// <returns>返回操作结果</returns>
        public virtual ActionResult GetAll()
        {
            Guid tableId = SuHuiRequest.GetGuid("tableId");
            IQueryable<DBColumn> items = DBColumnService.Entities.Where(c => c.TableId == tableId);
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }
    }
}
