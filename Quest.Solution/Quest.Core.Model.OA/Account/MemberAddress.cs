using SuHui.Framework;
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
    /// 用户地址信息
    /// </summary>
    [DataContract]
    public class MemberAddress
    {
        [StringLength(10)]
        [DataMember]
        public string Province { get; set; }

        [StringLength(20)]
        [DataMember]
        public string City { get; set; }

        [StringLength(20)]
        [DataMember]
        public string County { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [DataMember]
        public string Street { get; set; }
    }
}
