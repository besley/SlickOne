using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideBar()
        {
            return View();
        }

        public ActionResult NavBar()
        {
            return View();
        }
    }
}