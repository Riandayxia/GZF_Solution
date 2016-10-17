using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SuHui.Core.Models.Account
{
    /// <summary>
    ///     登录信息类
    /// </summary>
    [DataContract]
    public class LoginInfo
    {
        /// <summary>
        ///     获取或设置 登录账号
        /// </summary>
        [DataMember]
        public string Access { get; set; }

        /// <summary>
        ///     获取或设置 登录密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        ///     获取或设置 IP地址
        /// </summary>
        [DataMember]
        public string IpAddress { get; set; }
    }
}
