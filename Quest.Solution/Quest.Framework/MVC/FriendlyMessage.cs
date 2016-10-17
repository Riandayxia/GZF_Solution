/*  作者：       RaindayXia
*  创建时间：   2013/7/23 22:48:43
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quest.Framework.MVC
{
    /// <summary>
    /// 系统的常见操作
    /// </summary>
    public enum SysOperate
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 1,
        /// <summary>
        /// 加载
        /// </summary>
        Load = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 操作
        /// </summary>
        Operate = 4,
        /// <summary>
        /// 未知
        /// </summary>
        UnkownError = 5,
        /// <summary>
        /// 审批
        /// </summary>
        Approval = 6,
        /// <summary>
        /// 启动流程
        /// </summary>
        RunWorkflow = 7
    }

    /// <summary>
    /// 系统管理模块友好的提示信息
    /// </summary>
    public enum SystemMessage
    {
        /// <summary>
        /// 加载成功
        /// </summary>
        LoadSuccess = 0,
        /// <summary>
        /// 加载失败
        /// </summary>
        LoadError = 1,
        /// <summary>
        /// 操作成功
        /// </summary>
        OperateSuccess = 2,
        /// <summary>
        /// 操作失败
        /// </summary>
        OperateError = 3,
        /// <summary>
        /// 添加成功
        /// </summary>
        AddSuccess = 4,
        /// <summary>
        /// 添加失败
        /// </summary>
        AddError = 5,
        /// <summary>
        /// 更新成功
        /// </summary>
        UpdateSuccess = 6,
        /// <summary>
        /// 更新失败
        /// </summary>
        UpdateError = 7,
        /// <summary>
        /// 删除成功
        /// </summary>
        DeleteSuccess = 8,
        /// <summary>
        /// 删除失败
        /// </summary>
        DeleteError = 9,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnkownError = 10,
        /// <summary>
        /// 审批提交成功
        /// </summary>
        ApprovalSuccess = 11,
        /// <summary>
        /// 审批提交失败
        /// </summary>
        ApprovalError = 12,
        /// <summary>
        /// 流程发起成功
        /// </summary>
        WFRunSuccess=13,
        /// <summary>
        /// 流程发起失败
        /// </summary>
        WFRunError=14
    }

    /// <summary>
    /// 用户模块界面友好的消息提示
    /// </summary>
    public enum UserMessage
    {
        /// <summary>
        /// 用户未登录
        /// </summary>
        UserNotLogin = 0,
        /// <summary>
        /// 登录成功
        /// </summary>
        UserLoginSuccess = 1,
        /// <summary>
        /// 用户登录失败
        /// </summary>
        UserLoginError = 2,
        /// <summary>
        /// 修改密码成功
        /// </summary>
        ChangePasswordSuccess = 3,
        /// <summary>
        /// 修改密码失败
        /// </summary>
        ChangePasswordError = 4,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnkonwnError = 5
    }

    #region 友好的提示信息

    /// <summary>
    /// 友好的提示信息
    /// </summary>
    public static class FriendlyMessage
    {
        #region 获取用户模块界面友好的消息提示

        /// <summary>
        /// 获取消息(根据操作类型和状态)
        /// </summary>
        /// <param name="sysOp">操作类型</param>
        /// <param name="status">执行状态</param>
        /// <returns></returns>
        public static String ToMessage(this SysOperate sysOp, Boolean status)
        {
            String message = "";
            //根据操作类型和执行状态返回消息
            switch (sysOp)
            {
                case SysOperate.Add:
                    message = status ? SystemMessage.AddSuccess.ToMessage() : SystemMessage.AddError.ToMessage();
                    break;
                case SysOperate.Load:
                    message = status ? SystemMessage.LoadSuccess.ToMessage() : SystemMessage.LoadError.ToMessage();
                    break;
                case SysOperate.Update:
                    message = status ? SystemMessage.UpdateSuccess.ToMessage() : SystemMessage.UpdateError.ToMessage();
                    break;
                case SysOperate.Delete:
                    message = status ? SystemMessage.DeleteSuccess.ToMessage() : SystemMessage.DeleteError.ToMessage();
                    break;
                case SysOperate.Operate:
                    message = status ? SystemMessage.OperateSuccess.ToMessage() : SystemMessage.OperateError.ToMessage();
                    break;
                case SysOperate.Approval:
                    message = status ? SystemMessage.ApprovalSuccess.ToMessage() : SystemMessage.ApprovalError.ToMessage();
                    break;
                case SysOperate.RunWorkflow:
                    message = status ? SystemMessage.WFRunSuccess.ToMessage() : SystemMessage.WFRunError.ToMessage();
                    break;
                case SysOperate.UnkownError:
                    message = SystemMessage.UnkownError.ToMessage();
                    break;
            }
            return message;
        }

        /// <summary>
        /// 获取系统管理模块友好提示信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static String ToMessage(this SystemMessage code)
        {
            String message = "";
            switch (code)
            {
                case SystemMessage.AddSuccess:
                    message = "添加成功!";
                    break;
                case SystemMessage.AddError:
                    message = "添加失败!";
                    break;
                case SystemMessage.DeleteSuccess:
                    message = "删除成功!";
                    break;
                case SystemMessage.DeleteError:
                    message = "删除失败!";
                    break;
                case SystemMessage.LoadSuccess:
                    message = "加载成功!";
                    break;
                case SystemMessage.LoadError:
                    message = "加载失败!";
                    break;
                case SystemMessage.OperateSuccess:
                    message = "操作成功!";
                    break;
                case SystemMessage.OperateError:
                    message = "操作失败!";
                    break;
                case SystemMessage.UpdateSuccess:
                    message = "更新成功!";
                    break;
                case SystemMessage.UpdateError:
                    message = "更新失败!";
                    break;
                case SystemMessage.ApprovalSuccess:
                    message = "审批提交成功";
                    break;
                case SystemMessage.ApprovalError:
                    message = "审批提交失败";
                    break;
                case SystemMessage.UnkownError:
                    message = "未知错误!";
                    break;
                case SystemMessage.WFRunSuccess:
                    message = "流程发起成功";
                    break;
                case SystemMessage.WFRunError:
                    message = "流程发起失败!";
                    break;
                default:
                    message = "错误";
                    break;
            }
            return message;
        }

        /// <summary>
        /// 获取用户模块界面友好的消息提示
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static String ToMessage(this UserMessage code)
        {
            String message = "";
            switch (code)
            {
                case UserMessage.UserNotLogin:
                    message = "用户未登录!";
                    break;
                case UserMessage.UserLoginSuccess:
                    message = "登录成功!";
                    break;
                case UserMessage.UserLoginError:
                    message = "用户登录失败!";
                    break;
                case UserMessage.ChangePasswordSuccess:
                    message = "修改密码成功!";
                    break;
                case UserMessage.ChangePasswordError:
                    message = "修改密码失败!";
                    break;
                case UserMessage.UnkonwnError:
                    message = "未知错误";
                    break;
                default:
                    message = "未知错误";
                    break;
            }
            return message;
        }

        #endregion
    }

    #endregion
}
