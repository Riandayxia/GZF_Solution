using System;
using System.Web;

namespace Quest.Framework
{
    /// <summary>
    /// Session 操作类
    /// 1、GetSession(String name)根据session名获取session对象
    /// 2、SetSession(String name, Object val)设置session
    /// </summary>
    public class SessionHelper
    {
        private static Int32 Timeout = 20;
        /// <summary>
        /// 根据session名获取session对象
        /// 调动有效期为20分钟
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Object GetSession(String name)
        {
            HttpContext.Current.Session.Timeout = Timeout;
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 设置session
        /// 调动有效期为20分钟
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public static void SetSession(String name, Object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
            HttpContext.Current.Session.Timeout = Timeout;
        }

        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void Add(String strSessionName, String strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = Timeout;
        }

        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValues">Session值数组</param>
        public static void Adds(String strSessionName, String[] strValues)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = Timeout;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(String strSessionName, String strValue, Int32 iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValues">Session值数组</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Adds(String strSessionName, String[] strValues, Int32 iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static Object Get(String strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值数组</returns>
        public static Object[] Gets(String strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (Object[])HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Del(String strSessionName)
        {
            if (!HttpContext.Current.Session.IsNullOrEmpty())
                HttpContext.Current.Session[strSessionName] = null;
        }
    }
}
