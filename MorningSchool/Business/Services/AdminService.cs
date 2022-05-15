using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Business.Interfaces;
using MorningSchool.Migrations;
using MorningSchool.Models;
using MorningSchool.ViewModels;
using MorningSchool.ViewModels.Admin;
using X.PagedList;

namespace MorningSchool.Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AdminService(ApplicationContext db, IMapper mapper, INotificationService notificationService)
        {
            _db = db;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<List<EventChartViewModel>> GetEventsByDateFilter(DateTime dateStart, DateTime dateEnd)
        {
            var events = await _db.Events.Where(x => x.EventDate >= dateStart && x.EventDate <= dateEnd)
                .ToList().OrderBy(x => x.EventDate).GroupBy(x => x.EventDate.Date)
                .Select(x => new EventChartViewModel
                {
                    Count = x.Select(y => _db.Events.Count(z => z.EventDate.Date == y.EventDate.Date)).Count(),
                    Date = x.Key.ToShortDateString()
                }).ToListAsync();

            return events;
        }

        public async Task<List<EventChartByThemeViewModel>> GetEventByDateAndThemeFilter(DateTime dateStart,
            DateTime dateEnd)
        {
            var events = await _db.Events.Include(x => x.Theme)
                .Where(x => x.EventDate >= dateStart && x.EventDate <= dateEnd)
                .ToList().OrderBy(x => x.EventDate).GroupBy(x => x.Theme)
                .Select(x => new EventChartByThemeViewModel
                {
                    Count = x.Select(y => _db.Events.Count(z => z.Theme == y.Theme)).Count(),
                    Theme = x.Key.ThemeName
                }).ToListAsync();

            return events;
        }

        public async Task<List<EventChartByClassViewModel>> GetEventByDateAndClassFilter(DateTime dateStart,
            DateTime dateEnd)
        {
            var events = await _db.Events.Include(x => x.Class)
                .Where(x => x.EventDate >= dateStart && x.EventDate <= dateEnd && x.Class != null)
                .ToList().OrderBy(x => x.EventDate).GroupBy(x => x.Class)
                .Select(x => new EventChartByClassViewModel
                {
                    Count = x.Select(y => _db.Events.Count(z => z.Class == y.Class)).Count(),
                    ClassName = x.Key.ClassName
                }).ToListAsync();

            return events;
        }

        public async Task<List<CabinetViewModel>> GetAllCabinets()
        {
            var cabinets = await _db.Cabinets.ToListAsync();
            return _mapper.Map<List<CabinetViewModel>>(cabinets);
        }

        public async Task<List<ClassViewModel>> GetAllClasses()
        {
            var classes = await _db.Classes.Include(x => x.ClassroomTeacher).ToListAsync();
            return _mapper.Map<List<ClassViewModel>>(classes);
        }

        public async Task<List<ClassroomTeacherViewModel>> GetAllClassroomTeachers()
        {
            var classroomTeachers = await _db.ClassroomTeachers.ToListAsync();
            return _mapper.Map<List<ClassroomTeacherViewModel>>(classroomTeachers);
        }

        public async Task<List<EventViewModel>> GetAllAddEvents()
        {
            var events = await _db.Events.Include(x => x.Class).Include(x => x.Cabinet).Include(x => x.Theme)
                .ToListAsync();
            return _mapper.Map<List<EventViewModel>>(events);
        }

        public async Task<List<ThemeViewModel>> GetAllThemes()
        {
            var themes = await _db.Themes.ToListAsync();
            return _mapper.Map<List<ThemeViewModel>>(themes);
        }

        public async Task<List<EventResultViewModel>> GetAllEventResults()
        {
            var eventResults = await _db.EventResults.Include(x => x.Event).ToListAsync();
            return _mapper.Map<List<EventResultViewModel>>(eventResults);
        }

        public async Task<CabinetViewModel> AddCabinet(CabinetViewModel model)
        {
            var cabinet = _mapper.Map<Cabinet>(model);
            await _db.Cabinets.AddAsync(cabinet);
            await _db.SaveChangesAsync();
            return _mapper.Map<CabinetViewModel>(cabinet);
        }

        public async Task<ClassViewModel> AddClass(ClassViewModel model)
        {
            var cClass = _mapper.Map<Class>(model);
            await _db.Classes.AddAsync(cClass);
            await _db.SaveChangesAsync();
            var addedClass = await _db.Classes.Include(x => x.ClassroomTeacher)
                .FirstOrDefaultAsync(x => x.Id == cClass.Id);
            return _mapper.Map<ClassViewModel>(addedClass);
        }

        public async Task<ClassroomTeacherViewModel> AddClassroomTeacher(ClassroomTeacherViewModel model)
        {
            var classroomTeacher = _mapper.Map<ClassroomTeacher>(model);
            await _db.ClassroomTeachers.AddAsync(classroomTeacher);
            await _db.SaveChangesAsync();
            return _mapper.Map<ClassroomTeacherViewModel>(classroomTeacher);
        }

        public async Task<EventViewModel> AddEvent(EventViewModel model)
        {
            var eEvent = _mapper.Map<Event>(model);
            await _db.Events.AddAsync(eEvent);
            await _db.SaveChangesAsync();
            var addedEvent = await _db.Events.Include(x => x.Class).Include(x => x.Cabinet).Include(x => x.Theme)
                .FirstOrDefaultAsync(x => x.Id == eEvent.Id);

            _notificationService.SendNotificationAfterCreateEvent(_mapper.Map<EventViewModel>(addedEvent));
            return _mapper.Map<EventViewModel>(addedEvent);
        }

        public async Task<ThemeViewModel> AddTheme(ThemeViewModel model)
        {
            var theme = _mapper.Map<Theme>(model);
            await _db.Themes.AddAsync(theme);
            await _db.SaveChangesAsync();
            return _mapper.Map<ThemeViewModel>(theme);
        }

        public async Task<EventResultViewModel> AddEventResult(EventResultViewModel model)
        {
            var eventResult = _mapper.Map<EventResult>(model);
            await _db.EventResults.AddAsync(eventResult);
            await _db.SaveChangesAsync();
            var addedEventResult = await _db.EventResults.Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == eventResult.Id);
            return _mapper.Map<EventResultViewModel>(addedEventResult);
        }

        public async Task<CabinetViewModel> EditCabinet(CabinetViewModel model)
        {
            var editCabinet = await _db.Cabinets.FirstOrDefaultAsync(x => x.Id == model.Id);
            editCabinet.CabinetNumber = model.CabinetNumber;
            await _db.SaveChangesAsync();
            return _mapper.Map<CabinetViewModel>(editCabinet);
        }

        public async Task<ClassViewModel> EditClass(ClassViewModel model)
        {
            var editClass = await _db.Classes.FirstOrDefaultAsync(x => x.Id == model.Id);
            editClass.ClassName = model.ClassName;
            editClass.ClassroomTeacherId = model.ClassroomTeacherId;
            await _db.SaveChangesAsync();
            var editedClasses = await _db.Classes.FirstOrDefaultAsync(x => x.Id == editClass.Id);
            return _mapper.Map<ClassViewModel>(editedClasses);
        }

        public async Task<ClassroomTeacherViewModel> EditClassroomTeacher(ClassroomTeacherViewModel model)
        {
            var editClassroomTeacher = await _db.ClassroomTeachers.FirstOrDefaultAsync(x => x.Id == model.Id);
            editClassroomTeacher.Name = model.Name;
            editClassroomTeacher.Lastname = model.Lastname;
            editClassroomTeacher.Surname = model.Surname;
            editClassroomTeacher.TelephoneNumber = model.TelephoneNumber;
            await _db.SaveChangesAsync();
            return _mapper.Map<ClassroomTeacherViewModel>(editClassroomTeacher);
        }

        public async Task<EventViewModel> EditEvent(EventViewModel model)
        {
            var editEvent = await _db.Events.Include(x => x.Class).Include(x => x.Cabinet).Include(x => x.Theme)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            editEvent.CabinetId = model.CabinetId;
            editEvent.ClassId = model.ClassId;
            editEvent.Description = model.Description;
            editEvent.ThemeId = model.ThemeId;
            editEvent.EventDate = model.EventDate;
            editEvent.EventManager = model.EventManager;
            editEvent.EventsName = model.EventsName;
            editEvent.ImgUrl = model.ImgUrl;
            await _db.SaveChangesAsync();
            var editedEvent = await _db.Events.Include(x => x.Class).Include(x => x.Cabinet).Include(x => x.Theme)
                .FirstOrDefaultAsync(x => x.Id == editEvent.Id);
            return _mapper.Map<EventViewModel>(editedEvent);
        }

        public async Task<ThemeViewModel> EditTheme(ThemeViewModel model)
        {
            var editTheme = await _db.Themes.FirstOrDefaultAsync(x => x.Id == model.Id);
            editTheme.ThemeName = model.ThemeName;
            await _db.SaveChangesAsync();
            return _mapper.Map<ThemeViewModel>(editTheme);
        }

        public async Task<EventResultViewModel> EditEventResult(EventResultViewModel model)
        {
            var editEventResult = await _db.EventResults.FirstOrDefaultAsync(x => x.Id == model.Id);
            editEventResult.EventId = model.EventId;
            editEventResult.ImageUrl = model.ImageUrl;
            await _db.SaveChangesAsync();
            var editedEventResult = await _db.EventResults.Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == editEventResult.Id);
            return _mapper.Map<EventResultViewModel>(editedEventResult);
        }

        public async Task<CabinetViewModel> DeleteCabinet(CabinetViewModel model)
        {
            var cabinet = await _db.Cabinets.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.Cabinets.Remove(cabinet);
            await _db.SaveChangesAsync();
            return new CabinetViewModel();
        }

        public async Task<ClassViewModel> DeleteClass(ClassViewModel model)
        {
            var cClass = await _db.Classes.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.Classes.Remove(cClass);
            await _db.SaveChangesAsync();
            return new ClassViewModel();
        }

        public async Task<ClassroomTeacherViewModel> DeleteClassroomTeacher(ClassroomTeacherViewModel model)
        {
            var classroomTeacher = await _db.ClassroomTeachers.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.ClassroomTeachers.Remove(classroomTeacher);
            await _db.SaveChangesAsync();
            return new ClassroomTeacherViewModel();
        }

        public async Task<EventViewModel> DeleteEvent(EventViewModel model)
        {
            var eEvent = await _db.Events.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.Events.Remove(eEvent);
            await _db.SaveChangesAsync();
            return new EventViewModel();
        }

        public async Task<ThemeViewModel> DeleteTheme(ThemeViewModel model)
        {
            var theme = await _db.Themes.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.Themes.Remove(theme);
            await _db.SaveChangesAsync();
            return new ThemeViewModel();
        }

        public async Task<EventResultViewModel> DeleteEventResult(EventResultViewModel model)
        {
            var eventResult = await _db.EventResults.FirstOrDefaultAsync(x => x.Id == model.Id);
            _db.EventResults.Remove(eventResult);
            await _db.SaveChangesAsync();
            return new EventResultViewModel();
        }
    }
}