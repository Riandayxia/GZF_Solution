using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.BPM
{
    /// <summary>
    /// 流程实例对象
    /// </summary>
    [DBTable("流程实例")]
    [DataContract]
    public class WFRunInstance : BaseEntity
    {
        public WFRunInstance()
        {
            Id = CombHelper.NewComb();
        }

        /// <summary>
        /// 实体唯一标识Id
        /// </summary>
        [DataMember]
        [DBColumn("实体唯一标识")]
        [StringLength(64)]
        public String MainId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        [DBColumn("创建用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 运行时流程设计
        /// </summary>
        [DataMember]
        [DBColumn("运行时流程")]
        public String DesignJSON { get; set; }

    }
}
