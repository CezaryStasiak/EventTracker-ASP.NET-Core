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
        List<EventModel> list;

        public EventsController(IDbConnection connection, IOptions<AppSettings> congif)
        {
            settings = congif.Value;
            _connection = connection;
            list = _connection.GetAllEvents(15, settings.testDb).Result.ToList(); 
        }
        
        [HttpGet]
        public IActionResult Index()
        {
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
                Date = new DateData.Date { Year = int.Parse(date[0]), Day = int.Parse(date[1]), Month = int.Parse(date[2]) }
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