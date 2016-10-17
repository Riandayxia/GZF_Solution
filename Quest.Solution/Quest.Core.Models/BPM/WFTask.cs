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
    /// 流程任务信息对象
    /// </summary>
    [DBTable("任务信息")]
    [DataContract]
    public class WFTask : BaseEntity
    {
        public WFTask()
        {
            Id = CombHelper.NewComb();
        }

        /// <summary>
        /// 上一任务Id
        /// </summary>
        [DataMember]
        [DBColumn("上一任务Id")]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 实体Id
        /// 具体指申请流程的主题数据唯一标识
        /// </summary>
        [DataMember]
        [DBColumn("实体Id")]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 主体id 
        /// </summary>
        [DataMember]
        [DBColumn("主体id")]
        [StringLength(512)]
        public String MainId { get; set; }
        /// <summary>
        /// 步骤Id
        /// </summary>
        [DataMember]
        [DBColumn("步骤Id")]
        public Guid StepId { get; set; }
        /// <summary>
        /// 表单Id
        /// </summary>
        [DataMember]
        [DBColumn("表单Id")]
        public Guid FormId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [DataMember]
        [DBColumn("步骤名称")]
        [StringLength(256)]
        public String StepName { get; set; }

        /// <summary>
        /// 任务类型 0正常 1指派 2委托 3转交 4退回 5抄送
        /// </summary>
        [DataMember]
        [DBColumn("任务类型")]
        public Int32 Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        [DBColumn("标题")]
        [StringLength(256)]
        public String Title { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        [DataMember]
        [DBColumn("发送人")]
        public Guid SenderId { get; set; }

        /// <summary>
        /// 任务执行人
        /// </summary>
        [DataMember]
        [DBColumn("任务执行人")]
        [StringLength(1024)]
        public String ReceiveId { get; set; }

        /// <summary>
        /// 表单地址
        /// 该地址支持多平台,采用json格式
        /// {phoneUrl:'aaa',webUrl:'bbbb',otherUrl:''}
        /// </summary>
        [DataMember]
        [DBColumn("表单地址")]
        public String FormUrl { get; set; }

        /// <summary>
        /// 打开时间
        /// </summary>
        [DataMember]
        [DBColumn("打开时间")]
        public DateTime? OpenTime { get; set; }

        /// <summary>
        /// 规定完成时间
        /// </summary>
        [DataMember]
        [DBColumn("规定完成时间")]
        public Double CompletedTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        [DataMember]
        [DBColumn("实际完成时间")]
        public DateTime? ActualFinishTime { get; set; }

        /// <summary>
        /// 是否同意
        /// 0表示：不同意；1表示：同意
        /// </summary>
        [DataMember]
        [DBColumn("是否同意")]
        public Int32 Agreest { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        [DataMember]
        [DBColumn("意见")]
        [StringLength(1024)]
        public String Comment { get; set; }

        /// <summary>
        /// 是否签章 0未签 1已签
        /// </summary>
        [DataMember]
        [DBColumn("是否签章")]
        public Int32? IsSign { get; set; }

        /// <summary>
        /// 状态 -1 等待中的任务 0 待处理 1打开 2完成 3退回 4他人已处理 5他人已退回
        /// </summary>
        [DataMember]
        [DBColumn("状态")]
        public Int32 Status { get; set; }

        /// <summary>
        /// 其它说明
        /// </summary>
        [DataMember]
        [DBColumn("其它说明")]
        [StringLength(1024)]
        public String Note { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        [DBColumn("序号")]
        public Int32 Sort { get; set; }
    }
}
