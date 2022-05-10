using System.Collections.Generic;
using System.Threading.Tasks;
using MorningSchool.Models;

namespace MorningSchool.Business.Interfaces
{
    public interface IAdminService
    {
        Task<List<CabinetViewModel>> GetAllCabinets();
        Task<List<ClassViewModel>> GetAllClasses();
        Task<List<ClassroomTeacherViewModel>> GetAllClassroomTeachers();
        Task<List<EventViewModel>> GetAllAddEvents();
        Task<List<ThemeViewModel>> GetAllThemes();
        Task<CabinetViewModel> AddCabinet(CabinetViewModel model);
        Task<ClassViewModel> AddClass(ClassViewModel model);
        Task<ClassroomTeacherViewModel> AddClassroomTeacher(ClassroomTeacherViewModel model);
        Task<EventViewModel> AddEvent(EventViewModel model);
        Task<ThemeViewModel> AddTheme(ThemeViewModel model);
        Task<CabinetViewModel> EditCabinet(CabinetViewModel model);
        Task<ClassViewModel> EditClass(ClassViewModel model);
        Task<ClassroomTeacherViewModel> EditClassroomTeacher(ClassroomTeacherViewModel model);
        Task<EventViewModel> EditEvent(EventViewModel model);
        Task<ThemeViewModel> EditTheme(ThemeViewModel model);
        Task<CabinetViewModel> DeleteCabinet(CabinetViewModel model);
        Task<ClassViewModel> DeleteClass(ClassViewModel model);
        Task<ClassroomTeacherViewModel> DeleteClassroomTeacher(ClassroomTeacherViewModel model);
        Task<EventViewModel> DeleteEvent(EventViewModel model);
        Task<ThemeViewModel> DeleteTheme(ThemeViewModel model);
    }
}