using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// extjs data filter
    /// </summary>
    public class DataFilter
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// comparison
        /// </summary>
        public string comparison { get; set; }
    }
}
