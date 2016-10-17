using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// ext form load data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtFormLoadData<T>
    {
        /// <summary>
        /// data
        /// </summary>
        public T[] data { get; set; }
    }
}
