using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{
    /// <summary>
    /// ext message
    /// </summary>
    public class ExtMessage
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 相关id
        /// </summary>
        public object id { get; set; }
    }
}
