using Quest.Core.Models.Base;
using Quest.Framework;
using Quest.Framework.MVC;
using Quest.Mobile.CommonSupport.Filter;
using Quest.Mobile.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Quest.Mobile.Controllers.Base
{
    /// <summary>
    /// 数据字典 控制层
    /// </summary>
    [MenuDetail(Title = "用户管理", MType = MenuType.Menu, Icon = "yhgl", Use = MenuUse.All)]
    public partial class UserController
    {
        #region 属性

        #endregion

        #region 视图功能
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Registered(User entity)
        {
            OperationResult or = UserService.InsertRegistered(entity);
            return this.JsonFormat(or);
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Reset(User entity)
        {
            OperationResult or = UserService.Reset(entity);
            return this.JsonFormat(or);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthorize]
        [HttpPost]
        public ActionResult LoginPhone(LoginModel model)
        {
            User user = new User() { LoginName = model.Account, Password = model.Password };
            OperationResult result = UserService.Login(user);
            if (result.ResultType == OperationResultType.Success)
            {
                User member = (User)result.AppendData;
                CurrentUser.Save(member);
                //写入cookie
                WriteCookie(model, user);
            }
            return this.JsonFormat(result);
        }

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="user"></param>
        private void WriteCookie(LoginModel model, User user)
        {
            FormsAuthentication.SetAuthCookie(user.LoginName, true, FormsAuthentication.FormsCookiePath);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1, user.LoginName, DateTime.Now, DateTime.Now.AddMinutes(5), false, user.LoginName);
            Task t1 = Task.Factory.StartNew(delegate
            {
                // generate new identity
                FormsIdentity identity = new FormsIdentity(ticket);
            });
            Task t2 = Task.Factory.StartNew(delegate
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                // write to client.
                Response.Cookies.Add(cookie);
            });
            Task.WaitAll(t1, t2);
        }
        public ActionResult GetByLoginId()
        {
            Guid dicKey = QuestRequest.GetGuid("dicKey");
            return this.JsonFormat(UserService.GetByLoginId(dicKey));
        }
        #endregion
    }
}