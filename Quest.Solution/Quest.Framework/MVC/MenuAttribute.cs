/*  作者：      RaindayXia
*  创建时间：   2013/7/19 19:01:58
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SuHui.Framework;

namespace SuHui.Framework.MVC
{
    /// <summary>
    /// 描述标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute : DescriptionAttribute
    {
        #region Properties

        /// <summary>
        /// 是否激活
        /// </summary>
        public Boolean IsVisible { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public String Icon { get; set; }

        #endregion

        #region Constructors


        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.UIHelper.MenuAttribute"/> 类.
        /// </summary>
        public MenuAttribute()
            : this(String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.UIHelper.MenuAttribute"/> 类.
        /// </summary>
        /// <param name="title">标题</param>
        public MenuAttribute(String title)
            : this(title, String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.UIHelper.MenuAttribute"/> 类.
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        public MenuAttribute(String title, String desc)
            : this(title, desc, true, String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.UIHelper.MenuAttribute"/> 类.
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="isVisible">是否激活</param>
        public MenuAttribute(String title, String desc, Boolean isVisible)
            : this(title, desc, isVisible, String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.UIHelper.MenuAttribute"/> 类.
        /// </summary>      
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="isVisible">是否激活</param>
        /// <param name="icon">图标</param>
        public MenuAttribute(String title, String desc, Boolean isVisible, String icon)
            : base(title, desc)
        {
            this.IsVisible = isVisible;
            this.Icon = icon;
        }

        #endregion
    }
}
