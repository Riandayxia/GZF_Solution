using Quest.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quest.WebSite.Controllers
{
    public class InitDataController : BaseController
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult Init()
        {
            //var data = SysConfigService.SysConfig.AsQueryable().ToList();
            var hash = new Hashtable { { "myInfo", this.GetMyInfo() }, { "sysInfo", this.GetSettings() } };
            var result = new JavaScriptResult { Script = "var idata = " + JsonHelper.ToJson(hash) };
            return result;
        }
        /// <summary>s
        /// 我的基本信息
        /// </summary>
        /// <returns></returns>
        private dynamic GetMyInfo()
        {
            dynamic myInfo = new ExpandoObject();
            myInfo.curIp = "";
            myInfo.loginName = "Admin";
            myInfo.mobile = "";
            myInfo.email = "";
            myInfo.userId = "";
            myInfo.name = "";
            myInfo.roles = "";
            myInfo.roleColumns = "";
            myInfo.roleFunctions = "";

            return myInfo;
        }

        /// <summary>
        /// 系统配置信息
        /// </summary>
        /// <returns></returns>
        private dynamic GetSettings()
        {
            dynamic sysInfo = new ExpandoObject();
            sysInfo.errLogAllowedRoles = "";
            sysInfo.dictions = "";
            sysInfo.title = "ttt";
            sysInfo.siterights = "Copyright © 2012-2016 cnsuhui.com, All Rights Reserved.";
            sysInfo.siterights_cn = "";
            sysInfo.siteversion = "1.2.0";

            return sysInfo;
        }
    }
}
