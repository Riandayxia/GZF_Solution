//------------------------------------------------------------------------------
// <copyright file="IMenuService.cs">
//        Copyright(c)2013 QuestCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：溯汇软件@中国
//        公司网站：http://www.cnsuhui.com
//        所属工程：Quest.Core
//        生成时间：2016-08-18 09:39
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
using Quest.Core.Models.Base;
using Quest.Core.Base;

namespace Quest.WebSite.Controllers.Base
{
    /// <summary>
    /// 菜单 菜单管理
    /// </summary>
    [MenuDetail(Title = "菜单管理", MType = MenuType.Menu, Icon = "cdgl", Use = MenuUse.PC)]
    public partial class MenuController
    {
        #region 属性

        /// <summary>
        /// 获取指定Node值得菜单信息
        /// </summary>
        /// <returns>返回树形结构数据</returns>
        public ActionResult GetTree()
        {
            String parentName = QuestRequest.Get("node");
            return this.JsonFormat(MenuService.GetByParentName(parentName.IsNullOrEmpty() ? "root" : parentName));
        }
        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            String parentName = QuestRequest.Get("pName");
            IQueryable<Menu> items = MenuService.Entities.Where(c => c.ParentName == parentName);
            OperationResult or = new OperationResult(OperationResultType.Success, string.Empty, items);
            return this.JsonFormat(or);
        }

        /// <summary>
        /// 获取所有根菜单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoots()
        {
            return this.JsonFormat(MenuService.GetByRoot());
        }

        #endregion

        #region 视图功能

        #endregion
    }
}
