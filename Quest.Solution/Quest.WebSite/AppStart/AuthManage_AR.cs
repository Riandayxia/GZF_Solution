using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart
{
    [MenuDetail(Title = "权限管理", MType = MenuType.Menu, ParentName = "root", Icon = "xtgl", Use = MenuUse.PC)]
    public class AuthManage_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AuthManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AuthManage_default",
                "AuthManage/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.AuthManage" }
            );
        }
    }
}
