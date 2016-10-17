using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart
{
    [MenuDetail(Title = "系统管理", MType = MenuType.Menu, ParentName = "root", Icon = "xtgl", Use = MenuUse.PC)]
    public class Base_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Base";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Base_default",
                "Base/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.Base" }
            );
        }
    }
}
