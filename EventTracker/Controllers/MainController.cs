using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel user)
        {
            // user loging and session init goes here

            return RedirectToAction("Index", "Events");
        }
    }
}