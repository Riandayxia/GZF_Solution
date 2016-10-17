using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// ext grid data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtGirdData<T>
    {
        /// <summary>
        /// total
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public IList<T> data { get; set; }
    }

    /// <summary>
    /// easyui grid data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EasyUIGirdData<T>
    {
        /// <summary>
        /// total
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public IList<T> rows { get; set; }
    }
}
