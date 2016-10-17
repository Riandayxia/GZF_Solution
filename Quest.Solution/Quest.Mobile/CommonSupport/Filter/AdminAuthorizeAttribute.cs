using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quest.Mobile.CommonSupport.Filter
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (filterContext == null)
            //{
            //    throw new ArgumentNullException("filterContext");
            //}

            //var naAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthorizeAttribute), true);
            //var isNoAuthorize = naAttrs.Length == 1;//当前Action是否不需要验证
            //if (isNoAuthorize) return;

            //var vpAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewPageAttribute), true);
            //var isViewPage = vpAttrs.Length == 1;//当前Action请求是否为具体的功能页

            //String sbName = ConfigurationManager.AppSettings["Marked"], msg = String.Empty;
            //if (this.AuthorizeCore(filterContext, isViewPage, sbName, out msg) != false) return;

            //String dev = String.Empty;
            //var session = System.Web.HttpContext.Current.Session;
            //if (session != null)
            //{
            //    dev = Convert.ToString(session["device"]);
            //}

            //switch (dev)
            //{
            //    case "phone":
            //        //判定用户是否登录
            //        if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //        {
            //            // 登录过期,调用重新登录方法
            //            filterContext.Result = new ContentResult { Content = @"SHUtil.ReLogin('登录过期,请新登录')" };
            //        }
            //        break;
            //    default: //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
            //        if (!isViewPage)
            //        {
            //            var user = CurrentUser.GetUser();
            //            if (!user.IsNullOrEmpty()) return;

            //            filterContext.Result = new RedirectResult("/Home/Login");//直接URL输入的页面地址跳转到登录页    
            //        }
            //        break;
            //}
        }

        /// <summary>
        /// 权限判断业务逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="isViewPage"></param>
        /// <param name="sbName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext, bool isViewPage, String sbName, out string msg)
        {

            msg = "未登录，访问非法";
            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            //判定用户是否登录
            if (sbName != "phone" && !HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            ////如果当前请求的地址在权限表里存在
            //if (Caches.GetAllActions().Count(p => p.ActionUrl == (controllerName + "/" + actionName)) <= 0)
            //    return true;
            ////判断当前用户是否拥有该url的访问权
            //if (CurrentUser.GetInstance.Actions.Any(p => p.ActionUrl == (controllerName + "/" + actionName)))
            //    return true;
            msg = "您没有操作当前功能的权限";
            return false;
        }
    }
}
