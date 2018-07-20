using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EventTracker.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel model)
        {

            return View();
        }

    }
}