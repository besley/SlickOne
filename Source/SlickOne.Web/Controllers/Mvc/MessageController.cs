using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SlickOne.Web.Controllers.Mvc
{
    /// <summary>
    /// 消息组件控制器
    /// </summary>
    public class MessageController : Controller
    {
        // GET: Messenger
        public ActionResult Popup()
        {
            return View();
        }

        public ActionResult Messenger()
        {
            return View();
        }

        public ActionResult Dialog()
        {
            return View();
        }
    }
}