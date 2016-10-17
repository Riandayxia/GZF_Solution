using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart
{
    [MenuDetail(Title = "商业管理中心", MType = MenuType.Menu, ParentName = "root", Icon = "lcgl", Use = MenuUse.PC)]
    public class BusinessManageCenter_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BusinessManageCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BusinessManageCenter_default",
                "BusinessManageCenter/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.BusinessManageCenter" }
            );
        }
    }
}