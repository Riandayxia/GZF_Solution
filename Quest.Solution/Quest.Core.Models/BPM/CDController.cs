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
    /// 控制器
    /// </summary>
    [DBTable("控制器")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class CDController : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 获取或设置 对应表
        /// </summary>
        [DataMember]
        [DBColumn("对应表")]
        public Guid TableId { get; set; }

        /// <summary>
        /// 获取或设置 扩展代码
        /// </summary>
        [DataMember]
        [DBColumn("代码")]
        public String Code { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        [StringLength(64)]
        [DataMember]
        [DBColumn("创建者")]
        public String Creator { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [DataMember]
        [DBColumn("描述")]
        public String Desc { get; set; }

        #endregion

    }
}
