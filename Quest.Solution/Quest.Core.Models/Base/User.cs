using System;
using Quest.Framework;
using Quest.Framework.T4;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;

namespace Quest.Core.Models.Base
{
    [DBTable("用户")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class User : BaseEntity
    {
        public User()
        {
            Id = CombHelper.NewComb();
        }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("用户名")]
        public String LoginName { get; set; }

        /// <summary>
        /// 获取或设置 手机
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("手机")]
        public String Mobile { get; set; }

        /// <summary>
        /// 获取或设置 手机是否验证
        /// </summary>
        [DataMember]
        [DBColumn("手机是否验证")]
        public Boolean IsValidMobile { get; set; }

        /// <summary>
        /// 获取或设置 用户密码
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("用户密码")]
        public String Password { get; set; }

        /// <summary>
        /// 获取或设置 密码是否验证
        /// </summary>
        [DataMember]
        [DBColumn("密码是否验证")]
        public Boolean IsValidPassword { get; set; }

        /// <summary>
        /// 获取或设置 验证码
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("验证码")]
        public String VerificationCode { get; set; }

        /// <summary>
        /// 获取或设置 是否同意协议
        /// </summary>
        [DataMember]
        [DBColumn("是否同意协议")]
        public Boolean Protocol { get; set; }

        /// <summary>
        /// 获取或设置 性别
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("性别")]
        public String Gender { get; set; }

        /// <summary>
        /// 获取或设置 出生年月
        /// </summary>
        [DataMember]
        [DBColumn("出生年月")]

        public DateTime? Birth { get; set; }

         /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        [DBColumn("头像")]
        public String Avatar { get; set; }

    }
}
