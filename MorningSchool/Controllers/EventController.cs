using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
using MorningSchool.Models;
using X.PagedList;

namespace MorningSchool.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> Info(int id)
        {
            var eventById = await eventService.GetEventById(id);

            return View(eventById);
        }

        public IActionResult Rating(int filterId, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Filter = filterId;
            
            switch (filterId)
            {
                case 1:
                {
                    var rating = eventService.RatingByClass(startDate, endDate);

                    return View(rating);
                }
                case 2:
                {
                    var rating = eventService.RatingByThemes(startDate, endDate);

                    return View(rating);
                }
                default:
                {
                    var rating = eventService.RatingByClass(null, null);

                    return View(rating);
                }
            }
        }

        public IActionResult GetEvents(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                ViewBag.StartDate = startDate.Value;
            }

            if (endDate.HasValue)
            {
                ViewBag.EndDate = endDate.Value;
            }
            
            var events = eventService.GetEvents(startDate, endDate);
            
            return View(events);
        }
    }
}