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
    [DBTable("表单管理")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class WFForm : BaseEntity
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        [DataMember]
        [StringLength(512)]
        [DBColumn("表单名称")]
        public String Title { get; set; }

        /// <summary>
        /// 数据表
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("数据表")]
        public String DBTable { get; set; }

        /// <summary>
        /// 数据表名称
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("数据表名称")]
        public String DBTableName { get; set; }

        /// <summary>
        /// 处理地址
        /// </summary>
        [DataMember]
        [StringLength(512)]
        [DBColumn("处理地址")]
        public String Url_AddOrUpdate { get; set; }

        /// <summary>
        /// 表单html
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("标题字段")]
        public String TitleField { get; set; }
        /// <summary>
        /// 表单html
        /// </summary>
        [DataMember]
        [DBColumn("运行html")]
        public String RunHtml { get; set; }

        /// <summary>
        /// 表单html
        /// </summary>
        [DataMember]
        [DBColumn("设计html")]
        public String DesignHtml { get; set; }

        ///// <summary>
        ///// 相关属性
        ///// </summary>
        //[DBColumn("相关属性")]
        //public String Attribute { get; set; }

    }
}
