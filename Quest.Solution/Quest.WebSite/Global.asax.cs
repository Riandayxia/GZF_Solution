using Quest.WebSite;
using Quest.WebSite.AppStart;
using Quest.WebSite.Helper.Ioc;
using Quest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Quest.Core.Initialize;

namespace Quest.WebSite
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //注册自定义视图
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new QuestRazorViewEngine());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 设置MEF依赖注入容器
            DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            MefDependencySolver solver = new MefDependencySolver(catalog);
            DependencyResolver.SetResolver(solver);
            // 初始化数据连接
            ContentInitializer.Initialize();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //判断有效的身份验证信息
            if (HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity.IsAuthenticated &&
                HttpContext.Current.User.Identity is FormsIdentity)
            {
                //获得进行了Forms身份验证的用户标识  
                var userIdent = (FormsIdentity)(HttpContext.Current.User.Identity);
                //从身份验证票中获得用户数据  
                var userData = userIdent.Ticket.UserData;
                //分割用户数据得到用户角色数组  
                var rolues = userData.Split(',');
                //从用户标识和角色组初始化GenericPrincipal类  
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(userIdent, rolues);
            }
        }
    }
}