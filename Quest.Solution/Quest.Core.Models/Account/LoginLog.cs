using SuHui.Framework;
using SuHui.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SuHui.Core.Models.Account
{
    /// <summary>
    /// 实体类——登录记录信息
    /// </summary>
    [DBTable("登录记录信息")]
    [DataContract]
    public class LoginLog : BaseEntity<Guid>
    {
        /// <summary>
        /// 初始化一个 登录记录实体类 的新实例
        /// </summary>
        public LoginLog()
        {
            Id = CombHelper.NewComb();
        }

        [Required]
        [StringLength(15)]
        [DataMember]
        public string IpAddress { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        [DataMember]
        public virtual Member Member { get; set; }
    }
}
