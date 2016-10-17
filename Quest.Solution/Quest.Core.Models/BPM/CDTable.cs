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
    /// 数据表
    /// </summary>
    [DBTable("数据表")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class CDTable : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 获取或设置 表名称
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("数据库表名")]
        public String Name { get; set; }

        /// <summary>
        /// 获取或设置 别名
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("别名")]
        public String ByName { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [StringLength(1024)]
        [DataMember]
        [DBColumn("描述")]
        public String Desc { get; set; }

        /// <summary>
        /// 获取或设置 字段排序
        /// </summary>
        [DataMember]
        [DBColumn("排序")]
        public Int32 Sort { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        [StringLength(64)]
        [DataMember]
        [DBColumn("创建者")]
        public String Creator { get; set; }

        #endregion
    }
}
