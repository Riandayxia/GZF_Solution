using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Quest.WebSite.CommonSupport.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute()
        {
        }
        private String Message { get; set; }
        public LogAttribute(String message)
        {
            this.Message = message;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("filterContext");
            }
        }
    }
}
