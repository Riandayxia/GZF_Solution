using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Server
{
    /// <summary>
    /// 服务属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SuHui.Common.CustomsAttribute.Host.ServiceAttribute"/> 类.
        /// </summary>
        /// <param name="id">该唯一标识为它实现接口的唯一标识</param>
        public ServiceAttribute(String id)
        {
            this.Id = id.GetGuid();
        }

        #endregion
    }
}
