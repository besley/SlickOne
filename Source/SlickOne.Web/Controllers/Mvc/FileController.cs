using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Download()
        {
            return View();
        }
    }
}