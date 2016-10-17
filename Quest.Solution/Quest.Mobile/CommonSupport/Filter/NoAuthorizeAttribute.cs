using System;

namespace Quest.Mobile.CommonSupport.Filter
{
    /// <summary>
    /// 表示当前不需要验证权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NoAuthorizeAttribute : Attribute
    {
    }
}
