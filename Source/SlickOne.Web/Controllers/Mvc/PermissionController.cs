using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public ActionResult List()
        {
            return View();
        }
    }
}