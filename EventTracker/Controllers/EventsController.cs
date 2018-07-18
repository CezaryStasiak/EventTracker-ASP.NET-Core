using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;
using EventTracker.Settings;
using EventTracker.UserData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EventTracker.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppSettings settings;
        IDbConnection _connection;

        public EventsController(IDbConnection connection, IOptions<AppSettings> congif)
        {
            settings = congif.Value;
            _connection = connection;
        }
        
        public IActionResult Index()
        {
            return View(new JsonResult(_connection.GetAllEvents(15, settings.testDb)));
        }

        [HttpGet("event/{date}")]
        public IActionResult GetEventsByDate(int date)
        {
            return new JsonResult(_connection.GetAllEvents(15, settings.testDb));
        }

    }
}