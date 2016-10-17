using Quest.Core.Base;
using Quest.Framework;
using Quest.Mobile.CommonSupport.Filter;
using Quest.Mobile.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Quest.Core.Models.Base;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace Quest.Mobile.Controllers
{
    public class HomeController : BaseController
    {
        #region 属性
        /// <summary>
        /// 获取或设置 用户数据访问对象
        /// </summary>
        [Import]
        private IUserService UserService { get; set; }

       

        #endregion
        public HomeController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
