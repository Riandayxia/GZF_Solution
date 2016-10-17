using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Core.Models.BPM
{
    /// <summary>
    /// 数据列
    /// </summary>
    [DBTable("数据列")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class CDColumn : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 获取或设置 对应表
        /// </summary>
        [DataMember]
        [DBColumn("对应表")]
        public Guid TableId { get;set; }

        /// <summary>
        /// 获取或设置 字段名称
        /// </summary>
        [DataMember]
        [DBColumn("名称")]
        [StringLength(64)]
        public String Name { get; set; }

        /// <summary>
        /// 获取或设置 字段文本
        /// </summary>
        [DataMember]
        [DBColumn("文本")]
        [StringLength(128)]
        public String Text { get; set; }

        /// <summary>
        /// 获取或设置 字段类型
        /// </summary>
        [DataMember]
        [DBColumn("类型")]
        [StringLength(64)]
        public String DBType { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        [StringLength(64)]
        [DataMember]
        [DBColumn("创建者")]
        public String Creator { get; set; }

        /// <summary>
        /// 获取或设置 字段排序
        /// </summary>
        [DataMember]
        [DBColumn("排序")]
        public Int32 Sort { get; set; }

        /// <summary>
        /// 获取或设置 是否允许为空
        /// </summary>
        [DataMember]
        [DBColumn("是否为空")]
        public Boolean IsNull { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [DataMember]
        [DBColumn("描述")]
        [StringLength(1024)]
        public String Desc { get; set; }

        #endregion
    }
}
