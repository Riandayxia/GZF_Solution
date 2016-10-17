using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model
{

    /// <summary>
    /// 根节点
    /// </summary>
    internal class Root
    {
        public Root()
        {
            isMargin = true;
            isHeadMargin = true;
        }
        /// <summary>
        /// 头部信息
        /// </summary>
        public HeadInfo head { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public String company { get; set; }

        /// <summary>
        /// 是否显示页眉页脚 默认显示
        /// </summary>
        public Boolean isMargin { get; set; }

        /// <summary>
        /// 是否显示页眉页脚标题 默认显示
        /// </summary>
        public Boolean isHeadMargin { get; set; }

        /// <summary>
        /// 上边距
        /// </summary>
        public String topMargin { get; set; }
        /// <summary>
        /// 下边距
        /// </summary>
        public String bottomMargin { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        public String leftMargin { get; set; }
        /// <summary>
        /// 右边距
        /// </summary>
        public String rightMargin { get; set; }
        /// <summary>
        /// 页眉边距 占不支持
        /// </summary>
        public String headerMargin { get; set; }
        /// <summary>
        /// 页脚边距 占不支持
        /// </summary>
        public String footerMargin { get; set; }
    }

    /// <summary>
    /// 报表表格头部信息
    /// </summary>
    internal class HeadInfo
    {
        /// <summary>
        /// 头部标签
        /// </summary>
        public IList<ExportColumn> columns { get; set; }
        /// <summary>
        /// 合并行
        /// </summary>
        public Int32? rowspan { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public String sheetname { get; set; }
        /// <summary>
        /// 打印方向 默认纵向
        /// </summary>
        public Boolean landscape { get; set; }
        /// <summary>
        /// 默认单元格宽度
        /// </summary>
        public Int32? defaultwidth { get; set; }
        /// <summary>
        /// 默认行高度
        /// </summary>
        public Int32? defaultheight { get; set; }
        /// <summary>
        /// 默认黑色,表格边框颜色
        /// </summary>
        public String bordercolor { get; set; }
        /// <summary>
        /// 边框风格，默认 thin
        /// </summary>
        public String borderstyle { get; set; }
    }

    /// <summary>
    /// 导出的列
    /// </summary>
    internal class ExportColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public String text { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public String dataIndex { get; set; }
        /// <summary>
        /// 显示方式
        /// </summary>
        public String align { get; set; }
        /// <summary>
        /// 垂直显示方式
        /// </summary>
        public String valign { get; set; }
        /// <summary>
        /// 头部标题显示方式
        /// </summary>
        public String halign { get; set; }

        /// <summary>
        /// 背景颜色.例如#000000
        /// </summary>
        public String bgcolor { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public short? fontsize { get; set; }
        /// <summary>
        /// 字体颜色,例如#000000
        /// </summary>
        public String fontcolor { get; set; }
        /// <summary>
        /// 单元格合并位置，（fromRow,toRow,fromColumn,toColumn）
        /// </summary>
        public String cellregion { get; set; }
        /// <summary>
        /// 字体名称
        /// </summary>
        public String fontName { get; set; }
        /// <summary>
        /// 表头文字是否加粗，默认加粗
        /// </summary>
        public String fontweight { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public Int32 width { get; set; }
        /// <summary>
        /// 标题行高度
        /// </summary>
        public Int32? height { get; set; }
        /// <summary>
        /// 数据行高度
        /// </summary>
        public Int32? dheight { get; set; }
        /// <summary>
        ///是否是斜体
        /// </summary>
        public Boolean? IsItalic { get; set; }
        /// <summary>
        /// 是否有中间线
        /// </summary>
        public Boolean? IsStrikeout { get; set; }
        /// <summary>
        /// 设置下划线
        /// </summary>
        public Boolean? Underline { get; set; }
    }
}
