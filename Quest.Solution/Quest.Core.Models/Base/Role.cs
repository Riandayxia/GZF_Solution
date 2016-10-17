using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.Base
{
    [DBTable("角色")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Role : BaseEntity
    {
        public Role()
        {
        }

        #region Properties

        /// <summary>
        /// 获取或设置 角色名
        /// </summary>
        [StringLength(128)]
        [DataMember]
        [DBColumn("角色名")]
        public String Name { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [StringLength(1024)]
        [DBColumn("描述")]
        [DataMember]
        public String Desc { get; set; }

        #endregion

    }
}
