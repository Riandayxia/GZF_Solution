﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Quest.Core;
using Quest.Framework;
using Quest.Core.BPM;
using Quest.Core.Models.BPM;
using Quest.Framework.ExtJs;
using Quest.Mobile;

namespace Quest.WebSite.Controllers.Property
{
    /// <summary>
    /// 在线缴费账户信息 控制器
    /// </summary>
    public partial class PAccountController : BaseController
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns>返回操作结果</returns>
        public virtual ActionResult GetAll()
        {
            return this.JsonFormat(PAccountService.Entities);
        }
    }
}
