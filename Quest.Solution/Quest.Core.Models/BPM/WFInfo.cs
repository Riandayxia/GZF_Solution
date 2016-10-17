using Quest.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.BPM
{
    /// <summary>
    /// 流程信息对象
    /// </summary>
    [NotMapped]
    [DataContract]
    public class WFInfo
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// 数据表
        /// </summary>
        [DataMember]
        public String DBTableName { get; set; }

        /// <summary>
        /// 完成标识
        /// </summary>
        [DataMember]
        public String DBFieldName { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        [DataMember]
        public String Type { get; set; }

        /// <summary>
        /// 流程类型：0 常规流程 1 自由流程
        /// </summary>
        [DataMember]
        public int FlowType { get; set; }

        /// <summary>
        /// 流程管理者
        /// </summary>
        [DataMember]
        public String Manager { get; set; }

        /// <summary>
        /// 实例管理者
        /// </summary>
        [DataMember]
        public String InstanceManager { get; set; }

        /// <summary>
        /// 第一步ID
        /// </summary>
        [DataMember]
        public Guid FirstStepID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public String CreateUser { get; set; }

        /// <summary>
        /// 设计时
        /// </summary>
        [DataMember]
        public String DesignJSON { get; set; }

        /// <summary>
        /// 状态 1:设计中 2:已安装 3:已卸载 4:已删除
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Note { get; set; }

        /// <summary>
        /// 是否调试模式 0关闭 1开启(有调试窗口) 2开启(无调试窗口)
        /// </summary>
        [DataMember]
        public int Debug { get; set; }

        /// <summary>
        /// 调试人员
        /// </summary>
        [DataMember]
        public String DebugUsers { get; set; }

        /// <summary>
        /// 流程步骤
        /// </summary>
        [DataMember]
        public IEnumerable<Step> Steps { get; set; }

        /// <summary>
        /// 流程连线
        /// </summary>
        [DataMember]
        public IEnumerable<Line> Lines { get; set; }

    }

    /// <summary>
    /// 流程步骤实体对象
    /// </summary>
    [NotMapped]
    [DataContract]
    public class Step
    {
        /// <summary>
        /// 步骤实例Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 步骤类型 normal 一般步骤 subflow 子流程步骤
        /// </summary>
        [DataMember]
        public String Type { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 处理者
        /// 多个用户用分号(;)隔开
        /// </summary>
        [DataMember]
        public String DefaultUser { get; set; }

        /// <summary>
        /// 表单Id
        /// 该地址支持多平台,采用json格式
        /// {phoneUrl:'aaa',webUrl:'bbbb',otherUrl:''}
        /// </summary>
        [DataMember]
        public String FormId { get; set; }
        /// <summary>
        /// 表单地址
        /// 该地址支持多平台,采用json格式
        /// {phoneUrl:'aaa',webUrl:'bbbb',otherUrl:''}
        /// </summary>
        [DataMember]
        public String FormUrl { get; set; }
        /// <summary>
        /// 意见显示 0不显示 1显示
        /// </summary>
        [DataMember]
        public int OpinionDisplay { get; set; }
        /// <summary>
        /// 超期提示 0不提示 1要提示
        /// </summary>
        [DataMember]
        public int ExpiredPrompt { get; set; }
        /// <summary>
        /// 审签类型 0无签批意见栏 1有签批意见(无须签章) 2有签批意见(须签章)
        /// </summary>
        [DataMember]
        public int SignatureType { get; set; }
        /// <summary>
        /// 工时(小时)
        /// </summary>
        [DataMember]
        public decimal? WorkTime { get; set; }
        /// <summary>
        /// 限额时间(小时)
        /// </summary>
        [DataMember]
        public decimal? LimitTime { get; set; }
        /// <summary>
        /// 额外时间(小时)
        /// </summary>
        [DataMember]
        public decimal? OtherTime { get; set; }
        /// <summary>
        /// 是否归档 0不归档 1要归档
        /// </summary>
        [DataMember]
        public int Archives { get; set; }
        /// <summary>
        /// 归档参数
        /// </summary>
        [DataMember]
        public String ArchivesParams { get; set; }
        /// <summary>
        /// 步骤备注说明
        /// </summary>
        [DataMember]
        public String Note { get; set; }
        ///// <summary>
        ///// 步骤行为相关参数
        ///// </summary>
        //[DataMember]
        //public StepSet.Behavior Behavior { get; set; }

        /// <summary>
        /// 设计时x坐标(用于排序)
        /// </summary>
        [DataMember]
        public decimal Position_x { get; set; }
        /// <summary>
        /// 设计时y坐标(用于排序)
        /// </summary>
        [DataMember]
        public decimal Position_y { get; set; }
        /// <summary>
        /// 子流程ID
        /// </summary>
        [DataMember]
        public String SubFlowID { get; set; }
    }

    /// <summary>
    /// 流程连线实体
    /// </summary>
    [NotMapped]
    [DataContract]
    public class Line
    {
        /// <summary>
        /// 连线实体Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 连线文本信息
        /// </summary>
        [DataMember]
        public String Text { get; set; }
        /// <summary>
        /// 连线源步骤ID
        /// </summary>
        [DataMember]
        public Guid FromID { get; set; }
        /// <summary>
        /// 连线目标ID
        /// </summary>
        [DataMember]
        public Guid ToID { get; set; }
        /// <summary>
        /// 连线条件表达式
        /// </summary>
        [DataMember]
        public FilterGroup Expression { get; set; }
        /// <summary>
        /// Sql语句
        /// </summary>
        [DataMember]
        public String TSql { get; set; }
        /// <summary>
        /// 连线说明
        /// </summary>
        [DataMember]
        public String Note { get; set; }
    }
}

namespace Quest.Core.Models.BPM.StepSet
{
    /// <summary>
    /// 步骤行为实体
    /// </summary>
    [DataContract]
    [NotMapped]
    public class Behavior
    {
        /// <summary>
        /// 流转类型 0系统控制 1单选一个分支流转 2多选几个分支流转
        /// </summary>
        [DataMember]
        public int FlowType { get; set; }
        /// <summary>
        /// 运行时选择 0不允许 1允许
        /// </summary>
        [DataMember]
        public int RunSelect { get; set; }
        /// <summary>
        /// 处理者类型 0所有成员 1部门 2岗位 3工作组 4人员 5发起者 6前一步骤处理者 7某一步骤处理者 8字段值 9发起者主管 10发起者分管领导 11当前处理者主管 12当前处理者分管领导
        /// </summary>
        [DataMember]
        public int HandlerType { get; set; }
        /// <summary>
        /// 选择范围
        /// </summary>
        [DataMember]
        public String SelectRange { get; set; }
        /// <summary>
        /// 当处理者类型为 7某一步骤处理者 时的处理者步骤
        /// </summary>
        [DataMember]
        public Guid HandlerStepID { get; set; }
        /// <summary>
        /// 当处理者类型为 8字段值 时的字段
        /// </summary>
        [DataMember]
        public String ValueField { get; set; }
        /// <summary>
        /// 默认处理者
        /// </summary>
        [DataMember]
        public Guid DefaultHandler { get; set; }
        /// <summary>
        /// 退回策略 0不能退回 1单个退回 2全部退回
        /// </summary>
        [DataMember]
        public int BackModel { get; set; }
        /// <summary>
        /// 处理策略 0所有人必须处理 1一人同意即可 2依据人数比例 3独立处理
        /// </summary>
        [DataMember]
        public int HanlderModel { get; set; }
        /// <summary>
        /// 退回类型 0退回前一步 1退回第一步 2退回某一步
        /// </summary>
        [DataMember]
        public int BackType { get; set; }
        /// <summary>
        /// 策略百分比
        /// </summary>
        [DataMember]
        public decimal? Percentage { get; set; }
        /// <summary>
        /// 退回步骤ID 当退回类型为 2退回某一步 时
        /// </summary>
        [DataMember]
        public Guid BackStepID { get; set; }
        /// <summary>
        /// 会签策略 0 不会签 1 所有步骤同意 2 一个步骤同意即可 3 依据比例
        /// </summary>
        [DataMember]
        public int Countersignature { get; set; }
        /// <summary>
        /// 会签策略是依据比例时设置的百分比
        /// </summary>
        [DataMember]
        public decimal? CountersignaturePercentage { get; set; }
        ///// <summary>
        ///// 子流程处理策略 0 子流程完成后才能提交 1 子流程发起即可提交
        ///// </summary>
        //[DataMember]
        //public int SubFlowStrategy { get; set; }
        /// <summary>
        /// 抄送人员
        /// </summary>
        [DataMember]
        public String CopyFor { get; set; }
    }

}
