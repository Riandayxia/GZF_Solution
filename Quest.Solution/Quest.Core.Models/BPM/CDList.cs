using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.BPM
{
    /// <summary>
    /// 自定义列表
    /// </summary>
    [DBTable("自定义列表")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class CDList : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 获取或设置 对应表
        /// </summary>
        [DataMember]
        [DBColumn("数据库表Id")]
        public Guid DBTableId { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        [StringLength(64)]
        [DataMember]
        [DBColumn("数据库表")]
        public String DBTableName { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        [StringLength(64)]
        [DataMember]
        [DBColumn("创建者")]
        public String Creator { get; set; }

        /// <summary>
        /// 获取或设置 设计中的列表定义表达式
        /// 以Json数据存储，未发布是不可用
        /// </summary>
        [DataMember]
        [DBColumn("DesignJson")]
        public String DesignJson { get; set; }

        /// <summary>
        /// 获取或设置 可直接运行的列表定义表达式
        /// 以Json数据存储，并发布
        /// </summary>
        [DataMember]
        [DBColumn("RunJson")]
        public String RunJson { get; set; }

        #endregion
    }
}