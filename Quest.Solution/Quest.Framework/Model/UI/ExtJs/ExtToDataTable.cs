using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SuHui.Framework.Model.UI.ExtJs
{
    public class ExtToDataTable
    {
        /// <summary>
        /// 得到DataTable所有字段，用于model字段(简洁版)
        /// </summary>
        /// <param name="jsonObject">DataTable</param>
        /// <returns>处理后的字符串</returns>
        public static List<String> GetFields(DataTable dt)
        {
            List<String> items = new List<String>();

            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    items.Add(dt.Columns[i].ColumnName);
                }
            }

            return items;
        }
        /// <summary>
        /// 得到DataTable的列
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>列名</returns>
        public static List<ExtColumn> GetColumn(DataTable dt)
        {
            List<ExtColumn> items = new List<ExtColumn>();
            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    items.Add(new ExtColumn() { 
                        dataIndex=dt.Columns[i].ColumnName,
                        text = dt.Columns[i].ColumnName,
                        flex=1
                    });
                }
            }

            return items;
        }
    }
}
