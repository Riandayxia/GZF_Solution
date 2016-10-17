using System.Collections.Generic;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// 数据导出
    /// </summary>
    public class DataExport
    {
        /// <summary>
        /// 导出的列
        /// </summary>
        public List<ExportColumn> ExportColumns { get; set; }
        /// <summary>
        /// 筛选条件
        /// </summary>
        public List<DataFilter> ExportFilters { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public List<DataSort> ExportSorters { get; set; }
        /// <summary>
        /// 所选记录(json对象)
        /// </summary>
        public string SelectedRecords { get; set; }
    }
}
