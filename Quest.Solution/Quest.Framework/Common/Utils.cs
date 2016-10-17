using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Quest.Framework
{
    public class Utils
    {
        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static String ChkSql(String str)
        {
            var str2 = str ?? "";
            return RemoveUnsafeHtml(str2.Trim());
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static String RemoveUnsafeHtml(String content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIp(String ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static String GetIp()
        {
            String ip = String.Empty;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (String.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }

        ///// <summary>
        /////     把对象转换成Json字符串表示形式
        ///// </summary>
        ///// <param name="jsonObject"></param>
        ///// <returns></returns>
        //public static String BuildJsonString(object jsonObject)
        //{
        //    PublicHelper.CheckArgument(jsonObject, "jsonObject");
        //    return JsonConvert.SerializeObject(jsonObject);
        //}

        /// <summary>
        ///     判断指定字符串是否对象（Object）类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonObjectString(String input)
        {
            return input != null && input.StartsWith("{") && input.EndsWith("}");
        }

        /// <summary>
        ///     判断指定字符串是否集合类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonArrayString(String input)
        {
            return input != null && input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
