//------------------------------------------------------------------------------
// <copyright file="IDBColumnService.cs">
//        Copyright(c)2013 SuHuiCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：SuHui.Core
//        生成时间：2016-08-18 09:39
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using SuHui.Core;
using SuHui.Framework;
using SuHui.Framework.MVC;
using SuHui.Core.Models.Compiler;
using SuHui.Core.Compiler;

namespace SuHui.WebSite.Controllers.Compiler
{
	/// <summary>
    /// 数据列 控制层
    /// </summary>
    [Export]
    public partial class DBColumnController : BaseController
    {
        #region 属性

        /// <summary>
        /// 获取或设置 数据列数据访问对象
        /// </summary>
        [Import]
        public IDBColumnService DBColumnService { get; set; }
        
        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加 数据列数据
        /// </summary>
        /// <param name="entity">数据列对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("添加","icon_add")]
        public virtual ActionResult Add(DBColumn entity)
        {
            OperationResult or = DBColumnService.Insert(entity);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 修改 数据列数据
        /// </summary>
        /// <param name="entity">数据列对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("修改","icon_edit")]
        public virtual ActionResult Update(DBColumn entity)
        {
            entity.LastUpdatedTime = DateTime.Now;
            OperationResult or = DBColumnService.Update(entity);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 删除 数据列数据 
        /// 根据数据列唯一标识Id集合,数据格式','隔开“1,2,3,4...”
        /// </summary>
        /// <returns>返回操作结果</returns>
        [Feature("删除","icon_delete")]
        public virtual ActionResult Delete()
        {
            IList<Guid> ids = SuHuiRequest.GetGuids("ids");
            OperationResult or = DBColumnService.Delete(c => ids.Contains(c.Id));
            return this.JsonFormat(or);
        }

        #endregion
    }
}
