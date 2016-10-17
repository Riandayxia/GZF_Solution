using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.Property
{
    /// <summary>
    /// 在线缴费
    /// </summary>
    [DBTable("在线缴费")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Payment : BaseEntity
    {

        #region Properties

        /// <summary>
        /// 缴费户号
        /// </summary>
        [DataMember]
        [DBColumn("缴费户号")]
        public String SerialNumber { get; set; }
        /// <summary>
        /// 缴费户名
        /// </summary>
        [DataMember]
        [DBColumn("缴费户名")]
        public String SerialName { get; set; }
        /// <summary>
        /// 缴费单位
        /// </summary>
        [DataMember]
        [DBColumn("缴费单位")]
        public String PaymentUnit { get; set; }

        /// <summary>
        /// 投诉类型
        /// </summary>
        [DataMember]
        [DBColumn("缴费类型")]
        public String PType { get; set; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        [DataMember]
        [DBColumn("缴费金额")]
        public Double Amount { get; set; }

        #endregion
    }
}
