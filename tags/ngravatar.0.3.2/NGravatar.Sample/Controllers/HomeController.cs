using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGravatar.Sample.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            // Gravatar profile info can be loaded with an email address
            var grofileInfo = new Grofile().GetInfo("ngravatar@kendoll.net");

            return View(grofileInfo);
        }
    }
}
