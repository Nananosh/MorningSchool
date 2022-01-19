using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MorningSchool.Business.Interfaces;
using MorningSchool.Models;
using MorningSchool.ViewModels.Admin;

namespace MorningSchool.Controllers.Admin
{
    public class AdminController : Controller
    {
        private IMapper mapper;
        private IAdminService adminService;

        public AdminController(IMapper mapper, IAdminService adminService)
        {
            this.mapper = mapper;
            this.adminService = adminService;
        }

        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult AdminClass()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllClasses()
        {
            var classes = await adminService.GetAllClasses();
            return Json(mapper.Map<List<ClassViewModel>>(classes));
        }

        [HttpPost]
        public async Task<JsonResult> EditClass(ClassViewModel model)
        {
            var editedClass = await adminService.EditClass(mapper.Map<Class>(model));
            return Json(mapper.Map<ClassViewModel>(editedClass));
        }
        
        [HttpPost]
        public async Task<JsonResult> AddClass(ClassViewModel model)
        {
            var addedClass = await adminService.AddClass(mapper.Map<Class>(model));
            return Json(mapper.Map<ClassViewModel>(addedClass));
        }
        
        [HttpDelete]
        public async Task DeleteClass(ClassViewModel model)
        {
            await adminService.DeleteClass(mapper.Map<Class>(model));
        }
    }
}