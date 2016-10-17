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

    [DBTable("在线缴费账户信息")]
    [DataContract]
    [Export(typeof(IEntity))]
    public  class PAccount : BaseEntity
    {
        #region Properties
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [DBColumn("名称")]
        public String Name{ get; set; }

        /// <summary>
        /// 楼栋
        /// </summary>
        [DataMember]
        [DBColumn("楼栋")]
        public String Loudong { get; set; }

        /// <summary>
        /// 号数或卡号
        /// </summary>
        [DataMember]
        [DBColumn("号数或卡号")]
        public String SerialNumber { get; set; }

       
        /// <summary>
        /// 是否同意
        /// </summary>
        [DataMember]
        [DBColumn("是否同意")]
        public Boolean Agree { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        [DataMember]
        [DBColumn("账户类型")]
        public String PType { get; set; }
        #endregion
    }
}
