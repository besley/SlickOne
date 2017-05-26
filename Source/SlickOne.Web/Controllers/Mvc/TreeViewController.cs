using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class TreeViewController : Controller
    {
        // GET: TreeView
        public ActionResult Simple()
        {
            return View();
        }

        public ActionResult Complex()
        {
            return View();
        }
    }
}