using Quest.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.AppStart.BusinessManageCenter
{
    [MenuDetail(Title = "商户管理", MType = MenuType.Menu, ParentName = "BusinessManageCenter", Icon = "lcgl", Use = MenuUse.PC)]
    public class MerchantManage_AR : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MerchantManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MerchantManage_default",
                "MerchantManage/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Quest.WebSite.Controllers.BusinessManageCenter.MerchantManage" }
            );
        }
    }
}