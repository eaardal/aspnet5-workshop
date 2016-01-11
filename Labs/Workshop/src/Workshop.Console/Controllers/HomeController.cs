using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Workshop.Console.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("mvc")]
        public string Hello() => "Hello from MVC2";

        [HttpGet("mvcview")]
        public IActionResult Index() => View();
    }
}
