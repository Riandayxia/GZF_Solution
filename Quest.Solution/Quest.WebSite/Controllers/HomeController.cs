using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quest.WebSite.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
