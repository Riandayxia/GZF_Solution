using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.Framework
{
    /// <summary>
    ///     公共辅助操作类
    /// </summary>
    public static class PublicHelper
    {
        #region 公共方法

        /// <summary>
        ///     检验参数合法性，数值类型不能小于0，引用类型不能为null，否则抛出相应异常
        /// </summary>
        /// <param name="arg"> 待检参数 </param>
        /// <param name="argName"> 待检参数名称 </param>
        /// <param name="canZero"> 数值类型是否可以等于0 </param>
        /// <exception cref="ComponentException" />
        public static void CheckArgument(object arg, String argName, bool canZero = false)
        {
            if (arg.IsNullOrEmpty())
            {
                ArgumentNullException e = new ArgumentNullException(argName);
                throw ThrowComponentException(String.Format("参数 {0} 为空引发异常。", argName), e);
            }
            Type type = arg.GetType();
            if (type.IsValueType && type.IsNumeric())
            {
                bool flag = !canZero ? arg.CastTo(0.0) <= 0.0 : arg.CastTo(0.0) < 0.0;
                if (flag)
                {
                    ArgumentOutOfRangeException e = new ArgumentOutOfRangeException(argName);
                    throw ThrowComponentException(String.Format("参数 {0} 不在有效范围内引发异常。具体信息请查看系统日志。", argName), e);
                }
            }
            if (type == typeof(Guid) && (Guid)arg == Guid.Empty)
            {
                ArgumentNullException e = new ArgumentNullException(argName);
                throw ThrowComponentException(String.Format("参数{0}为空Guid引发异常。", argName), e);
            }
        }

        /// <summary>
        ///     向调用层抛出组件异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ComponentException ThrowComponentException(String msg, Exception e = null)
        {
            if (String.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (String.IsNullOrEmpty(msg))
            {
                msg = "未知组件异常，详情请查看日志信息。";
            }
            return e == null ? new ComponentException(String.Format("组件异常：{0}", msg)) : new ComponentException(String.Format("组件异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(String msg, Exception e = null)
        {
            if (String.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (String.IsNullOrEmpty(msg))
            {
                msg = "未知数据访问层异常，详情请查看日志信息。";
            }
            return e == null
                ? new DataAccessException(String.Format("数据访问层异常：{0}", msg))
                : new DataAccessException(String.Format("数据访问层异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static BusinessException ThrowBusinessException(String msg, Exception e = null)
        {
            if (String.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (String.IsNullOrEmpty(msg))
            {
                msg = "未知业务逻辑层异常，详情请查看日志信息。";
            }
            return e == null ? new BusinessException(String.Format("业务逻辑层异常：{0}", msg)) : new BusinessException(String.Format("业务逻辑层异常：{0}", msg), e);
        }

        #endregion
    }
}
