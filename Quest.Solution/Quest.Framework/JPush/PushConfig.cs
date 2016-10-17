using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SuHui.Framework.JPush
{
    public class PushConfig
    {
        public static String app_key = ConfigurationManager.AppSettings["AppKey"];
        public static String master_secret = ConfigurationManager.AppSettings["MasterSecret"];
        /// <summary>
        /// 推送信息到指定设备
        /// </summary>
        /// <param name="userRIds">推送给多个设备id，参数为："xxxxx1","xxxxxx2","xxxxxx3"</param>
        /// <param name="msg">推送信息</param>
        /// <returns>返回操作结果</returns>
        public static String Send(String[] userRIds, String msg)
        {
            String r = String.Empty;
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.s_registrationId(userRIds);
            //pushPayload.audience = Audience.s_tag_and("tag1", "tag_all");
            var notification = new Notification();
            notification.IosNotification = new IosNotification().setAlert(msg).setBadge(5).setSound("happy").AddExtra("from", "JPush");
            pushPayload.notification = notification;

            JPushClient client = new JPushClient(app_key, master_secret);
            var result = client.SendPush(pushPayload);
            return r;
        }

        /// <summary>
        /// 推送信息为广播
        /// </summary>
        /// <param name="msg">推送信息</param>
        /// <returns>返回操作结果</returns>
        public static String Send(String msg)
        {
            String r = String.Empty;
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.all();
            //pushPayload.audience = Audience.s_tag_and("tag1", "tag_all");
            var notification = new Notification();
            notification.IosNotification = new IosNotification().setAlert(msg).setBadge(5).setSound("happy").AddExtra("from", "JPush");
            pushPayload.notification = notification;

            JPushClient client = new JPushClient(app_key, master_secret);
            var result = client.SendPush(pushPayload);
            return r;
        }
    }
}
