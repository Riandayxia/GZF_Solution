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
using Quest.Framework.MVC;
using Quest.Core.Models.Property;

namespace Quest.WebSite.Controllers.Property
{
    /// <summary>
    /// 活动资讯 控制器
    /// </summary>

    [MenuDetail(Title = "社区资讯", MType = MenuType.Menu, Icon = "lcsl", Use = MenuUse.PC)]
    public partial class CommunityController : BaseController
    {
        #region 基本方法
        /// <summary>
        /// 添加 数据列数据
        /// </summary>
        /// <param name="entity">数据列对象</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        [ValidateInput(false)]
        [Feature("添加", "icon_add")]
        public  ActionResult OAdd(Community entity)
        {
            OperationResult or = CommunityService.Add(entity);
            return this.JsonFormat(or);
        }
       #endregion
        #region  查询方法

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns>返回操作结果</returns>
        public  ActionResult GetAll()
        {
            return this.JsonFormat(CommunityService.Entities);
        }
        #endregion
    }
}
