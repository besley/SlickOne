using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class RoleController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        // GET: Role
        public ActionResult Edit()
        {
            return View();
        }
    }
}