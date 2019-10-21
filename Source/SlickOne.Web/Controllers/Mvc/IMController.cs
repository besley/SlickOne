using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class IMController : Controller
    {
        // GET: IM
        public ActionResult Notification()
        {
            return View();
        }

        public ActionResult Messenger()
        {
            return View();
        }
    }
}