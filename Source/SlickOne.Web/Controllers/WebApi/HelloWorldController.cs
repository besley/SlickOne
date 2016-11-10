using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SlickOne.Web.Controllers.WebApi
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public string Get(string id)
        {
            return string.Format("Hello World!--{0}", id);
        }
    }
}
