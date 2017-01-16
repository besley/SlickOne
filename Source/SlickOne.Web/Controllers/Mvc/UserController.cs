using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Dialog()
        {
            return View();
        }
    }
}