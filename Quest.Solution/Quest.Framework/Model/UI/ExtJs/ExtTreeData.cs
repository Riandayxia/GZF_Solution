using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.Framework.ExtJs
{
    /// <summary>
    /// 树形配置项
    /// </summary>
    public class ExtTreeData<T, key>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public key id { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        public String text { get; set; }

        /// <summary>
        /// 图标的样式
        /// </summary>
        public String iconCls { get; set; }

        /// <summary>
        /// 节点的样式
        /// </summary>
        public String cls { get; set; }

        /// <summary>
        /// 图标的路径
        /// </summary>
        public String icon { get; set; }

        /// <summary>
        /// 是否为叶子
        /// </summary>
        public Boolean leaf { get; set; }

        /// <summary>
        /// 树形Id
        /// </summary>
        public virtual String treeid { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public String href { get; set; }

        /// <summary>
        /// 链接的目标位置
        /// </summary>
        public String hrefTarget { get; set; }

        /// <summary>
        /// 设置是否展开
        /// </summary>
        public bool expanded { get; set; }

        /// <summary>
        /// 前面的复选框是否选中，必须把Checked
        /// 转换为小写的checked
        /// </summary>
        public Boolean Checked { get; set; }

        /// <summary>
        /// 数据原型对象
        /// </summary>
        public T Tobject { get; set; }

        /// <summary>
        /// 孩子数据
        /// </summary>
        public virtual IList<ExtTreeData<T, key>> children { get; set; }

    }
}
