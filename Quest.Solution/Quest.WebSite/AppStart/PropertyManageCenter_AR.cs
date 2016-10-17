using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart
{
    [MenuDetail(Title = "物业管理中心", MType = MenuType.Menu, ParentName = "root", Icon = "lcgl", Use = MenuUse.PC)]
    public class PropertyManageCenter_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PropertyManageCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PropertyManageCenter_default",
                "PropertyManageCenter/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.PropertyManageCenter" }
            );
        }
    }
}