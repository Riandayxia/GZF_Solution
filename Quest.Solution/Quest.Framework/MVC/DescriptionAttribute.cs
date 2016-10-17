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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// 描述主题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Desc { get; set; }

        #endregion

        #region Constructors
        public DescriptionAttribute() : this(String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="Quest.UIHelper.DescriptionAttribute"/> 类.
        /// </summary>
        /// <param name="title">标题</param>
        public DescriptionAttribute(String title) : this(title, String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="Quest.UIHelper.DescriptionAttribute"/> 类.
        /// </summary>
        /// <param name="id">唯一标识Guid</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="isVisible">是否激活</param>
        /// <param name="icon">图标</param>
        /// <param name="type">域名类型</param>
        public DescriptionAttribute(String title, String description)
        {
            this.Title = title;
            this.Desc = description;
        }

        #endregion
    }
}
