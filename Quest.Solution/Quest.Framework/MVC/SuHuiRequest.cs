using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

namespace Quest.Framework
{
    public class QuestRequest
    {
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static String Get(String objName)
        {
            return Utils.ChkSql(HttpContext.Current.Request[objName]);
        }
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static String Get(String objName, String defaultValue)
        {
            if (HttpContext.Current.Request[objName] == null)
                return defaultValue;

            return Utils.ChkSql(HttpContext.Current.Request[objName]);
        }
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Guid GetGuid(String objName)
        {
            return Get(objName).GetGuid();
        }
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Int32 GetInt(String objName)
        {
            return Get(objName).GetInt32();
        }
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Decimal GetDecimal(String objName)
        {
            return Get(objName).GetDecimal();
        }

        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Double GetDouble(String objName)
        {
            return Get(objName).GetDouble();
        }
        /// <summary>
        /// 取得get值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(String objName)
        {
            return Get(objName).GetDateTime();
        }
        /// <summary>
        /// 取得多个值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static String[] GetArray(String objName)
        {
            if (HttpContext.Current.Request[objName] == null) return null;
            if (String.IsNullOrEmpty(Get(objName))) return new String[] { };
            return Get(objName).Split(',');
        }

        /// <summary>
        /// 获取指定数据值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static List<Guid> GetGuids(String objName)
        {
            if (HttpContext.Current.Request[objName] == null) return null;
            if (String.IsNullOrEmpty(Get(objName))) return new List<Guid>();
            String[] id = Get(objName).Split(',');
            List<Guid> ids = new List<Guid>();
            foreach (String item in id)
            {
                ids.Add(item.GetGuid());
            };
            return ids;
        }
        /// <summary>
        /// 转换前段传入参数为指定IList<T>集合
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="json">前段Json数据</param>
        /// <returns>返回操作结果</returns>
        public static IList<T> GetList<T>(String json)
        {
            if (Get(json).IsNullOrEmpty())
                return null;

            IList<T> result = JsonHelper.ParseFromJson<IList<T>>(Get(json));
            return result;
        }
        /// <summary>
        /// 取得Boolean值
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Boolean GetBoolean(String objName)
        {
            if (HttpContext.Current.Request[objName] == null) return false;
            return Get(objName).ToLower() == "true" || Get(objName).ToLower() == "1" ? true : false;
        }
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        public static Boolean IsPost
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod.Equals("POST");
            }
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        public static Boolean IsGet
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod.Equals("GET");
            }
        }
        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static String GetPageName()
        {
            var urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }
        /// <summary>
        /// 获得当前页面绝对路径
        /// </summary>
        /// <returns></returns>
        public static String GetAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }
        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static Int32 GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }
        /// <summary>
        /// 取得post过来的json对象并转换成字符串
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static String GetJson(Stream inputStream)
        {
            if (inputStream.Length <= 0) return null;
            var streamReader = new StreamReader(inputStream);
            return streamReader.ReadToEnd();
        }
    }
}
