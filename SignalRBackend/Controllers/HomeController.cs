using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRBackend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View("Map");
        }

        public ActionResult Chat()
        {
            return View("Chat");
        }

        public ActionResult GameScore()
        {
            return View("GameScore");
        }

        public ActionResult Object()
        {
            return View("Object");
        }     
    }
}
