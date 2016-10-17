using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Server
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class IServiceAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 描述主题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.Common.CustomsAttribute.Host.IServiceAttribute"/> 类.
        /// </summary>
        /// <param name="id">唯一标识Guid</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        public IServiceAttribute(Guid id, String title, String description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.Common.CustomsAttribute.Host.IServiceAttribute"/> 类.
        /// </summary>
        /// <param name="id">唯一标识Guid</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        public IServiceAttribute(String id, String title, String description) : this(id.GetGuid(), title, description) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.Common.CustomsAttribute.Host.IServiceAttribute"/> 类.
        /// </summary>
        /// <param name="id">唯一标识Guid</param>
        /// <param name="title">标题</param>
        public IServiceAttribute(String id, String title) : this(id, title, String.Empty) { }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.Common.CustomsAttribute.Host.IServiceAttribute"/> 类.
        /// </summary>
        /// <param name="id">唯一标识Guid</param>
        public IServiceAttribute(String id) : this(id, String.Empty) { }

        #endregion
    }
}
