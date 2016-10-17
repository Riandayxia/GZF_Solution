using SuHui.Framework;
using SuHui.Framework.T4;
using System;
using System.Runtime.Serialization;

namespace SuHui.Core.Models.CMS
{
    [DBTable("广告信息")]
    [DataContract]
    public class News : BaseEntity<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("NewsId")]
        public int Id { get; set; }

        /// <summary>
        /// 关联栏目
        /// </summary>
        public int ColumnId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [ResultColumn]
        public string ColumnName { get; set; }

        /// <summary>
        /// 分类链接
        /// </summary>
        [ResultColumn]
        public string ColumnSEOLink { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [ExistColumn]
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 列表图标
        /// </summary>
        public string ListIcon { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 页面链接
        /// </summary>
        public string SEOLink { get; set; }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// 页面关键词
        /// </summary>
        public string SeoKeywords { get; set; }

        /// <summary>
        /// 页面描述
        /// </summary>
        public string SeoDescription { get; set; }

        public News()
        {
            SEOLink = Utils.GetGuid();
        }
    }
}
