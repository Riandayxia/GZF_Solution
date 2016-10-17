using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.Users
{
    [DBTable("地址")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Address : BaseEntity
    {
        public Address()
        {
            Id = CombHelper.NewComb();
        }
        /// <summary>
        /// 获取或设置 用户Id
        /// </summary>
        [DBColumn("用户Id")]
        [DataMember]
        public Guid UsersId { get; set; }

        /// <summary>
        /// 获取或设置 收货人
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("收货人")]
        public String Receiver { get; set; }

        /// <summary>
        /// 获取或设置 联系电话
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("联系电话")]
        public String Mobile { get; set; }

        /// <summary>
        /// 获取或设置 所在小区
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("所在小区")]
        public String TheCell { get; set; }

        /// <summary>
        /// 获取或设置 是否为默认地址
        /// </summary>
        [DataMember]
        [DBColumn("是否为默认地址")]
        public Boolean IsDefault  { get; set; }

        /// <summary>
        /// 获取或设置 详细地址
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("详细地址")]
        public String DetailedAddress { get; set; }
    }
}
