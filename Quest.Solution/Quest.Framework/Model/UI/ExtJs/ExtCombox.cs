using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.Framework.ExtJs
{
    /// <summary>
    /// 下拉框对象
    /// </summary>
    public class ExtCombox<T, key>
    {
        /// <summary>
        /// 项文本
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// 项值
        /// </summary>
        public key Value { get; set; }

        /// <summary>
        /// 原型对象
        /// </summary>
        public T Tobject { get; set; }
    }
}
