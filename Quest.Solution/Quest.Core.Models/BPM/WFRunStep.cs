using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.BPM
{
    /// <summary>
    /// 流程运行步骤对象
    /// </summary>
    [DBTable("流程步骤")]
    [DataContract]
   public  class WFRunStep : BaseEntity
    {
        /// <summary>
        /// 当前步骤Id
        /// </summary>
        [DataMember]
        [DBColumn("步骤Id")]
        public Guid SId { get; set; }

        /// <summary>
        /// 上一步骤Id
        /// </summary>
        [DataMember]
        [DBColumn("上一步骤Id")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [DataMember]
        [DBColumn("步骤名称")]
        public String Name { get; set; }

        /// <summary>
        /// 流程运行Id
        /// 具体指申请流程的主题数据唯一标识
        /// </summary>
        [DataMember]
        [DBColumn("流程运行Id")]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        [DataMember]
        [DBColumn("发送人")]
        public Guid SenderId { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        [DataMember]
        [DBColumn("表单Id")]
        public Guid FormId { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DataMember]
        [DBColumn("发送时间")]
        public DateTime? SenderTime { get; set; }

        /// <summary>
        /// 表单地址
        /// 该地址支持多平台,采用json格式
        /// {phoneUrl:'aaa',webUrl:'bbbb',otherUrl:''}
        /// </summary>
        [DataMember]
        public String FormUrl { get; set; }

        /// <summary>
        /// 接收人员Id
        /// 过个接收人已分号隔开
        /// 如：xxxx1;xxxx2
        /// </summary>
        [DataMember]
        [DBColumn("接收人员Id")]
        [StringLength(1024)]
        public String ReceiveId { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        [DataMember]
        [DBColumn("接收时间")]
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [DataMember]
        [DBColumn("有效期")]
        public DateTime? Valid { get; set; }

        /// <summary>
        /// 状态 -1 等待中的任务 0 待处理 1打开 2完成 3退回 4他人已处理 5他人已退回
        /// </summary>
        [DataMember]
        [DBColumn("状态")]
        public Int32 Status { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        [DBColumn("序号")]
        public Int32 Sort { get; set; }
    }
}
