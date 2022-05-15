using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Business.Interfaces;
using MorningSchool.Migrations;
using MorningSchool.Models;
using MorningSchool.ViewModels;

namespace MorningSchool.Business.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public EventService(
            ApplicationContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<EventViewModel>> GetLastEvents(int count)
        {
            var lastEvents = await _db.Events.OrderByDescending(e => e.EventDate).Take(count).ToListAsync();

            return _mapper.Map<List<EventViewModel>>(lastEvents);
        }

        public async Task<EventViewModel> GetEventById(int id)
        {
            var eventElement = await _db.Events
                .Include(c => c.Theme)
                .Include(c => c.Cabinet)
                .Include(c => c.Class)
                .ThenInclude(t => t.ClassroomTeacher)
                .FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<EventViewModel>(eventElement);
        }

        public List<Rating> RatingByClass(DateTime? startDate, DateTime? endDate)
        {
            var top = _db.Events.Include(x => x.Class).Include(x => x.Theme).Where(x => x.Class != null).ToList();

            if (startDate.HasValue)
            {
                top = top.Where(x => x.EventDate.Date >= startDate.Value.Date).ToList();
            }

            if (endDate.HasValue)
            {
                top = top.Where(x => x.EventDate.Date <= endDate.Value.Date).ToList();
            }

            var groupingTop = top
                .GroupBy(x => x.Class)
                .Select(g => new Rating
                {
                    Name = g.Key.ClassName,
                    CountEvents = top.Where(x => x.ClassId == g.Key.Id).ToList().Count() * 10
                });

            var userTop = groupingTop.OrderByDescending(x => x.CountEvents).ToList();

            return userTop;
        }

        public async Task<List<EventViewModel>> GetEventsByName(string name)
        {
            var events = await _db.Events
                .Include(x => x.Cabinet)
                .Include(x => x.Class)
                .Where(x => x.EventsName.Contains(name)).ToListAsync();

            return _mapper.Map<List<EventViewModel>>(events);
        }

        public async Task<List<EventResultViewModel>> GetAllEventResultsByEventId(int id)
        {
            var eventResults = await _db.EventResults.Include(x => x.Event).Where(x => x.EventId == id).ToListAsync();
            return _mapper.Map<List<EventResultViewModel>>(eventResults);
        }

        public List<Rating> RatingByThemes(DateTime? startDate, DateTime? endDate)
        {
            var top = _db.Events.Include(x => x.Theme).Include(x => x.Class).ToList();

            if (startDate.HasValue)
            {
                top = top.Where(x => x.EventDate.Date >= startDate.Value.Date).ToList();
            }

            if (endDate.HasValue)
            {
                top = top.Where(x => x.EventDate.Date <= endDate.Value.Date).ToList();
            }

            var groupingTop = top
                .GroupBy(x => x.Theme)
                .Select(g => new Rating
                {
                    Name = g.Key.ThemeName,
                    CountEvents = top.Where(x => x.ThemeId == g.Key.Id).ToList().Count() * 10
                });

            var userTop = groupingTop.OrderByDescending(x => x.CountEvents).ToList();

            return userTop;
        }

        public List<EventViewModel> GetEvents(DateTime? startDate, DateTime? endDate)
        {
            var events = _db.Events.ToList();

            if (startDate.HasValue)
            {
                events = events.Where(x => x.EventDate.Date >= startDate.Value.Date).ToList();
            }

            if (endDate.HasValue)
            {
                events = events.Where(x => x.EventDate.Date <= endDate.Value.Date).ToList();
            }

            return _mapper.Map<List<EventViewModel>>(events).OrderByDescending(x => x.EventDate).ToList();
        }
    }
}