using SuHui.Core.Models.Account;
using SuHui.Core.Models.Security;
using SuHui.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuHui.Core.Account
{
    /// <summary>
    ///     账户模块核心业务契约
    /// </summary>
    public interface IAccountService
    {
        #region 属性

        /// <summary>
        /// 获取 用户信息查询数据集合
        /// </summary>
        IQueryable<Member> QueryMembers { get; }

        /// <summary>
        /// 获取 用户扩展信息查询数据集合
        /// </summary>
        IQueryable<MemberExtend> QueryMemberExtend { get; }

        /// <summary>
        /// 获取 登陆记录信息查询数据集合
        /// </summary>
        IQueryable<LoginLog> QueryLoginLog { get; }

        /// <summary>
        /// 获取 角色信息查询数据集合
        /// </summary>
        IQueryable<Role> QueryRole { get; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Login(LoginInfo loginInfo);

        #endregion
    }
}
