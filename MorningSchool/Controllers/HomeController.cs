using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MorningSchool.Business.Interfaces;
using MorningSchool.Constants;
using MorningSchool.Models;

namespace MorningSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string eventName)
        {
            var getEventsByName = await _eventService.GetEventsByName(eventName);

            return View(getEventsByName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}