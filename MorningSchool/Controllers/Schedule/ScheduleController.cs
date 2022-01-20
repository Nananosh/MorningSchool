using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
using MorningSchool.Models;
using MorningSchool.ViewModels.Admin;
using MorningSchool.ViewModels.Schedule;

namespace MorningSchool.Controllers.Schedule
{
    public class ScheduleController : Controller
    {
        private IMapper mapper;
        private IScheduleService scheduleService;
        private IAdminService adminService;

        public ScheduleController(IMapper mapper, IScheduleService scheduleService, IAdminService adminService)
        {
            this.mapper = mapper;
            this.scheduleService = scheduleService;
            this.adminService = adminService;
        }
        // GET
        public IActionResult AdminSchedule(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        
        public IActionResult ClassSchedule(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<IActionResult> AllSchedule()
        {
            var classes = await adminService.GetAllClasses();
            return View(mapper.Map<List<ClassViewModel>>(classes));
        }
        
        [HttpGet]
        public async Task<JsonResult> GetAllSchedulesByClassId(int id)
        {
            var schedules = await scheduleService.GetAllSchedulesByClassId(id);
            return Json(mapper.Map<List<ScheduleViewModel>>(schedules));
        }

        [HttpPost]
        public async Task<JsonResult> EditSchedule(ScheduleViewModel model)
        {
            var editedSchedule = await scheduleService.EditSchedule(mapper.Map<Models.Schedule>(model));
            return Json(mapper.Map<ScheduleViewModel>(editedSchedule));
        }
        
        [HttpPost]
        public async Task<JsonResult> AddSchedule(ScheduleViewModel model)
        {
            var addedSchedule = await scheduleService.AddSchedule(mapper.Map<Models.Schedule>(model));
            return Json(mapper.Map<ScheduleViewModel>(addedSchedule));
        }
        
        [HttpDelete]
        public async Task<JsonResult> DeleteSchedule(ScheduleViewModel model)
        {
            await scheduleService.DeleteSchedule(mapper.Map<Models.Schedule>(model));
            return Json(new Models.Schedule());
        }
    }
}