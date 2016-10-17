using System;
using SuHui.Framework;
using SuHui.Framework.T4;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Composition;

namespace SuHui.Core.Models.Test
{
    [DBTable("TableTest")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class TableTest : BaseEntity
    {
        public TableTest()
        {
        }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("用户名")]
        public String Name { get; set; }
        /// <summary>
        /// 获取或设置 用户密码
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("用户密码")]
        public String Password { get; set; }

    }
    /// <summary>
    /// 实体类-数据表映射 用户
    /// </summary>    
    internal partial class TestConfiguration : MappingBase<TableTest>
    {
    }
}
