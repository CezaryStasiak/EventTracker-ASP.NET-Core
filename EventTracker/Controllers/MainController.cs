using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EventTracker.Services;
using System;

namespace EventTracker.Controllers
{
    public class MainController : Controller
    {
        UserManager _userManager;

        public MainController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                //authenticate
                var user = new UserModel()
                {
                    UserEmail = model.UserEmail,
                    UserPassword = model.UserPassword
                };
                _userManager.SignIn(HttpContext, user);
                return RedirectToAction("Index", "Events", null);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("summary", ex.Message);
                return View(model);
            }
        }

    }
}