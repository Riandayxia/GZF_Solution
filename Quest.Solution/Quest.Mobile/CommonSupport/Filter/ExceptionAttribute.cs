using System;
using System.Web.Mvc;

namespace Quest.Mobile.CommonSupport.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ExceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (filterContext.Exception == null) return;
            //var ex = filterContext.Exception; //获取异常源

            //if (ex.GetType() != typeof (RepositoryException) && ex.GetType() != typeof (QuestException)) return;
            //var excResult = new ContentResult
            //{
            //    Content =
            //        "{\"success\":false,\"msg\":\"" + filterContext.Exception.GetBaseException().Message +
            //        "\",\"id\":null}"
            //};
            //filterContext.Result = excResult;

            //filterContext.ExceptionHandled = true;
        }
    }
}
