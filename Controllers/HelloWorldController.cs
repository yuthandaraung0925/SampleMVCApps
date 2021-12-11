
using Microsoft.AspNetCore.Mvc;

namespace SampleMVCApps.Controllers
{
    public class HelloWorldCOntroller: Controller{
        public IActionResult Index(){
            ViewData["Message"] = "Hello, This is my view";
            return View();
        }

        public IActionResult Welcome(){
            ViewData["Message"] = "Hello, Welcome to Hello World Application";
            return View();
        }
    }
}