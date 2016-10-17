using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Quest.Core;
using Quest.Framework;
using Quest.Core.BPM;
using Quest.Core.Models.BPM;
using Quest.Framework.ExtJs;

namespace Quest.WebSite.Controllers.BPM
{
    public partial class CDColumnController : BaseController
    {
        /// <summary>
        /// 添加 数据
        /// </summary>
        /// <returns>返回操作结果</returns>
        public virtual ActionResult Test()
        {
            IList<Guid> ids = QuestRequest.GetGuids("ids");
            OperationResult or = CDColumnService.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取指定表的字段信息
        /// </summary>
        /// <returns>返回操作结果</returns>
        public virtual ActionResult GetAll()
        {
            Guid tableId = QuestRequest.GetGuid("tableId");
            IQueryable<CDColumn> items = CDColumnService.Entities.Where(c => c.TableId == tableId);
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取自定义数据表数据
        /// </summary>
        /// <returns>返回下拉数据结构</returns>
        public ActionResult GetCombox()
        {
            Guid tableId = QuestRequest.GetGuid("tId");
            List<ExtCombox<CDColumn, String>> items = CDColumnService.Entities
                .Where(c => c.TableId == tableId)
                .Select(c => new ExtCombox<CDColumn, String>
                {
                    Text = c.Text,
                    Value = c.Name,
                    Tobject = c
                })
                .OrderBy(c => c.Tobject.CreatedTime).ToList();
          
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

    }
}
