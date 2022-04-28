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
        private readonly IEventService eventService;
        
        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var lastEvents = await eventService.GetLastEvents(ConstantsMessages.EventsOnHomePage);
            
            return View(lastEvents);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}