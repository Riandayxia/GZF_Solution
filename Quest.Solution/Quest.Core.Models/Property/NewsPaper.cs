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
    [DBTable("报事管理")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class NewsPaper : BaseEntity
    {
        #region Properties
      
        /// <summary>
        /// 报事类型
        /// </summary>
        [DataMember]
        [DBColumn("报事类型")]
        public String NType { get; set; }

      
        /// 报事地址
        /// </summary>
        [DataMember]
        [DBColumn("报事地址")]
        public String Address { get; set; }
         
     
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
        /// 报事标题
        /// </summary>
        [DataMember]
        [DBColumn("报事标题")]
        public String Title { get; set; }
        /// <summary>
        /// 报事内容
        /// </summary>
        [DataMember]
        [DBColumn("报事内容")]
        public String Content { get; set; }
        /// <summary>
        /// <summary>
        /// 处理状态
        /// </summary>
        [DataMember]
        [DBColumn("处理状态")]
        public Int32 Status { get; set; }

        /// <summary>
        /// 上传附件
        /// </summary>
        [DataMember]
        [DBColumn("上传附件")]
        public Int32 Accessory { get; set; }
         #endregion
    }
}
