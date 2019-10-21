using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class ResourceController : Controller
    {
        // GET: Resource
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Authorize()
        {
            return View();
        }
    }
}