using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model.UI.ExtJs
{
    /// <summary>
    /// Ext.Grid Column配置
    /// </summary>
    [Serializable]
    public class ExtColumn
    {
        /// <summary>
        /// 获取或设置 标题
        /// </summary>
        public String header { get; set; }

        /// <summary>
        /// 获取或设置 绑定字段
        /// </summary>
        public String dataIndex { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String text { get; set; }


        /// <summary>
        /// 布局
        /// </summary>
        public Int32 flex { get; set; }



    }
}
