/*  作者：       RaindayXia
*  创建时间：   2013/7/22 15:38:20
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Reflection;

namespace Quest.Framework
{
    /// <summary>
    /// 强制转化辅助类(无异常抛出)
    /// </summary>
    public static class ConvertHelper
    {
        #region 强制转化

        /// <summary>
        /// 将最后一个字符串的路径path替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static String Path(this String str, String path)
        {
            Int32 index = str.LastIndexOf('\\');
            Int32 indexDian = str.LastIndexOf('.');
            return str.Substring(0, index + 1) + path + str.Substring(indexDian);
        }

        public static List<String> GetList(this String ids)
        {
            List<String> listId = new List<String>();
            if (!String.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<String>(ids.Split(','));
                foreach (var item in sort)
                {
                    listId.Add(item);

                }
            }
            return listId;
        }
        ///// <summary>
        ///// IList<ExtTreeData<T, Guid>> 转换为List<T> tempItems
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="items"></param>
        ///// <param name="tempItems"></param>
        ///// <returns></returns>
        //public static List<T> GetList<T, key>(this IList<ExtTreeData<T, key>> items, List<T> tempItems)
        //{
        //    foreach (var item in items)
        //    {
        //        tempItems.Add(item.Tobject);
        //        if (!item.children.IsNullOrEmpty() && item.children.Count > 0)
        //            item.children.GetList<T, key>(tempItems);
        //    }
        //    return tempItems;
        //}

        /// <summary>
        /// 从^分割的字符串中获取多个Id,先是用 ^ 分割，再使用 & 分割
        /// </summary>
        /// <param name="ids">先是用 ^ 分割，再使用 & 分割</param>
        /// <returns></returns>
        public static List<String> GetIdSort(this String ids)
        {
            List<String> listId = new List<String>();
            if (!String.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<String>(ids.Split('^')
                    .Where(w => !String.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));
                foreach (var item in sort)
                {
                    listId.Add(item);
                }
            }
            return listId;
        }

        /// <summary>
        /// 从，分割的字符串中获取单个Id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static String GetId(this String ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<String>(ids.Split('^')
                    .Where(w => !String.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));
                foreach (var item in sort)
                {
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 将String转换为Dictionary类型，过滤掉为空的值，首先 6 分割，再 7 分割
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<String, String> StringToDictionary(String value)
        {
            Dictionary<String, String> queryDictionary = new Dictionary<String, String>();
            String[] s = value.Split('^');
            for (Int32 i = 0; i < s.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(s[i]) && !s[i].Contains("undefined"))
                {
                    var ss = s[i].Split('&');
                    if ((!String.IsNullOrEmpty(ss[0])) && (!String.IsNullOrEmpty(ss[1])))
                    {
                        queryDictionary.Add(ss[0], ss[1]);
                    }
                }

            }
            return queryDictionary;
        }

        /// <summary>
        /// 强制转化可空int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ObjToIntNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new int?(GetInt32(obj));
        }

        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值0</returns>
        public static Int32 GetInt32(this Object Value)
        {
            return GetInt32(Value, 0);
        }

        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值0</returns>
        public static Int32 GetInt32(this Object Value, Int32 defaultValue)
        {
            if (Value == null) return defaultValue;
            if (Value is String && Value.ToString().HasValue() == false) return defaultValue;

            if (Value is DBNull) return defaultValue;

            if ((Value is String) == false && (Value is IConvertible) == true)
            {
                return (Value as IConvertible).ToInt32(CultureInfo.CurrentCulture);
            }

            Int32 retVal = defaultValue;
            if (Int32.TryParse(Value.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out retVal))
            {
                return retVal;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 强制转化为long
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>long</returns>
        public static long GetLong(this object value, long defalutValue)
        {
            if (value != null)
            {
                long num;
                if (value.Equals(DBNull.Value))
                {
                    return defalutValue;
                }
                if (long.TryParse(value.ToString(), out num))
                {
                    return num;
                }
            }
            return defalutValue;
        }

        /// <summary>
        /// 强制转化为long
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>long</returns>
        public static long GetLong(this object value)
        {
            return GetLong(value, 0);
        }

        /// <summary>
        /// 得到对象的 Double 类型的值，默认值0.0
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值0.0</returns>
        public static Double GetDouble(this Object vlaue, Double defaultValue = 0.0)
        {
            if (vlaue is DBNull)
                return defaultValue;
            if (vlaue.IsNullOrEmpty())
                return defaultValue;
            Double.TryParse(vlaue.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 指定对象转换为Double
        /// 这个方法将会把不存在的值为0.0.
        /// </summary>
        /// <param name="o">被指定类型.</param>
        /// <returns> System.Double 表示的指定值.</returns>
        public static Double GetDouble(Object vlaue)
        {
            return GetDouble(vlaue, 0);
        }

        /// <summary>
        /// Object转换为Decimal
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换正常，返回转换的值，如果不能转换则返回默认值</returns>
        public static Decimal GetDecimal(this Object Value, Decimal defaultValue = 0)
        {
            Decimal.TryParse(Value.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Object转换为Decimal
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>转换正常，返回转换的值，如果不能转换则返回默认值</returns>
        public static Decimal GetDecimal(this Object Value)
        {
            Decimal decValue = 0;
            Decimal.TryParse(Value.ToString(), out decValue);
            return decValue;
        }

        /// <summary>
        /// 得到对象的 String 类型的值，默认值String.Empty
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值String.Empty</returns>
        public static String GetString(this Object Value)
        {
            return GetString(Value, String.Empty);
        }

        /// <summary>
        /// 得到对象的 String 类型的值，默认值String.Empty
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值 。</returns>
        public static String GetString(this Object Value, String defaultValue)
        {
            if (Value == null) return defaultValue;
            String retVal = defaultValue;
            try
            {
                var strValue = Value as String;
                if (strValue != null)
                {
                    return strValue;
                }

                char[] chrs = Value as char[];
                if (chrs != null)
                {
                    return new String(chrs);
                }

                retVal = Value.ToString();
            }
            catch
            {
                return defaultValue;
            }
            return retVal;
        }

        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue </returns>
        public static DateTime GetDateTime(this Object Value)
        {
            return GetDateTime(Value, DateTime.MinValue);
        }

        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回默认值为DateTime.MinValue</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue</returns>
        public static DateTime GetDateTime(this Object Value, DateTime defaultValue)
        {
            if (Value == null) return defaultValue;

            if (Value is DBNull) return defaultValue;

            String strValue = Value as String;
            if (strValue == null && (Value is IConvertible))
            {
                return (Value as IConvertible).ToDateTime(CultureInfo.CurrentCulture);
            }
            if (strValue != null)
            {
                strValue = strValue
                    .Replace("年", "-")
                    .Replace("月", "-")
                    .Replace("日", "-")
                    .Replace("点", ":")
                    .Replace("时", ":")
                    .Replace("分", ":")
                    .Replace("秒", ":")
                      ;
            }
            DateTime dt = defaultValue;
            if (DateTime.TryParse(Value.ToString(), out dt))
            {
                return dt;
            }

            return defaultValue;
        }

        /// <summary>
        /// 得到对象的布尔类型的值，默认值false
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static Boolean GetBoolean(this Object Value)
        {
            return GetBoolean(Value, false);
        }

        /// <summary>
        /// 得到对象的 Boolean 类型的值，默认值false
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static Boolean GetBoolean(this Object Value, Boolean defaultValue)
        {
            if (Value == null) return defaultValue;
            if (Value is String && Value.ToString().HasValue() == false) return defaultValue;

            if ((Value is String) == false && (Value is IConvertible) == true)
            {
                if (Value is DBNull) return defaultValue;

                try
                {
                    return (Value as IConvertible).ToBoolean(CultureInfo.CurrentCulture);
                }
                catch { }
            }

            if (Value is String)
            {
                if (Value.ToString() == "0") return false;
                if (Value.ToString() == "1") return true;
                if (Value.ToString().ToLower() == "yes") return true;
                if (Value.ToString().ToLower() == "no") return false;
            }
            ///  if (Value.GetInt(0) != 0) return true;
            Boolean retVal = defaultValue;
            if (Boolean.TryParse(Value.ToString(), out retVal))
            {
                return retVal;
            }
            else return defaultValue;
        }

        /// <summary>
        /// 检测 GuidValue 是否包含有效的值，默认值Guid.Empty
        /// </summary>
        /// <param name="GuidValue">要转换的值</param>
        /// <param name="defaultVlaue">默认指</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值Guid.Empty</returns>
        public static Guid GetGuid(this Object GuidValue, Guid defaultVlaue)
        {
            try
            {
                return new Guid(GuidValue.GetString());
            }
            catch { return defaultVlaue; }
        }

        /// <summary>
        /// 检测 value 是否包含有效的值，默认值Guid.Empty
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值Guid.Empty</returns>
        public static Guid GetGuid(this Object value)
        {
            #region __PVB__

            if (value == null)
                return Guid.Empty;

            #endregion

            return GetGuid(value, Guid.Empty);
        }

        /// <summary>
        /// 检测 Value 是否包含有效的值，默认值false
        /// </summary>
        /// <param name="Value"> 传入的值</param>
        /// <returns> 包含，返回true，不包含，返回默认值false</returns>
        public static Boolean HasValue(this String Value)
        {
            if (Value != null)
            {
                return !String.IsNullOrEmpty(Value.ToString());
            }
            else return false;
        }

        /// <summary>
        /// 获取SQL 存储过程 能够解析的Ids字符串
        /// "'''1'',''2'',''3'',''4''...''N'''"
        /// </summary>
        /// <param name="ids">"1,2,3,4...N"Id组合字符串</param>
        /// <returns>数据字符</returns>
        public static String GetIds(String ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                String[] strTemp = ids.Split(',');
                String rVlaue = "'";
                if (ids.GetGuid().Equals(Guid.Empty))
                {
                    foreach (String str in strTemp)
                    {
                        if (rVlaue.Equals("'"))
                            rVlaue += str;
                        else
                            rVlaue += "','" + str;
                    }
                    rVlaue += "'";
                    return rVlaue;
                }
                else
                    return "'" + ids + "'";
            }
            return String.Empty;
        }

        /// <summary>
        /// 获取SQL 存储过程 能够解析的Ids字符串
        /// "'''1'',''2'',''3'',''4''...''N'''"
        /// </summary>
        /// <param name="value">被转换的字符</param>
        /// <returns>解析后的字符</returns>
        public static String GetIds(this Object value)
        {
            return GetIds(value.ToString());
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static Boolean IsNullOrEmpty<T>(this T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (String.IsNullOrEmpty(data.ToString().Trim()) || data.ToString() == "")
                {
                    return true;
                }
            }

            //如果为"00000000=0000=0000=000000000000"
            if (data.GetType() == typeof(Guid))
            {
                if (data.Equals(Guid.Empty))
                    return true;
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static Boolean IsNullOrEmpty(this Object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (String.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        #endregion
    }

    /// <summary>
    /// Datable转换为List集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConvertHelper<T> where T : new()
    {
        /// <summary>
        /// 利用反射和泛型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataTable dt)
        {
            // 定义集合
            List<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            String tempName = String.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量
                    //检查DataTable是否包含此列（列名==对象的属性名）
                    if (dt.Columns.Contains(tempName))
                    {
                        //判断此属性是否有Setter
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出
                        //取值
                        Object value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }
    }
}
