/*  作者：      RaindayXia
*  创建时间：   2012/7/19 15:42:15
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Globalization;
using System.Web.Mvc;
using Quest.Framework;
using Newtonsoft.Json;
using Quest.Framework.MVC;

namespace Quest.Mobile
{
    /// <summary>
    /// 格式化json扩展
    /// </summary>
    public static class FormatJsonExtension
    {
        /// <summary>
        /// 普通序列化(不进行UI友好的json化)
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <returns></returns>        
        public static FormatJsonResult JsonFormat(this Controller c, OperationResult or)
        {
            FormatJsonResult result = new FormatJsonResult();
            result.NotUIFriendlySerialize = false;
            result.data = or.AppendData;
            result.msg = or.Message;
            result.callbackName = "";
            result.success = false;
            if (or.ResultType.Equals(OperationResultType.Success))
            {
                result.success = true;
            }
            else
            {
                result.data = or;
            }
            return result;
        }

        /// <summary>
        /// 普通序列化(不进行UI友好的json化)
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <returns></returns>        
        public static FormatJsonResult JsonFormat(this Controller c, Object data, String callbackName = "")
        {
            FormatJsonResult result = new FormatJsonResult();
            result.NotUIFriendlySerialize = true;
            result.data = data;
            Boolean status = false;
            if (!data.IsNullOrEmpty())
                status = true;

            result.success = status;
            result.callbackName = callbackName;
            return result;
        }

        public static FormatJsonResult JsonFormat(this Controller c, Boolean status, SysOperate op, String callbackName = "")
        {
            return JsonFormat(c, null, status, op, callbackName);
        }

        /// <summary>
        /// UI友好的json格式序列化
        /// </summary>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, Object data, Boolean success, String message, String callbackName = "")
        {
            FormatJsonResult result = new FormatJsonResult();

            result.data = data;
            result.msg = message;
            result.success = success;
            result.callbackName = callbackName;
            return result;
        }

        /// <summary>
        /// 根据操作和提供的数据判断执行状态
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <param name="op">操作类型(增删改查,等等)</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, Object data, SysOperate op, String callbackName = "")
        {
            if (!data.IsNullOrEmpty())
            {
                return JsonFormatSuccess(c, data, op.ToMessage(true), callbackName);
            }
            return JsonFormatError(c, op.ToMessage(false), callbackName);
        }

        /// <summary>
        /// 根据操作和提供的数据判断执行状态
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <param name="op">操作类型(增删改查,等等)</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, Object data, Boolean status, UserMessage op, String callbackName = "")
        {
            if (!data.IsNullOrEmpty())
            {
                return JsonFormatSuccess(c, data, op.ToMessage(), callbackName);
            }
            return JsonFormatError(c, op.ToMessage(), callbackName);
        }

        /// <summary>
        /// 根据操作和提供的数据判断执行状态
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <param name="status">数据</param>
        /// <param name="op">操作类型(增删改查,等等)</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, Object data, Boolean status, SysOperate op, String callbackName = "")
        {
            if (status)
            {
                return JsonFormatSuccess(c, data, op.ToMessage(true), callbackName);
            }
            return JsonFormatError(c, op.ToMessage(false), callbackName);
        }

        /// <summary>
        /// 成功的json返回
        /// </summary>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormatSuccess(this Controller c, Object data, String message, String callbackName = "")
        {
            return JsonFormat(c, data, true, message, callbackName);
        }

        /// <summary>
        /// 失败的json返回
        /// </summary>
        /// <param name="c"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormatError(this Controller c, String message, String callbackName = "")
        {
            return JsonFormat(c, null, false, message, callbackName);
        }
    }

    /// <summary>
    /// JsonResult格式化扩展
    /// </summary>
    public class FormatJsonResult : ActionResult
    {
        /// <summary>
        /// 是否产生错误
        /// </summary>
        public Boolean success { get; set; }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public String msg { get; set; }

        /// <summary>
        /// 回掉名称
        /// </summary>
        public String callbackName { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary> 
        public Object data { get; set; }

        /// <summary>
        /// 正常序列化方式(为True则不进行UI友好的序列化)
        /// </summary>
        public Boolean NotUIFriendlySerialize { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            StringWriter sw = new StringWriter();

            JsonSerializer serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    //Converters = new JsonConverter[] { new Newtonsoft.Json.Converters.IsoDateTimeConverter() },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    //PreserveReferencesHandling = PreserveReferencesHandling.Objects
                }
            );

            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;

                if (!NotUIFriendlySerialize)
                    serializer.Serialize(jsonWriter, this);
                else
                    serializer.Serialize(jsonWriter, data);
            }

            HttpResponseBase response = context.HttpContext.Response;
            // 清除在返回前已经设置好的标头信息，这样后面的跳转才不会报错
            response.Clear(); //设置输出缓冲
            response.BufferOutput = true;
            //if (!response.IsRequestBeingRedirected)//在跳转之前做判断,防止重复
            //{
            //    response.ContentType = "application/json";
            //}

            if (callbackName.IsNullOrEmpty())
                response.Write(sw.ToString());
            else
                response.Write(callbackName + "(" + sw.ToString() + ")");
        }
    }
}


