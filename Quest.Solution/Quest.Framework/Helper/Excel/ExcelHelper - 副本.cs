using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.IO;
using NPOI.HPSF;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using NPOI.SS.Util;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using SuHui.Framework.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Controls;
using System.Threading;
using org.pdfbox.pdmodel;
using org.pdfbox.util;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SuHui.Framework.Helper.Excel
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class ExcelHelper
    {
        #region 导出excel
        private const Int32 MaxRowPerSheet = 65535;
        private Int32 _rowPerSheet = 1000;

        static bool cellColorBug = true; //关于NPOI自定义颜色设置有个bug，这个可以保证第一次单元格设置不会始终黑色
        /// <summary>
        /// 自定义颜色
        /// </summary>
        HSSFPalette XlPalette = null;
        /// <summary>
        /// 整个表格border颜色，默认黑色
        /// </summary>
        private short wholeBorderColor = HSSFColor.Black.Index;
        public short WholeBorderColor
        {
            get { return wholeBorderColor; }
            set { wholeBorderColor = value; }
        }

        /// <summary>
        /// 表头单元格字体是否加粗
        /// </summary>
        private short headFontWeight = (short)FontBoldWeight.Bold;
        /// <summary>
        /// 表头单元格字体是否加粗
        /// </summary>
        public short HeadFontWeight
        {
            get { return headFontWeight; }
            set { headFontWeight = value; }
        }
        /// <summary>
        /// 整个表格border样式，默认solid
        /// </summary>
        private NPOI.SS.UserModel.BorderStyle wholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
        private NPOI.SS.UserModel.BorderStyle WholeBorderStyle
        {
            get { return wholeBorderStyle; }
            set { wholeBorderStyle = value; }
        }
        /// <summary>
        /// 单sheet最大行数
        /// </summary>
        public Int32 RowPerSheet
        {
            get { return _rowPerSheet; }
            set
            {
                if (value < 0 || value > MaxRowPerSheet)
                {
                    value = MaxRowPerSheet;
                }
                else
                {
                    _rowPerSheet = value;
                }
            }
        }

        /// <summary>
        /// 填充导出数据并返回Iworkbook
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="records">数据</param>
        /// <param name="json_headers">列表</param>
        public void Export<T>(IList<T> records, String json_headers, String fileName)
        {
            if (records == null)
                throw new ArgumentNullException("records");
            if (json_headers == null || json_headers.Length == 0)
                throw new ArgumentNullException("json_headers is null or Length 0");

            Root root = JsonHelper.DecodeObject<Root>(json_headers);

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(root.head.sheetname);
            SetWorkbook<T>(records, fileName, root, workbook, sheet);

            this.Export(workbook, fileName);
        }

        private void SetWorkbook<T>(IList<T> records, String fileName, Root root, HSSFWorkbook workbook, ISheet sheet)
        {
            #region 设置页眉页脚
            if (root.isMargin)
            {
                sheet.Header.Left = "\n\n项目名称：" + fileName + "\n";
                sheet.Header.Right = "\n\n第" + HSSFHeader.Page + "页 共" + HSSFHeader.NumPages + "页\n";
                sheet.Footer.Right = root.company;
            }
            if (root.isHeadMargin)
            {
                sheet.Header.Center = HSSFHeader.StartBold + HSSFHeader.FontSize(20) + root.head.sheetname + HSSFHeader.EndBold;
            }
            #endregion

            List<PropertyInfo> columns = new List<PropertyInfo>();
            // 设置头部
            SetHeader<T>(root, workbook, sheet, columns);

            // 填充数据
            SetData<T>(records, root, workbook, sheet, columns);
        }


        public string RenderControlAsString(System.Web.UI.Control ctl)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            ctl.RenderControl(writer);
            return sw.ToString();
        }
        public string ExcelToPDF<T>(IList<T> records, String json_headers, String fileName)
        {
            if (records == null)
                throw new ArgumentNullException("records");
            if (json_headers == null || json_headers.Length == 0)
                throw new ArgumentNullException("json_headers is null or Length 0");

            Root root = JsonHelper.DecodeObject<Root>(json_headers);

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(root.head.sheetname);
            SetWorkbook<T>(records, fileName, root, workbook, sheet);

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                HttpContext context = HttpContext.Current;
                //获取请求的文件路径，如C:\www\a.pdf
                string path = context.Request.MapPath(context.Request.Path);

                //创建PDF实体和文字解析器
                PDDocument doc = PDDocument.load(path);
                PDFTextStripper stripper = new PDFTextStripper();

                //创建一个TextBox用来显示PDF内容
                System.Web.UI.WebControls.TextBox tb = new System.Web.UI.WebControls.TextBox();

                //tb.Style.Add("width", "100%");
                //tb.Style.Add("height", "100%");

                //tb.TextMode = TextBoxMode.MultiLine;
                tb.Text = stripper.getText(doc);

                //将TextBox render出来
                context.Response.Write(RenderControlAsString(tb));
            }

            //using (var ms = new MemoryStream())
            //{
            //    workbook.Write(ms);
            //    ms.Flush();
            //    ms.Position = 0;

            //    HttpContext content = HttpContext.Current;

            //    // 设置编码和附件格式
            //    content.Response.ContentType = "application/pdf";
            //    content.Response.ContentEncoding = Encoding.UTF8;
            //    content.Response.Charset = "";
            //    content.Response.AppendHeader("Content-Disposition",
            //        "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".pdf");

            //    content.Response.BinaryWrite(ms.GetBuffer());
            //    content.Response.End();
            //}

            ////实例化Excel
            //Microsoft.Office.Interop.Excel.Application repExcel = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook workbook = null;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            ////打开文件，n.FullPath是文件路径
            //workbook = repExcel.Application.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            //string filesavefilename = path.ToString();
            //string strsavefilename = filesavefilename.Substring(0, filesavefilename.Length - 3) + "pdf";
            //object savefilename = (object)strsavefilename;
            //object ofmt = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
            ////进行另存为操作  
            //workbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, savefilename, Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityMinimum, true, true, 1, 100, false, Type.Missing);
            //object osave = false;
            ////逐步关闭所有使用的对象
            //workbook.Close(osave, Type.Missing, Type.Missing);
            //repExcel.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            //worksheet = null;
            ////垃圾回收
            //GC.Collect();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            //workbook = null;
            //GC.Collect();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(repExcel.Application.Workbooks);
            //GC.Collect();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(repExcel);
            //repExcel = null;
            //GC.Collect();
            ////依据时间杀灭进程
            //System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            //foreach (System.Diagnostics.Process p in process)
            //{
            //    if (DateTime.Now.Second - p.StartTime.Second > 0 && DateTime.Now.Second - p.StartTime.Second < 5)
            //    {
            //        p.Kill();
            //    }
            //}
            return "Hello";
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="root"></param>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columns"></param>
        internal void SetData<T>(IList<T> records, Root root, HSSFWorkbook workbook, ISheet sheet, List<PropertyInfo> columns)
        {
            IList<HSSFCellStyle> ics = new List<HSSFCellStyle>();
            Int32 height = 20;
            for (Int32 r = 0; r < records.Count; r++)
            {
                IRow row = null;
                if (!sheet.IsNullOrEmpty())
                    row = sheet.CreateRow(root.head.rowspan.GetInt32() + r);

                for (Int32 i = 0; i < columns.Count; i++)
                {
                    if (columns[i].IsNullOrEmpty())
                        continue;
                    var value = columns[i].GetValue(records[r], null);
                    String drValue = String.Empty;
                    if (value != null)
                        drValue = value.ToString();

                    drValue = drValue.Replace("<br/>", "\n");
                    drValue = drValue.Replace("<br>", "\n");
                    if (!row.IsNullOrEmpty())
                    {
                        ICell newCell = row.CreateCell(i);

                        if (ics.Count < columns.Count)
                        {
                            ExportColumn ec = root.head.columns.SingleOrDefault(c => c.dataIndex == columns[i].Name);

                            SetHeadCellBold(ec);
                            // 设置行高
                            if (!ec.dheight.IsNullOrEmpty())
                            {
                                height = ec.dheight.GetInt32() == 0 ? 1 : ec.dheight.GetInt32();
                            }
                            // 获得样式  
                            ICellStyle style = GetCellStyle(workbook, ExcelStyle.Default, ec);
                            ics.Add(style as HSSFCellStyle);
                            newCell.CellStyle = style;
                            //设置单元格的宽度
                            if (!root.head.defaultwidth.IsNullOrEmpty() && !ec.width.IsNullOrEmpty())
                                sheet.SetColumnWidth(i, ec.width * 40);
                            else
                                sheet.AutoSizeColumn(i); //每列宽度自适应
                        }
                        else if (ics.Count == columns.Count)
                        {
                            HSSFCellStyle style = ics[i];
                            newCell.CellStyle = style;
                        }
                        if (height > 20)
                            row.Height = (short)(height * 20);

                        newCell.SetCellValue(drValue);
                    }
                }
            }

            // 设置方向
            sheet.PrintSetup.Landscape = root.head.landscape;

            // 上边距
            sheet.SetMargin(MarginType.TopMargin, root.topMargin.IsNullOrEmpty() ? 1.05 : root.topMargin.GetDouble());
            // 下边距
            sheet.SetMargin(MarginType.BottomMargin, root.bottomMargin.IsNullOrEmpty() ? 0.7 : root.bottomMargin.GetDouble());
            // 左边距
            sheet.SetMargin(MarginType.LeftMargin, root.leftMargin.IsNullOrEmpty() ? 0.7 : root.leftMargin.GetDouble());
            // 右边距
            sheet.SetMargin(MarginType.RightMargin, root.rightMargin.IsNullOrEmpty() ? 0.7 : root.rightMargin.GetDouble());

            //// 页眉边距
            //sheet.SetMargin(MarginType.HeaderMargin, 1.3);
            //// 页脚边距
            //sheet.SetMargin(MarginType.FooterMargin, 1);
            // 打印居中
            sheet.HorizontallyCenter = true;

            // 设置缩放比例
            sheet.PrintSetup.Scale = 100;
            sheet.PrintSetup.FitWidth = 100;
            sheet.PrintSetup.FitHeight = 100;
            sheet.SetZoom(1, 1);
            // 设置纸张
            sheet.PrintSetup.PaperSize = 9;

            IPrintSetup ps = sheet.PrintSetup;

            // 设置顶端标题行
            Int32 endRow = root.head.rowspan.GetInt32() > 1 ? root.head.rowspan.GetInt32() - 1 : 0;
            workbook.SetRepeatingRowsAndColumns(0, 0, 0, 0, endRow);
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="root"></param>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columns"></param>
        internal void SetHeader<T>(Root root, HSSFWorkbook workbook, ISheet sheet, List<PropertyInfo> columns)
        {
            if (root.head.defaultwidth.HasValue)
            {
                //设置表格默认宽高
                sheet.DefaultColumnWidth = root.head.defaultwidth.Value; //12
            }
            if (root.head.defaultheight.HasValue)
            {
                //设置表格默认行高
                sheet.DefaultRowHeight = (short)root.head.defaultheight.Value; //25
            }

            if (!root.head.borderstyle.IsNullOrEmpty())
            {
                string bStyle = root.head.borderstyle.Trim();
                if (!string.IsNullOrEmpty(bStyle))
                {
                    switch (bStyle)
                    {
                        case "none":
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.None;
                            break;
                        case "solid":
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
                            break;
                        case "dashed":
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Dashed;
                            break;
                        case "dotted":
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Dotted;
                            break;
                        case "double":
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Double;
                            break;
                        default:
                            WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
                            break;
                    }
                }
            }
            XlPalette = workbook.GetCustomPalette();
            if (!string.IsNullOrEmpty(root.head.bordercolor))
            {
                Color co = ColorTranslator.FromHtml(root.head.bordercolor);
                XlPalette.SetColorAtIndex(HSSFColor.Plum.Index, (byte)co.R, (byte)co.G, (byte)co.B);
                WholeBorderColor = NPOI.HSSF.Util.HSSFColor.Plum.Index;//这句代码根据16进制不起作用，起到颜色初始化
            }

            //创建行
            List<IRow> rowList = new List<IRow>();
            Int32 rowN = root.head.rowspan.GetInt32();
            for (Int32 i = 0; i < rowN; i++)
            {
                IRow row = sheet.CreateRow(i);
                rowList.Add(row);
            }

            //合并单元格
            //填充内容
            for (Int32 i = 0; i < root.head.columns.Count; i++)
            {
                //读取最重要的区域,0=fromRow,1=toRow,2=fromColumn,3=toColumn
                ExportColumn ec = root.head.columns[i];
                Int32[] c = ec.cellregion.Split(',').ToIntArray();

                if (c[2] == c[3])
                {
                    PropertyInfo pi = typeof(T).GetProperty(ec.dataIndex);
                    columns.Add(pi);
                }

                if (c[0] < c[1] || c[2] < c[3])   //例如1,1,2,2 第二行中的第3列,例如1,1,2,7 第二行中的(第3列到8列),合并列
                {
                    CellRangeAddress cellr = new CellRangeAddress(c[0], c[1], c[2], c[3]);
                    sheet.AddMergedRegion(cellr);
                    //设置边框
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellr, WholeBorderStyle, WholeBorderColor);
                }
            }

            // 是否创建表头
            if (!rowList.IsNullOrEmpty() && rowList.Count > 0)
            {
                //填充内容
                for (Int32 i = 0; i < root.head.columns.Count; i++)
                {
                    //读取最重要的区域,0=fromRow,1=toRow,2=fromColumn,3=toColumn
                    ExportColumn ec = root.head.columns[i];
                    Int32[] c = ec.cellregion.Split(',').ToIntArray();
                    CellRangeAddress cellr = new CellRangeAddress(c[0], c[1], c[2], c[3]);
                    //计算text要插入的位置的索引
                    Int32 txtIndex = -1;
                    Int32 txtRow = -1;

                    ICellStyle cellStyle = GetCellStyle(workbook, ExcelStyle.Title, ec);
                    if ((c[0] == c[1] && c[2] == c[3]) || (c[0] == c[1] && c[2] < c[3]))
                    { //例如1,1,2,2 第二行中的第3列,例如1,1,2,7 第二行中的(第3列到8列)
                        txtIndex = c[2];
                        txtRow = c[0];
                        ICell newCell = rowList[txtRow].CreateCell(txtIndex);

                        //设置单元格的高度
                        if (!ec.height.IsNullOrEmpty())
                        {
                            rowList[txtRow].Height = (short)(ec.height * 20);
                        }
                        SetHeadCellBold(ec);
                        newCell.SetCellValue(ec.text);

                        //水平对齐  默认居中
                        cellStyle.Alignment = ec.halign.IsNullOrEmpty() ? HorizontalAlignment.Center : ec.halign.ToHorAlign();
                        newCell.CellStyle = cellStyle;

                        sheet.AddMergedRegion(cellr);
                        //设置边框
                        ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellr, WholeBorderStyle, WholeBorderColor);
                    }
                    if (c[0] < c[1] && c[2] == c[3]) //合并c[0]到c[1]行 ，列没变 ，   'cellregion':'0,1,1,1',
                    {
                        txtIndex = c[2];
                        txtRow = c[0];
                        ICell newCell = rowList[txtRow].CreateCell(txtIndex);
                        //设置单元格的高度
                        if (!ec.height.IsNullOrEmpty())
                        {
                            rowList[txtRow].Height = (short)(ec.height * 20);
                        }
                        SetHeadCellBold(ec);
                        newCell.SetCellValue(ec.text);

                        //水平对齐  默认居中
                        cellStyle.Alignment = ec.halign.IsNullOrEmpty() ? HorizontalAlignment.Center : ec.halign.ToHorAlign();
                        newCell.CellStyle = cellStyle;
                        sheet.AddMergedRegion(cellr);
                        //设置边框
                        ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellr, WholeBorderStyle, WholeBorderColor);
                    }
                    if (c[0] < c[1] && c[2] < c[3]) //合并c[0]到c[1]行 ，列没变 ，   'cellregion':'4,5,2,4',
                    {
                        txtIndex = c[2];
                        txtRow = c[0];
                        ICell newCell = rowList[txtRow].CreateCell(txtIndex);
                        //设置单元格的高度
                        if (!ec.height.IsNullOrEmpty())
                        {
                            rowList[txtRow].Height = (short)(ec.height * 20);
                        }
                        SetHeadCellBold(ec);
                        newCell.SetCellValue(ec.text);

                        //水平对齐  默认居中
                        cellStyle.Alignment = ec.halign.IsNullOrEmpty() ? HorizontalAlignment.Center : ec.halign.ToHorAlign();
                        newCell.CellStyle = cellStyle;
                    }

                    //设置单元格的宽度
                    if (!root.head.defaultwidth.IsNullOrEmpty() && !ec.width.IsNullOrEmpty())
                        sheet.SetColumnWidth(i, ec.width * 40);
                    else
                        sheet.AutoSizeColumn(i); //每列宽度自适应
                }
            }
        }

        /// <summary>
        /// web导出
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fileName"></param>
        private void Export(IWorkbook workbook, String fileName)
        {
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                HttpContext content = HttpContext.Current;

                // 设置编码和附件格式
                content.Response.ContentType = "application/vnd.ms-excel";
                content.Response.ContentEncoding = Encoding.UTF8;
                content.Response.Charset = "";
                content.Response.AppendHeader("Content-Disposition",
                    "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".xls");

                content.Response.BinaryWrite(ms.GetBuffer());
                content.Response.End();
            }
        }
        /// <summary>
        /// excel样式
        /// </summary>
        public enum ExcelStyle
        {
            /// <summary>
            /// 头
            /// </summary>
            Header,
            /// <summary>
            /// 标题
            /// </summary>
            Title,
            /// <summary>
            /// 链接
            /// </summary>
            Url,
            /// <summary>
            /// 时间
            /// </summary>
            Date,
            /// <summary>
            /// 数字
            /// </summary>
            Number,
            /// <summary>
            /// 货币
            /// </summary>
            Decimal,
            /// <summary>
            /// 百分比
            /// </summary>
            Percentage,
            /// <summary>
            /// 科学计数
            /// </summary>
            ScientificNotation,
            /// <summary>
            /// 默认
            /// </summary>
            Default
        }

        /// <summary>
        /// 单元格样式
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sty"></param>
        /// <param name="ec"></param>
        /// <returns></returns>
        private ICellStyle GetCellStyle(IWorkbook wb, ExcelStyle sty, ExportColumn ec)
        {
            ICellStyle newStyle = wb.CreateCellStyle();
            //边框  
            newStyle.BorderRight = newStyle.BorderLeft = newStyle.BorderBottom = newStyle.BorderTop = WholeBorderStyle;
            newStyle.BottomBorderColor = newStyle.RightBorderColor = newStyle.LeftBorderColor = newStyle.TopBorderColor = WholeBorderColor;

            //水平对齐  默认居中
            newStyle.Alignment = ec.align.IsNullOrEmpty() ? HorizontalAlignment.Center : ec.align.ToHorAlign();

            //垂直对齐 默认顶部
            newStyle.VerticalAlignment = ec.valign.IsNullOrEmpty() ? VerticalAlignment.Center : ec.valign.ToVerAlign();

            ////默认填充整个背景颜色
            //newStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;

            //自动换行  
            newStyle.WrapText = true;

            //缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对  
            newStyle.Indention = 1;

            //定义几种字体  
            var font = wb.CreateFont();
            Boolean fontc = string.IsNullOrEmpty(ec.fontcolor); //是否有字体颜色            

            font.FontName = String.IsNullOrWhiteSpace(ec.fontName) ? "宋体" : ec.fontName;//设置字体为宋体
            font.IsItalic = ec.IsItalic.HasValue && ec.IsItalic.Value ? true : false;//是否是斜体
            font.IsStrikeout = ec.IsStrikeout.HasValue && ec.IsStrikeout.Value ? true : false;//是否有中间线
            font.Underline = ec.Underline.IsNullOrEmpty() && ec.Underline.IsNullOrEmpty() ? FontUnderlineType.None : FontUnderlineType.Single;//设置下划线
            font.Boldweight = HeadFontWeight;
            font.FontHeightInPoints = ec.fontsize ?? 8; //设置字体大小;
            newStyle.SetFont(font);
            //上面基本都是设共公的设置  
            //下面列出了常用的字段类型  
            switch (sty)
            {
                case ExcelStyle.Header:
                    newStyle.Alignment = HorizontalAlignment.Center;
                    var headerFont = wb.CreateFont();
                    headerFont.FontHeightInPoints = 20;
                    headerFont.Boldweight = 700;
                    newStyle.SetFont(headerFont);
                    break;
                case ExcelStyle.Title:
                    newStyle.Alignment = HorizontalAlignment.Center;
                    var titleFont = wb.CreateFont();
                    titleFont.FontHeightInPoints = 10;
                    titleFont.Boldweight = 10;
                    newStyle.SetFont(titleFont);
                    break;
                case ExcelStyle.Date:
                    IDataFormat datastyle = wb.CreateDataFormat();

                    newStyle.DataFormat = datastyle.GetFormat("yyyy/MM/dd HH:mm");
                    newStyle.SetFont(font);
                    break;
                case ExcelStyle.Number:
                    newStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");
                    newStyle.SetFont(font);
                    break;
                case ExcelStyle.Decimal:
                    var format = wb.CreateDataFormat();
                    newStyle.DataFormat = format.GetFormat("￥#,##0");
                    newStyle.SetFont(font);
                    break;
                case ExcelStyle.Url:
                    var fontcolorblue = wb.CreateFont();
                    fontcolorblue.Color = HSSFColor.Blue.Index;
                    fontcolorblue.FontName = String.IsNullOrWhiteSpace(ec.fontName) ? "宋体" : ec.fontName;//设置字体为宋体;
                    fontcolorblue.Underline = FontUnderlineType.Single;
                    newStyle.SetFont(fontcolorblue);
                    break;
                case ExcelStyle.Percentage:
                    newStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    newStyle.SetFont(font);
                    break;
                case ExcelStyle.ScientificNotation:
                    newStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    newStyle.SetFont(font);
                    break;
                case ExcelStyle.Default:
                    newStyle.SetFont(font);
                    break;
            }

            return newStyle;
        }

        /// <summary>
        /// 设置表头单元格字体是否加粗，默认加粗
        /// </summary>
        /// <param name="al"></param>
        private void SetHeadCellBold(ExportColumn al)
        {
            switch (al.fontweight)
            {
                case "bold":
                    HeadFontWeight = (short)FontBoldWeight.Bold;
                    break;
                case "none":
                    HeadFontWeight = (short)FontBoldWeight.None;
                    break;
                case "normal":
                    HeadFontWeight = (short)FontBoldWeight.Normal;
                    break;
                default:
                    HeadFontWeight = (short)FontBoldWeight.Normal;
                    break;
            }
        }

        #endregion

        #region 读取excel
        /// <summary>
        /// 从excel读取为dataset
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="HeaderRowIndex"></param>
        /// <returns></returns>
        public static DataSet RenderDataSetFromExcel(String FilePath, int HeaderRowIndex = 0)
        {
            HSSFWorkbook workbook;
            using (var excelFileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(excelFileStream);
            }

            var ds = new DataSet();
            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                var sheet = workbook.GetSheetAt(sheetIndex);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                var dt = new DataTable(sheet.SheetName);

                var headerRow = sheet.GetRow(HeaderRowIndex);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    dt.Columns.Add(column);
                }

                var rowCount = sheet.LastRowNum;

                for (var j = (sheet.FirstRowNum + 1); j <= sheet.LastRowNum; j++)
                {
                    var row = sheet.GetRow(j);
                    var dr = dt.NewRow();
                    for (var i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = row.GetCell(i);

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }

                ds.Tables.Add(dt);
            }


            return ds;
        }
        /// <summary>
        /// 从excel读取为datatable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerRowIndex"></param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(String filePath, int sheetIndex = 0, int headerRowIndex = 0)
        {
            HSSFWorkbook workbook;
            using (var excelFileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(excelFileStream);
            }

            var sheet = workbook.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            var dt = new DataTable(sheet.SheetName);

            var headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                dt.Columns.Add(column);
            }

            var rowCount = sheet.LastRowNum;

            for (int j = (sheet.FirstRowNum + 1); j < sheet.LastRowNum; j++)
            {
                var row = sheet.GetRow(j);
                var dr = dt.NewRow();
                for (var i = 0; i < row.LastCellNum; i++)
                {
                    var cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
    public static class ExeclUtility
    {
        internal static Int32[] ToIntArray(this String[] region)
        {
            ArrayList aList = new ArrayList();
            foreach (String i in region)
                aList.Add(Convert.ToInt32(i));
            return (Int32[])aList.ToArray(typeof(Int32));
        }

        internal static HorizontalAlignment ToHorAlign(this string str)
        {
            switch (str.ToLower())
            {
                case "center":
                    return HorizontalAlignment.Center;
                    break;
                case "left":
                    return HorizontalAlignment.Left;
                    break;
                case "right":
                    return HorizontalAlignment.Right;
                    break;
                default:
                    return HorizontalAlignment.Center;
                    break;
            }
            return HorizontalAlignment.Center;
        }

        internal static VerticalAlignment ToVerAlign(this string str)
        {
            switch (str.ToLower())
            {
                case "center":
                    return VerticalAlignment.Center;
                    break;
                case "top":
                    return VerticalAlignment.Top;
                    break;
                case "bottom":
                    return VerticalAlignment.Bottom;
                    break;
                default:
                    return VerticalAlignment.Center;
                    break;
            }
            return VerticalAlignment.Center;
        }
    }

    public class OutPDF
    {
        private static System.Drawing.Bitmap bitmap;
        private static string url;
        private static int w = 760, h = 900;
        //public static void setBitmap()
        //{
        //    using (WebBrowser wb = new WebBrowser())//应该是new的问题？。。
        //    {
        //        wb.Width = w;
        //        wb.Height = h;
        //        wb.ScrollBarsEnabled = false;

        //        wb.Navigate(url);
        //        ////确保页面被解析完全
        //        //while (wb.ReadyState != WebBrowserReadyState.Complete)
        //        //{
        //        //    System.Windows.Forms.Application.DoEvents();
        //        //}
        //        bitmap = new System.Drawing.Bitmap(w, h);
        //        //wb.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, w, h));
        //        wb.Dispose();
        //    }
        //}
        public static void CreatPdf()
        {
            Document doc = new Document(PageSize.A4, 9, 18, 36, 36);//左右上下
            MemoryStream ms = new MemoryStream();
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.CloseStream = false;
                doc.Open();
                //url = "http://localhost:5026/Orders/SeeOneOrder?Order_Id=31";
                //Thread thread = new Thread(new ThreadStart(setBitmap));
                //thread.SetApartmentState(ApartmentState.STA);
                //thread.Start();
                //while (thread.IsAlive)
                //    Thread.Sleep(100);
                bitmap.Save("t.bmp");

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);
                img.ScalePercent(75);//560 630
                doc.Add(img);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                doc.Close();
                //using (FileStream fs = new FileStream("out.pdf", FileMode.Create))
                //{
                ms.Position = 0;
                byte[] bit = new byte[ms.Length];
                ms.Read(bit, 0, (int)ms.Length);
                //fs.Write(bit, 0, bit.Length);
                //}
                ViewPdf(ms);
            }
        }
        private static void ViewPdf(Stream fs)
        {

            HttpContext.Current.Response.Clear();
            //中文名的话
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" +
            //             HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ";charset=GB2312");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=out.pdf");
            HttpContext.Current.Response.AddHeader("Content-Length", fs.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/pdf";
            long fileLength = fs.Length;
            int size = 10240;//10K一--分块下载，10K为1块
            byte[] readData = new byte[size];
            if (size > fileLength)
                size = Convert.ToInt32(fileLength);
            long fPos = 0;
            bool isEnd = false;
            while (!isEnd)
            {
                if ((fPos + size) >= fileLength)
                {
                    size = Convert.ToInt32(fileLength - fPos);
                    isEnd = true;
                }
                readData = new byte[size];
                fs.Position = fPos;
                fs.Read(readData, 0, size);
                HttpContext.Current.Response.BinaryWrite(readData);
                HttpContext.Current.Response.OutputStream.Flush();
                fPos += size;
            }
            fs.Close();
            HttpContext.Current.Response.OutputStream.Close();
            HttpContext.Current.Response.End();//非常重要，没有这句的话，页面的HTML代码将会保存到文件中
            HttpContext.Current.Response.Close();
        }
    }
}
