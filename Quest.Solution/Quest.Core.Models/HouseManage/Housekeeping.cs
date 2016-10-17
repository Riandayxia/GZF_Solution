using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.HouseManage
{
    [DBTable("家政服务")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Housekeeping : BaseEntity
    {
        public Housekeeping()
        {
            Id = CombHelper.NewComb();
        }
        #region Properties

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
        /// 服务类型
        /// </summary>
        [DataMember]
        [DBColumn("服务类型")]
        public String Type { get; set; }

        /// <summary>
        /// 特殊要求
        /// </summary>
        [DataMember]
        [DBColumn("特殊要求")]
        public String Content { get; set; }

        /// <summary>
        /// 服务时长
        /// </summary>
        [DataMember]
        [DBColumn("服务时长")]
        public String Duration { get; set; }

        /// <summary>
        /// 服务价格
        /// </summary>
        [DataMember]
        [DBColumn("服务价格")]
        public String ServicePrice { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        [DataMember]
        [DBColumn("服务地址")]
        public String Address { get; set; }
        #endregion
    }
}
