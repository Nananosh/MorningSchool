using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
using MorningSchool.Models;
using MorningSchool.ViewModels;
using X.PagedList;

namespace MorningSchool.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IAdminService _adminService;
        private readonly INotificationService _notificationService;

        public EventController(IEventService eventService, IAdminService adminService,
            INotificationService notificationService)
        {
            _eventService = eventService;
            _adminService = adminService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Info(int id)
        {
            var eventById = await _eventService.GetEventById(id);
            ViewBag.EventResult = await _eventService.GetAllEventResultsByEventId(id);

            return View(eventById);
        }

        public IActionResult Rating(int filterId, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Filter = filterId;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            switch (filterId)
            {
                case 1:
                {
                    var rating = _eventService.RatingByClass(startDate, endDate);

                    return View(rating);
                }
                case 2:
                {
                    var rating = _eventService.RatingByThemes(startDate, endDate);

                    return View(rating);
                }
                default:
                {
                    var rating = _eventService.RatingByClass(null, null);

                    return View(rating);
                }
            }
        }

        public async Task<IActionResult> AllTeacher()
        {
            var teacher = await _adminService.GetAllClassroomTeachers();

            return View(teacher);
        }

        public async Task<IActionResult> AllClass()
        {
            var classes = await _adminService.GetAllClasses();

            return View(classes);
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

            var events = _eventService.GetEvents(startDate, endDate);

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(events);
        }

        public async Task<string> SubscribeEvent(string email)
        {
            var model = new EventSubscriptionViewModel {Email = email};

            return await _notificationService.SubscribeEvent(model);
        }
    }
}