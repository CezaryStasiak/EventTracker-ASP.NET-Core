﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventTracker.Models;
using EventTracker.Services;
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
            return View(list);
        }

        [HttpPost]
        public IActionResult Index(string Title, string Description, string Date)
        {

            return View();
        }

        [HttpGet("Events/Index/{year}-{month}-{day}")]
        public async Task<IActionResult> Index(int year, int month, int day)
        {
            await GetEventsAsync(httpcontext.User.Claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                return View(list.Where(e => e.Date.Year == year && e.Date.Month == month && e.Date.Day == day).ToList());
            }
            catch
            {
                return View(list);
            }
        }

    }
}