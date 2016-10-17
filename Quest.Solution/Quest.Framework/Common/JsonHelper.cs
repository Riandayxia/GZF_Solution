/*  作者：       RaindayXia
*  创建时间：   2013/6/9 23:10:11
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;

namespace Quest.Framework
{
    /// <summary>s
    /// 提供了一个关于json的辅助类
    /// </summary>
    public static class JsonHelper
    {
        #region Method
        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="obj">集合对象</param>
        /// <returns>转换的Json格式字符串</returns>
        public static String GetJson<T>(T obj)
        {
            if (obj == null) return "[]";
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);

                String szJson = Encoding.UTF8.GetString(stream.ToArray());

                //替换Json的Date字符串 
                //String p = @"\\/Date\((\d+)\+\d+\)\\/";
                //MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                //Regex reg = new Regex(p);
                //szJson = reg.Replace(szJson, matchEvaluator);
                return szJson;
            }
        }
        /// <summary>
        /// 字符串转换为list集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="josn"></param>
        /// <returns></returns>
        public static List<T> ConvertJsonList<T>(String json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            List<T> items = js.Deserialize<List<T>>(json);

            return items;
        }
        private static String ConvertJsonDateToDateString(Match m)
        {
            String result = String.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="szJson">Json字符串</param>
        /// <returns>实体</returns>
        public static T ParseFromJson<T>(String szJson)
        {
            T result;
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                result = (T)serializer.ReadObject(ms);
                return result;
            }
        }

        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns></returns>
        public static Dictionary<String, Object> JsonToDictionary(String jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<String, Object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>   
        /// 根据传入的Json数据，解码为对象(一个)   
        /// </summary>   
        /// <typeparam name="T"></typeparam>   
        /// <param name="data"></param>   
        /// <returns></returns>   
        public static T DecodeObject<T>(string data)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            AddIsoDateTimeConverter(serializer);
            StringReader sr = new StringReader(data);
            return (T)serializer.Deserialize(sr, typeof(T));
        }

        /// <summary>   
        /// 添加时间转换器   
        /// </summary>   
        /// <param name="serializer"></param>   
        private static void AddIsoDateTimeConverter(JsonSerializer serializer)
        {
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            //定义时间转化格式   
            idtc.DateTimeFormat = "yyyy-MM-dd";
            //idtc.DateTimeFormat = "yyyy-MM-dd";   
            serializer.Converters.Add(idtc);
        }

        private static String ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        public static T JsonDeserializes<T>(String jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"或带T的时间格式字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"(\d{4})-(\d{2})-(\d{2})(T|\s)((\d{2}:\d{2}:\d{2}(.\d{3}|.\d{2}|\s|))|\s|)";
            //string p = @"\d{4}-\d{2}-\d{2}(\s\d{2}:\d{2}:\d{2}|\s|)";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary>
        /// 获取Json的List集合（此方法需要约定：每个对象json字符串用♀分割）
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <returns>实体集合</returns>
        public static List<T> GetObjectList<T>(String strJson)
        {
            List<T> oList = new List<T>();
            if (String.IsNullOrEmpty(strJson)) return oList;

            string p = @"(\d{4})-(\d{2})-(\d{2})(T|\s)((\d{2}:\d{2}:\d{2}(.\d{3}|.\d{2}|\s|))|\s|)";
            //string p = @"\d{4}-\d{2}-\d{2}(\s\d{2}:\d{2}:\d{2}|\s|)";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            strJson = reg.Replace(strJson, matchEvaluator);

            if (strJson.IndexOf('♀') == -1)
            {
                oList.Add(ParseFromJson<T>(strJson));
            }
            else
            {
                String[] arrs = strJson.Split('♀');
                foreach (String str in arrs)
                {
                    oList.Add(ParseFromJson<T>(str));
                }
            }
            return oList;
        }
        /// <summary>
        /// 获取Json的List集合
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <param name="divide">Json字符串分隔符</param>
        /// <returns>实体集合</returns>
        public static List<T> GetObjectList<T>(String strJson, Char divide)
        {
            List<T> oList = new List<T>();
            if (String.IsNullOrEmpty(strJson)) return oList;
            if (strJson.IndexOf(divide) == -1)
            {
                oList.Add(ParseFromJson<T>(strJson));
            }
            else
            {
                String[] arrs = strJson.Split(divide);
                foreach (String str in arrs)
                {
                    oList.Add(ParseFromJson<T>(str));
                }
            }
            return oList;
        }
        /// <summary>
        /// 得到实体类所有字段，用于model字段
        /// </summary>
        /// <param name="jsonObject">Object对象</param>
        /// <returns>处理后的字符串</returns>
        public static String GetFields(Object jsonObject)
        {
            StringBuilder jsonStr = new StringBuilder();
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (Int32 i = 0; i < propertyInfo.Length; i++)
            {
                jsonStr.Append("\"");
                jsonStr.Append(propertyInfo[i].Name);
                jsonStr.Append("\",");
            }
            return DeleteLast(jsonStr.ToString());
        }
        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject">对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(Object jsonObject)
        {
            var sw = new StringWriter();
            var isoConvert = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            isoConvert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //if (FormatDate)
            var jsonConverter = new JsonConverter[] { isoConvert };


            var serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    Converters = jsonConverter,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                }
                );
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                //if (FormatJson != false)
                jsonWriter.Formatting = Formatting.Indented;
                serializer.Serialize(jsonWriter, jsonObject);
            }
            return sw.ToString();
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static String ToJson(Object t, Boolean HasNullIgnore)
        {
            if (HasNullIgnore)
                return JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// 普通集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static String ToArrayString(IEnumerable array)
        {
            String jsonString = "[";
            foreach (Object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// 删除结尾字符
        /// </summary>
        /// <param name="str">需要删除的字符</param>
        /// <returns>完成后的字符串</returns>
        public static String DeleteLast(String str)
        {
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(DataTable table)
        {
            String jsonString = "[";
            DataRowCollection drc = table.Rows;
            for (Int32 i = 0; i < drc.Count; i++)
            {
                jsonString += "{";
                foreach (DataColumn column in table.Columns)
                {
                    jsonString += "\"" + ToJson(column.ColumnName) + "\":";
                    if (column.DataType == typeof(DateTime))
                    {
                        jsonString += "\"" + (drc[i][column.ColumnName]).ToString() + "\",";//ToJson(drc[i][column.ColumnName].ToString())
                    }
                    else if (column.DataType == typeof(String))
                    {
                        jsonString += "\"" + ToJson(drc[i][column.ColumnName].ToString()) + "\",";
                    }
                    else
                    {
                        jsonString += ToJson(drc[i][column.ColumnName].ToString()) + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// DataReader转换为Json
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(IDataReader dataReader)
        {
            String jsonString = "[";
            while (dataReader.Read())
            {
                jsonString += "{";

                for (Int32 i = 0; i < dataReader.FieldCount; i++)
                {
                    if ((String.IsNullOrEmpty(dataReader[i].ToString()) && dataReader.GetFieldType(i) == typeof(DateTime)) == false)
                    {
                        jsonString += @"""" + ToJson(dataReader.GetName(i)) + @""":";
                        if (dataReader.GetFieldType(i) == typeof(String))
                            jsonString += @"""" + ToJson(dataReader[i].ToString()) + @""",";
                        else if (dataReader.GetFieldType(i) == typeof(DateTime))
                            jsonString += @"""" + ((DateTime)dataReader[i]).ToString("yyyy-MM-dd HH:mm:ss") + @""",";
                        else if (dataReader.GetFieldType(i) == typeof(Boolean))
                            jsonString += Convert.ToString(dataReader[i]).ToLower() + ",";
                        else
                            jsonString += ToJson(dataReader[i].ToString()) + ",";
                    }
                }
                jsonString += "},";
            }
            dataReader.Close();
            jsonString = jsonString.TrimEnd(',');
            return jsonString += "]";
        }
        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(DataSet dataSet)
        {
            if (dataSet != null)
            {
                String jsonString = "{";
                foreach (DataTable table in dataSet.Tables)
                {
                    jsonString += "\"" + ToJson(table.TableName) + "\":" + ToJson(table) + ",";
                }
                return jsonString = DeleteLast(jsonString) + "}";
            }
            return "{\"Table\":{}}";
        }
        /// <summary>
        /// String转换为Json
        /// </summary>
        /// <param name="value">String对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            String temstr;
            temstr = value;
            temstr = temstr.Replace("{", "｛").Replace("}", "｝").Replace(":", "：").Replace(",", "，").Replace("[", "【").Replace("]", "】").Replace(";", "；").Replace("\n", "<br/>").Replace("\r", "");

            temstr = temstr.Replace("\t", "   ");
            temstr = temstr.Replace("'", "\'");
            temstr = temstr.Replace(@"\", @"\\");
            temstr = temstr.Replace("\"", "\"\"");
            return temstr;
        }
        /// <summary>
        /// 字符串过滤：空格,双引号和换行符
        /// </summary>
        /// <param name="str">传入String</param>
        /// <returns>新String</returns>
        public static String StringDispose(String str)
        {
            if (str == null || str.Length < 1) { return ""; }
            String strs = str.Replace(" ", "");
            strs = strs.Replace("'", "");
            strs = strs.Replace("\"", "");
            strs = strs.Replace("\n", "");
            strs = strs.Replace("\t", "");
            strs = strs.Replace("\r", "");
            strs = strs.Replace("\r\n", "");
            strs = strs.Replace(Environment.NewLine.ToString(), "");
            return strs;
        }
        /// <summary>
        /// 解析字符串,Json格式:[{"key":"value","key":"value"}]
        /// </summary>
        /// <param name="JsonStr">String字符串</param>
        /// <returns>返回Hashtable</returns>
        public static Hashtable GetHashTableByJosn(String JsonStr)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonStr.Trim()));
            sb.Remove(0, 2);
            sb.Remove(sb.Length - 2, 2);
            String strs = sb.ToString();

            String[] strArrA = strs.Split(new char[] { ',' });

            if (strArrA == null || strArrA.Length < 1) { return null; }

            Hashtable hs = new Hashtable();
            foreach (String str in strArrA)
            {
                String[] strArrB = str.Split(new char[] { ':' });
                hs.Add(strArrB[0], strArrB[1]);
            }
            return hs;
        }
        /// <summary>
        /// 解析字符串,Json格式:[{"key":"value","key":"value"}]或[{"key":"value","key":"value"},{"key":"value","key":"value""}]
        /// </summary>
        /// <param name="JsonStr">传入Json字符串</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetDataTableByJosn(String JsonStr)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonStr.Trim()));
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            String strs = sb.ToString();

            String[] strArrA = strs.Replace("},{", "}{").Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            if (strArrA == null || strArrA.Length < 1) { return new DataTable(); }

            DataTable dt = new DataTable("TableA");
            Boolean ckA = true;
            foreach (String strA in strArrA)
            {
                String[] strArrB = strA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (ckA)
                {
                    foreach (String strB in strArrB)
                    {
                        String[] strArrC = strB.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        dt.Columns.Add(strArrC[0], typeof(String));
                    }
                    ckA = false;
                }
            }
            foreach (String strA in strArrA)
            {
                DataRow drAll = dt.NewRow();
                String[] strArrB = strA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String strB in strArrB)
                {
                    String[] strArrC = strB.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    drAll[strArrC[0]] += strArrC[1];
                }
                dt.Rows.Add(drAll);
            }

            return dt;
        }
        /// <summary>
        /// Json字符串转化成DataSet,传入格式:{"TableName1":"[{"key":"value","key":"value"}]","TableName2":"[{"key":"value","key":"value"}]"}
        /// </summary>
        /// <param name="JsonString"></param>
        /// <returns>返回DataSet</returns>
        public static DataSet GetDataSetByJson(String JsonString)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonString).Trim());
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            String strs = sb.ToString();
            String[] strArrA = strs.Split(new String[] { "]," }, StringSplitOptions.RemoveEmptyEntries);
            if (strArrA == null || strArrA.Length < 1) { return null; }
            DataSet ds = new DataSet("DataSetA");
            foreach (String strA in strArrA)
            {
                String[] strArrB = strA.Split(new String[] { ":[" }, StringSplitOptions.RemoveEmptyEntries);
                Int32 a = 0;
                DataTable dt = new DataTable();
                foreach (String strB in strArrB)
                {
                    if (a == 0)
                    {
                        dt.TableName = strB.ToString();
                    }
                    else
                    {
                        String[] strArrC = strB.Replace("]", "").Replace("},{", "}{").Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                        Boolean ckA = true;
                        foreach (String strC in strArrC)
                        {
                            String[] strArrD = strC.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (ckA)
                            {
                                foreach (String strD in strArrD)
                                {
                                    String[] strArrE = strD.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    dt.Columns.Add(strArrE[0], typeof(String));
                                }
                                ckA = false;
                            }
                        }
                        foreach (String strC in strArrC)
                        {
                            DataRow drAll = dt.NewRow();
                            String[] strArrD = strC.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (String strD in strArrD)
                            {
                                String[] strArrE = strD.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                drAll[strArrE[0]] += strArrE[1];
                            }
                            dt.Rows.Add(drAll);
                        }
                    }
                    a += 1;
                }
                ds.Tables.Add(dt);
            }
            return ds;
        }
        /// <summary>
        /// DataSet转化成Json格式的字符串
        /// </summary>
        /// <param name="ds">传入DataSet</param>
        /// <returns>Json字符串</returns>
        public static String GetJsonByDataSet(DataSet ds)
        {
            if (ds == null) { return ""; }
            StringBuilder strA = new StringBuilder();
            strA.Append("{");
            foreach (DataTable dt in ds.Tables)
            {
                strA.Append("\"" + dt.TableName.ToString() + "\":\"[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {
                        strA.Append("{");
                        for (Int32 j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j < dt.Columns.Count - 1)
                            {
                                strA.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":"
                                    + "\"" + dt.Rows[i][j].ToString() + "\",");
                            }
                            else if (j == dt.Columns.Count - 1)
                            {
                                strA.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":"
                                    + "\"" + dt.Rows[i][j].ToString() + "\"");
                            }
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            strA.Append("}");
                        }
                        else
                        {
                            strA.Append("},");
                        }
                    }
                }
                else
                {
                    return null;
                }
                strA.Append("]\",");
            }
            strA.Remove(strA.Length - 1, 1);
            strA.Append("}");
            return strA.ToString();
        }
        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!strJson.IsNullOrEmpty())
                return JsonConvert.DeserializeObject<T>(strJson);
            return null;
        }
        #endregion
    }
}
