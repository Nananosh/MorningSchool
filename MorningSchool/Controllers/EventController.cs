using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
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

        public IActionResult GetEvents(int? page)
        {
            var pageSize = 3;
            var pageNumber = (page ?? 1);

            return View(eventService.GetEvents().ToPagedList(pageNumber, pageSize));

        }
    }
}