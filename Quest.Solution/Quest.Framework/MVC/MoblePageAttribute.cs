/*  作者：      RaindayXia
*  创建时间：   2013/7/19 22:15:05
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuHui.Framework.MVC
{
    /// <summary>
    /// 表示当前功能页面允许被手机访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MoblePageAttribute : Attribute
    {
    }
}