using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.BPM
{
    [DBTable("流程模型")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class WFModel : BaseEntity
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        [DataMember]
        [StringLength(512)]
        [DBColumn("流程名称")]
        public String Name { get; set; }

        /// <summary>
        /// 获取或设置 表名称
        /// </summary>
        [DataMember]
        [DBColumn("表名称")]
        public String DBTableName { get; set; }

        /// <summary>
        /// 标识流程完成
        /// </summary>
        [DataMember]
        [DBColumn("完成标识")]
        public String DBFieldName { get; set; }

        /// <summary>
        /// 管理人员
        /// </summary>
        [DataMember]
        [StringLength(640)]
        [DBColumn("管理人员")]
        public String Manager { get; set; }

        /// <summary>
        /// 实例管理人员
        /// </summary>
        [DataMember]
        [StringLength(640)]
        [DBColumn("实例管理人员")]
        public String InstanceManager { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        [DBColumn("创建人")]
        public Guid CreateUserID { get; set; }

        /// <summary>
        /// 设计Json
        /// </summary>
        [DataMember]
        [DBColumn("设计Json")]
        public String DesignJSON { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        [DataMember]
        [DBColumn("安装日期")]
        public DateTime? InstallDate { get; set; }

        /// <summary>
        /// 安装人员
        /// </summary>
        [DataMember]
        [DBColumn("安装人员")]
        public Guid? InstallUserID { get; set; }

        /// <summary>
        /// 状态 1:设计中 2:已安装 3:已卸载 4:已删除
        /// </summary>
        [DataMember]
        [DBColumn("状态")]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [DBColumn("描述")]
        public String Desc { get; set; }
    }
}

