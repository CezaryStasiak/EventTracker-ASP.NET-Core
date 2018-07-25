using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EventTracker.Controllers
{
    public class MainController : Controller
    {
        public MainController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.SetInt32("UserId", 25);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel model) 
        {
            /* TBD ....
            if session is Available, check if user exist. If true get user id from database, use it as session key to get events 
            that belong to this user.
             */
            
            return View();
        }

    }
}