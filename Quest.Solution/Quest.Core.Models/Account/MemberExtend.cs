using SuHui.Framework;
using SuHui.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SuHui.Core.Models.Account
{
    /// <summary>
    ///     实体类——用户扩展信息
    /// </summary>
    [DBTable("用户扩展信息")]
    [DataContract]
    public class MemberExtend : BaseEntity<Guid>
    {
        /// <summary>
        /// 初始化一个 用户扩展实体类 的新实例
        /// </summary>
        public MemberExtend()
        {
            Id = CombHelper.NewComb();
        }

        [DataMember]
        public string Tel { get; set; }

        [DataMember]
        public MemberAddress Address { get; set; }

        [DataMember]
        public virtual Member Member { get; set; }
    }
}
