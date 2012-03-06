using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGravatar.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var profile = new Grofile().GetInfo("ngravatar@kendoll.net");
            ViewData["Grofile"] = profile;
            return View();
        }

    }
}
