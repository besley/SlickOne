using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    public class WorkflowController : Controller
    {
        // GET: Workflow
        public ActionResult Process()
        {
            return View();
        }

        public ActionResult ProcessInstance()
        {
            return View();
        }

        public ActionResult ActivityInstance()
        {
            return View();
        }

        public ActionResult EntityDef()
        {
            return View();
        }
    }
}