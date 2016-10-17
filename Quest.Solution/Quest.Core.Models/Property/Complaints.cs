using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.Property
{
    [DBTable("投诉管理")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Complaints : BaseEntity
    {

         #region Properties
        /// <summary>
        /// 投诉类型
        /// </summary>
        [DataMember]
        [DBColumn("投诉类型")]
        public String CType { get; set; }

        /// <summary>
        /// 投诉内容
        /// </summary>
        [DataMember]
        [DBColumn("投诉内容")]
        public String Content { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [DataMember]
        [DBColumn("提交人")]
        public String Submitter { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DataMember]
        [DBColumn("联系人")]
        public String Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember]
        [DBColumn("联系电话")]
        public String Phone { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [DataMember]
        [DBColumn("详细地址")]
        public String Address { get; set; }
         #endregion
    }
}
