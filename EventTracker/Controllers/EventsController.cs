using System;
using System.Collections.Generic;
using System.Linq;
using EventTracker.Models;
using EventTracker.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    public class EventsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        ISession _session => _httpContextAccessor.HttpContext.Session;
        IDbConnection _connection;
        List<EventModel> list;
        
        public EventsController(IDbConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _connection = connection;
            list = _connection.Read<EventModel>($"SELECT * FROM Events WHERE UserId={_session.GetInt32("UserId")}").ToList();
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            //insert testing------------------------------------
            _connection.Insert(
                new EventModel()
                {
                    Title = "Something",
                    Description = "Doing Something",
                    Date = new DateTime(2018, 05, 31),
                    UserId = 25,
                    StartTime = new TimeSpan(15, 20, 00),
                    Length = new TimeSpan(4, 30, 0)
                }, "Events");
            // --------------------------------------------------
            if (list.Count < 1)
            {
                return View(new List<EventModel> { new EventModel { Title = "Error" } });
            }
            else
            {
            return View(list);
            }
        }

        [HttpPost]
        public IActionResult Index(string Title, string Description, string Date)
        {
            string[] date = Date.Split("-");
            list.Add(new EventModel
            {
                Title = Title,
                Description = Description,
                Date = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2])) 
            });
            return View(list);
        }

        [HttpGet("Events/Index/{year}-{month}-{day}")]
        public IActionResult Index(int year, int month, int day)
        {
            return View(list.Where(e => e.Date.Year == year && e.Date.Month == month && e.Date.Day == day).ToList());
        }

    }
}