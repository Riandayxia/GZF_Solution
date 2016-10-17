/*  作者：      RaindayXia
*  创建时间：   2013/7/19 19:01:58
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quest.Framework;

namespace Quest.Framework.MVC
{
    /// <summary>
    /// 描述标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuDetailAttribute : DescriptionAttribute
    {
        public MenuType MType { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public Boolean IsVisible { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public String Icon { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        public String ParentName { get; set; }

        /// <summary>
        /// 菜单用途
        /// </summary>
        public MenuUse Use { get; set; }

        public MenuDetailAttribute()
            : base()
        {
            IsVisible = true;
        }
    }

    public enum MenuType
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 10001001,
        /// <summary>
        /// 功能
        /// </summary>
        Feature = 10001002
    }
    /// <summary>
    /// 菜单用途枚举
    /// </summary>
    public enum MenuUse
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 10012003,
        /// <summary>
        /// 电脑
        /// </summary>
        PC = 10012001,
        /// <summary>
        /// 手机
        /// </summary>
        Phone = 10012002
    }
}
