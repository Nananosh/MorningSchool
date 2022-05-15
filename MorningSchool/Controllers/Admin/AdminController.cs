using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
using MorningSchool.Models;
using MorningSchool.ViewModels;

namespace MorningSchool.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult EventsChart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminCabinet()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminClass()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminClassroomTeacher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminEvent()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminTheme()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminEventResult()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ThemesChart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ClassesChart()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEventByDateFilter(string dateStart, string dateEnd)
        {
            DateTime dStart = DateTime.Parse(dateStart);
            DateTime dEnd = DateTime.Parse(dateEnd);
            var events = await _adminService.GetEventsByDateFilter(dStart, dEnd);

            return Json(events);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventByDateAndThemeFilter(string dateStart, string dateEnd)
        {
            DateTime dStart = DateTime.Parse(dateStart);
            DateTime dEnd = DateTime.Parse(dateEnd);
            var events = await _adminService.GetEventByDateAndThemeFilter(dStart, dEnd);

            return Json(events);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventByDateAndClassFilter(string dateStart, string dateEnd)
        {
            DateTime dStart = DateTime.Parse(dateStart);
            DateTime dEnd = DateTime.Parse(dateEnd);
            var events = await _adminService.GetEventByDateAndClassFilter(dStart, dEnd);

            return Json(events);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllCabinets()
        {
            return Json(await _adminService.GetAllCabinets());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllClasses()
        {
            return Json(await _adminService.GetAllClasses());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllClassroomTeachers()
        {
            return Json(await _adminService.GetAllClassroomTeachers());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllEvents()
        {
            return Json(await _adminService.GetAllAddEvents());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllThemes()
        {
            return Json(await _adminService.GetAllThemes());
        }
        
        [HttpGet]
        public async Task<JsonResult> GetAllEventResults()
        {
            return Json(await _adminService.GetAllEventResults());
        }

        [HttpPost]
        public async Task<JsonResult> AddCabinet(CabinetViewModel model)
        {
            return Json(await _adminService.AddCabinet(model));
        }

        [HttpPost]
        public async Task<JsonResult> AddClass(ClassViewModel model)
        {
            return Json(await _adminService.AddClass(model));
        }

        [HttpPost]
        public async Task<JsonResult> AddClassroomTeacher(ClassroomTeacherViewModel model)
        {
            return Json(await _adminService.AddClassroomTeacher(model));
        }

        [HttpPost]
        public async Task<JsonResult> AddEvent(EventViewModel model)
        {
            return Json(await _adminService.AddEvent(model));
        }

        [HttpPost]
        public async Task<JsonResult> AddTheme(ThemeViewModel model)
        {
            return Json(await _adminService.AddTheme(model));
        }
        
        [HttpPost]
        public async Task<JsonResult> AddEventResult(EventResultViewModel model)
        {
            return Json(await _adminService.AddEventResult(model));
        }

        [HttpPost]
        public async Task<JsonResult> EditCabinet(CabinetViewModel model)
        {
            return Json(await _adminService.EditCabinet(model));
        }

        [HttpPost]
        public async Task<JsonResult> EditClass(ClassViewModel model)
        {
            return Json(await _adminService.EditClass(model));
        }

        [HttpPost]
        public async Task<JsonResult> EditClassroomTeacher(ClassroomTeacherViewModel model)
        {
            return Json(await _adminService.EditClassroomTeacher(model));
        }

        [HttpPost]
        public async Task<JsonResult> EditEvent(EventViewModel model)
        {
            return Json(await _adminService.EditEvent(model));
        }

        [HttpPost]
        public async Task<JsonResult> EditTheme(ThemeViewModel model)
        {
            return Json(await _adminService.EditTheme(model));
        }
        
        [HttpPost]
        public async Task<JsonResult> EditEventResult(EventResultViewModel model)
        {
            return Json(await _adminService.EditEventResult(model));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCabinet(CabinetViewModel model)
        {
            return Json(await _adminService.DeleteCabinet(model));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteClass(ClassViewModel model)
        {
            return Json(await _adminService.DeleteClass(model));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteClassroomTeacher(ClassroomTeacherViewModel model)
        {
            return Json(await _adminService.DeleteClassroomTeacher(model));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteEvent(EventViewModel model)
        {
            return Json(await _adminService.DeleteEvent(model));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteTheme(ThemeViewModel model)
        {
            return Json(await _adminService.DeleteTheme(model));
        }
        
        [HttpDelete]
        public async Task<JsonResult> DeleteEventResult(EventResultViewModel model)
        {
            return Json(await _adminService.DeleteEventResult(model));
        }
    }
}