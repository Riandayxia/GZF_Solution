using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.Mobile.AppStart
{
    [MenuDetail(Title = "流程管理", MType = MenuType.Menu, ParentName = "root", Icon = "lcgl", Use = MenuUse.PC)]
    public class BPM_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BPM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BPM_default",
                "BPM/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.Mobile.Controllers.BPM" }
            );
        }
    }
}
