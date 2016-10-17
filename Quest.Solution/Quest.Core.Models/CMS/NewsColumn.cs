using System.Collections.Generic;
using Niwar.Framework.Data.PetaPoco;
using Niwar.Framework.DomainModel;
using Niwar.Framework.Common;

namespace Niwar.CMS
{
    [TableName("NewsColumns")]
    [PrimaryKey("ColumnId")]
    public class NewsColumn : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("ColumnId")]
        public int Id { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        [ResultColumn]
        public string ParentName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ExistColumn]
        public string ColumnName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 外链
        /// </summary>
        public string OutLink { get; set; }

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

        /// <summary>
        /// 下级团队
        /// </summary>
        [ResultColumn]
        public IList<NewsColumn> children { get; set; }

        [ResultColumn]
        public bool leaf { get; set; }

        [ResultColumn]
        public bool expanded { get; set; }

        public NewsColumn() {
            SEOLink = Utils.GetGuid();
        }
    }
}
