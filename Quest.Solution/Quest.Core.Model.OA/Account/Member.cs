using SuHui.Core.Models.Security;
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
    ///     实体类——用户信息
    /// </summary>
    [DBTable("用户信息")]
    [DataContract]
    public class Member : BaseEntity<Guid>
    {
        public Member()
        {
            Id = CombHelper.NewComb();
        }

        [Required]
        [StringLength(20)]
        [DataMember]
        public String UserName { get; set; }

        [Required]
        [StringLength(32)]
        [DataMember]
        public String Password { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [Required]
        [StringLength(20)]
        [DataMember]
        public String NickName { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public String Email { get; set; }

        [StringLength(20)]
        [DataMember]
        public String Sex { get; set; }

        /// <summary>
        /// 获取或设置 用户扩展信息
        /// </summary>
        [DataMember]
        public virtual MemberExtend Extend { get; set; }

        /// <summary>
        /// 获取或设置 用户拥有的角色信息集合
        /// </summary>
        [DataMember]
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 获取或设置 用户登录记录集合
        /// </summary>
        [DataMember]
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
    }
}
