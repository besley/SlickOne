using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SlickOne.Web.Controllers.WebApi
{
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public string Get(string id)
        {
            return string.Format("Hello World!--{0}", id);
        }
    }
}
