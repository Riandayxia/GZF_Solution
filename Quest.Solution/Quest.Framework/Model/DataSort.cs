using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// ext data sort
    /// </summary>
    public class DataSort
    {
        /// <summary>
        /// 要排序的字段
        /// </summary>
        public string property { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string direction { get; set; }
    }
}
