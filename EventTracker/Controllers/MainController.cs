using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EventTracker.Services;
using System;
using System.Threading.Tasks;

namespace EventTracker.Controllers
{
    public class MainController : Controller
    {
        IUserManager _userManager;

        public MainController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserViewModel() { Validation = new UserValidation().UserCreationSuccess() });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model) 
        {
            if (!ModelState.IsValid )
                return View();
            try
            {
                //authenticate
                var user = new UserModel()
                {
                    UserEmail = model.UserLogin.UserEmail,
                    UserPassword = model.UserLogin.UserPassword
                };
                await _userManager.SignIn(HttpContext, user);
                return RedirectToAction("Index", "Events", null);
            }
            catch
            {
                return View("Index", new UserViewModel() { Validation = new UserValidation().WrongPassword() });
            }
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Main");
            try
            {
                var user = new UserModel()
                {
                    UserEmail = model.UserLogin.UserEmail,
                    UserPassword = model.UserLogin.UserPassword
                };
                UserValidation userRegistration = (UserValidation) _userManager.Register(user);
                return View("Index", new UserViewModel { Validation = userRegistration });
            }
            catch
            {
                return View("Index", new UserViewModel { Validation = new UserValidation().RandomError() });
            }
        }

    }
}