using System;

namespace Quest.WebSite.CommonSupport.Filter
{
    /// <summary>
    /// 表示当前不需要验证权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NoAuthorizeAttribute : Attribute
    {
    }
}
