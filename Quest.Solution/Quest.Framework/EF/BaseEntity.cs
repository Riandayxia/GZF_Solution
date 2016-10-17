using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Framework
{
    /// <summary>
    ///     可持久到数据库的领域模型的基类。
    /// </summary>
    [DataContract]
    public abstract class BaseEntity : IEntity
    {
        #region 构造函数

        /// <summary>
        ///     数据实体基类
        /// </summary>
        protected BaseEntity()
        {
            IsDeleted = false;
            Id = Guid.NewGuid();
            CreatedTime = DateTime.Now;
            LastUpdatedTime = DateTime.Now;
        }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region 属性

        [Key]
        [Column(Order = 1)]
        [DataMember]
        [DBColumn("唯一标识")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [DataMember]
        [DBColumn("是否删除")]
        public Boolean IsDeleted { get; set; }

        /// <summary>
        /// 获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [DataMember]
        [DBColumn("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 上次修改时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [DataMember]
        [DBColumn("修改时间")]
        public DateTime? LastUpdatedTime { get; set; }

        ///// <summary>
        /////     获取或设置 版本控制标识，用于处理并发
        ///// </summary>
        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] Timestamp { get; set; }

        #endregion
    }
}
