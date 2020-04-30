using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SteeltoeossServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IConfiguration config;
        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var val1 = config["Value1"];
            var val2 = config["Value2"];
            return new string[] { val1, val2 };
        }
    }
}