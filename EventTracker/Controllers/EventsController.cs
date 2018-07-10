using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.UserData;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    public class EventsController : Controller
    {
        DataLoader data = new DataLoader();
        public EventsController()
        {
            data.Load();
        }
        
        public IActionResult Index()
        {
            DataLoader data = new DataLoader();
            return View(data.events);
        }

        [HttpGet("event/{date}")]
        public IActionResult SingleEvent(int date)
        {
            return new JsonResult(data.events[0]);
        }

    }
}