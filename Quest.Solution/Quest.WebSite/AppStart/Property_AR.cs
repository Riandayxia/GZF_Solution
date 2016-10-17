using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart
{
    [MenuDetail(Title = "物业管理", MType = MenuType.Menu, ParentName = "root", Icon = "xtgl", Use = MenuUse.PC)]
    public class Property_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Property";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Property_default",
                "Property/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.Property" }
            );
        }
    }
}
