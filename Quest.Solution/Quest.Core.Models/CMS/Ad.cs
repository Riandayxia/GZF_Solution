using SuHui.Framework;
using SuHui.Framework.T4;
using System;
using System.Runtime.Serialization;

namespace SuHui.Core.Models.CMS
{
    [DBTable("广告信息")]
    [DataContract]
    public class Ad :  BaseEntity<Guid>
    {

        /// <summary>
        /// 广告代码
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        [DataMember]
        public int AdType { get; set; }

        /// <summary>
        /// 广告宽
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        /// 广告高
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// 广告内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 广告备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

    }
}
