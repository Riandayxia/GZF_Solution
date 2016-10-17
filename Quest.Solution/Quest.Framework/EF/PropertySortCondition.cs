using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Framework.EF
{
    /// <summary>
    ///     属性排序条件信息类
    /// </summary>
    [DataContract]
    public class PropertySortCondition
    {
        /// <summary>
        ///     构造一个指定属性名称的升序排序的排序条件
        /// </summary>
        /// <param name="property">排序属性名称</param>
        public PropertySortCondition(String property)
            : this(property, "asc") { }

        /// <summary>
        ///     构造一个排序属性名称和排序方式的排序条件
        /// </summary>
        /// <param name="property">排序属性名称</param>
        /// <param name="direction">排序方式</param>
        public PropertySortCondition(String property, String direction)
        {
            this.PropertyName = property;
            this.Direction = direction;
            this.ListSortDirection = direction == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }

        /// <summary>
        /// 获取或设置 排序属性名称
        /// </summary>
        [DataMember(Name = "property")]
        public String PropertyName { get; set; }

        /// <summary>
        /// 获取或设置 排序方向
        /// </summary>
        [DataMember(Name = "direction")]
        public String Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value.ToLower();
                ListSortDirection = direction == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            }
        }

        /// <summary>
        /// 排序方向枚举
        /// </summary>
        [NonSerialized]
        public ListSortDirection ListSortDirection;

        /// <summary>
        /// 存储排序方向
        /// </summary>
        [NonSerialized]
        private String direction;

    }

}
