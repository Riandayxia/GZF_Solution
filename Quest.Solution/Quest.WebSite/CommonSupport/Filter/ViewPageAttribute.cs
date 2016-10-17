using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.WebSite.CommonSupport.Filter
{
    /// <summary>
    /// 表示当前请求为页面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewPageAttribute : Attribute
    {
    }
}
