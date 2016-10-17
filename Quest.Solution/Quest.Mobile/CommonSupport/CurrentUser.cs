using Quest.Core.Models.Base;
using Quest.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quest.Mobile
{
    /// <summary>
    /// 当前登录用户信息类
    /// </summary>
    [Export]
    public class CurrentUser
    {
        #region 字段

        /// <summary>
        /// 全局对应Key
        /// </summary>
        static String _currentuser = "Currentuser";

        /// <summary>
        /// 角色对应Key
        /// </summary>
        static String _role = "Role";

        #endregion

        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; private set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public String LoginName { get; private set; }

        /// <summary>
        /// 用户基本信息
        /// </summary>
        public User UserInfo { get; set; }

        /// <summary>
        /// 用户组信息
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// 当前登录IP
        /// </summary>
        public String CurIp { get; set; }

        #endregion

        #region 构造

        /// <summary>
        /// 构造当前用户附加信息(CurrentUser)
        /// </summary>
        public CurrentUser()
        {
            CurrentUser cu = SessionHelper.Get(_currentuser) as CurrentUser;

            if (cu.IsNullOrEmpty() || cu.UserInfo.IsNullOrEmpty())
            {
                //if (!HttpContext.Current.User.Identity.IsAuthenticated)
                //    return;

                //#region 注册MEF
                //AggregateCatalog aggregateCatalog = new AggregateCatalog();
                //var thisAssembly = new DirectoryCatalog(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll");
                //aggregateCatalog.Catalogs.Add(thisAssembly);
                //CompositionContainer container = new CompositionContainer(aggregateCatalog);
                //container.ComposeParts(this);
                //#endregion

                String userName = HttpContext.Current.User.Identity.Name;
                this.UserInfo = new User();
                this.UserInfo.Id = "00000000-0000-0000-0001-000000000001".GetGuid();
                SessionHelper.SetSession(_currentuser, this);
            }
            else
            {
                this.CurIp = Utils.GetIp();
                this.UserID = cu.UserID;
                this.LoginName = cu.LoginName;
                this.UserInfo = cu.UserInfo;
                this.Roles = cu.Roles;
            }
        }

        /// <summary>
        /// 构造当前用户附加信息(CurrentUser)
        /// </summary>
        /// <param name="user">用户对象</param>
        public CurrentUser(User user)
        {
            #region 注册MEF
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            var thisAssembly = new DirectoryCatalog(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll");
            aggregateCatalog.Catalogs.Add(thisAssembly);
            CompositionContainer container = new CompositionContainer(aggregateCatalog);
            container.ComposeParts(this);
            #endregion

            this.CurIp = Utils.GetIp();
            this.UserID = user.Id;
            this.UserInfo = user;
        }

        #endregion

        #region 静态实例

        public static CurrentUser GetInstance
        {
            get
            {
                return new CurrentUser();
            }
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 保存用户信息到Session中
        /// </summary>
        /// <param name="user">用户信息</param>
        public static void Save(User user)
        {
            // 保存用户信息
            if (!user.IsNullOrEmpty())
            {
                CurrentUser cu = new CurrentUser(user);
                SessionHelper.SetSession(_currentuser, cu);
            }
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <returns>返回操作结果</returns>
        public static Guid GetUserId()
        {
            return CurrentUser.GetInstance.UserID;
        }

        /// <summary>
        /// 获取当前用角色集合
        /// </summary>
        /// <returns>返回操作结果 List<SGSRole></returns>
        public static List<Role> GetRoles()
        {
            List<Role> roles = SessionHelper.GetSession(_role) as List<Role>;
            if (roles.IsNullOrEmpty())
            {
                roles = CurrentUser.GetInstance.Roles;
            }
            return roles;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>返回操作结果SGSUser</returns>
        public static User GetUser()
        {
            return CurrentUser.GetInstance.UserInfo;
        }
        
        ///<summary>
        /// 根据 Agent 判断是否是智能手机
        ///</summary>
        ///<returns></returns>
        public static bool CheckAgent()
        {
            bool flag = false;

            string agent = System.Web.HttpContext.Current.Request.UserAgent;
            string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };

            //排除 Windows 桌面系统
            if (!agent.Contains("Windows NT") || (agent.Contains("Windows NT") && agent.Contains("compatible; MSIE 9.0;")))
            {
                //排除 苹果桌面系统
                if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh"))
                {
                    foreach (string item in keywords)
                    {
                        if (agent.Contains(item))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            return flag;
        }
        #endregion
    }
}