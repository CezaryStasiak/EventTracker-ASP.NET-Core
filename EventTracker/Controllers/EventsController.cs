using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventTracker.Models;
using EventTracker.Services;
using EventTracker.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private HttpContext httpcontext;
        private IEventManager _eventManager;
        List<EventModel> list;
        
        public EventsController(IHttpContextAccessor httpContextAccessor, EventManager eventManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventManager = eventManager;
            httpcontext = _httpContextAccessor.HttpContext;
        }

        private async Task GetEventsAsync(string userId)
        {
            list = (List<EventModel>) await _eventManager.GetEventsAsync(userId);
        }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await GetEventsAsync(httpcontext.User.Claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
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