//------------------------------------------------------------------------------
// <copyright file="IDictionaryService.cs">
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
    /// 数据字典 控制层
    /// </summary>
    [MenuDetail(Title = "数据字典", MType = MenuType.Menu, Icon = "sjzd", Use = MenuUse.PC)]
    public partial class DictionaryController 
    {
        #region 属性

        #endregion

        #region 视图功能

        /// <summary>
        /// 获取数据字典信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            return this.JsonFormat(DictionaryService.Entities);
        }
        #endregion
    }
}
