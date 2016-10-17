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
    [DBTable("菜单")]
    [DataContract]
    [Export(typeof(IEntity))]
    public class Menu : BaseEntity
    {
        /// <summary>
        /// 获取或设置 菜单名称
        /// </summary>
        [DataMember]
        [StringLength(64)] 
        [DBColumn("菜单名称")]
        public String Name { get; set; }

        /// <summary>
        /// 获取或设置 上级菜单名称
        /// </summary>
        [DataMember]
        [StringLength(64)]
        [DBColumn("上级菜单名称")]
        public String ParentName { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称
        /// 用于菜单类型
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("控制器名称")]
        public String ControllerName { get; set; }

        /// <summary>
        /// 获取或设置 对象功能名称
        /// 用于功能类型
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("功能名称")]
        public String FeatureName { get; set; }

        /// <summary>
        /// 获取或设置 菜单链接
        /// </summary>
        [DataMember]
        [StringLength(512)]
        [DBColumn("菜单链接")]
        public String MenuLink { get; set; }

        /// <summary>
        /// 获取或设置 手机地址
        /// </summary>
        [DataMember]
        [DBColumn("手机地址")]
        [StringLength(512)]
        public String PhoneLink { get; set; }

        /// <summary>
        /// 获取或设置 参数
        /// </summary>
        [DataMember]
        [DBColumn("参数")]
        [StringLength(512)]
        public String Params { get; set; }

        /// <summary>
        /// 获取或设置 图标样式
        /// </summary>
        [DataMember]
        [StringLength(128)]
        [DBColumn("图标样式")]
        public String IconClass { get; set; }

        /// <summary>
        /// 获取或设置 是否是菜单
        /// </summary>
        [DataMember]
        [DBColumn("菜单类型")]
        public Int32 MType { get; set; }
        /// <summary>
        /// 获取或设置 菜单用途
        /// </summary>
        [DataMember]
        [DBColumn("菜单用途")]
        public Int32 Use { get; set; }

    }
}
