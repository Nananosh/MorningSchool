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
        private readonly ApplicationContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public EventService(
            ApplicationContext db,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.roleManager = roleManager;
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        
        public async Task<List<EventViewModel>> GetLastEvents(int count)
        {
            var lastEvents = await db.Events.OrderByDescending(e => e.EventDate).Take(count).ToListAsync();

            return mapper.Map<List<EventViewModel>>(lastEvents);
        }

        public async Task<EventViewModel> GetEventById(int id)
        {
            var eventElement = await db.Events
                .Include(c=>c.Theme)
                .Include(c=>c.Cabinet)
                .Include(c=>c.Class)
                .ThenInclude(t => t.ClassroomTeacher)
                .FirstOrDefaultAsync(e => e.Id == id);

            return mapper.Map<EventViewModel>(eventElement);
        }

        public List<Rating> RatingByClass(DateTime? startDate, DateTime? endDate)
        {
            var top = db.Events.Include(x => x.Class).Include(x => x.Theme).ToList();

            if (startDate.HasValue)
            {
                top = top.Where(x => x.EventDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                top = top.Where(x => x.EventDate <= endDate.Value).ToList();
            }
            
            var groupingTop = top
                .GroupBy(x => x.Class)
                .Select(g => new Rating
                {
                    Name = g.Key.ClassName,
                    CountEvents = g.Sum(x => x.Id)

                });

            var userTop = groupingTop.OrderByDescending(x => x.CountEvents).ToList();

            return userTop;
        }

        public async Task<List<EventViewModel>> GetEventsByName(string name)
        {
            var events = await db.Events
                .Include(x => x.Cabinet)
                .Include(x => x.Class)
                .Where(x => x.EventsName.Contains(name)).ToListAsync();

            return mapper.Map<List<EventViewModel>>(events);
        }
        
        public List<Rating> RatingByThemes(DateTime? startDate, DateTime? endDate)
        {
            var top = db.Events.Include(x => x.Theme).Include(x => x.Class).ToList();

            if (startDate.HasValue)
            {
                top = top.Where(x => x.EventDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                top = top.Where(x => x.EventDate <= endDate.Value).ToList();
            }
            
            var groupingTop = top
                .GroupBy(x => x.Theme)
                .Select(g => new Rating
                {
                    Name = g.Key.ThemeName,
                    CountEvents = g.Sum(x => x.Id)

                });

            var userTop = groupingTop.OrderByDescending(x => x.CountEvents).ToList();

            return userTop;
        }

        public List<EventViewModel> GetEvents(DateTime? startDate, DateTime? endDate)
        {
            var events = db.Events.ToList();

            if (startDate.HasValue)
            {
                events = events.Where(x => x.EventDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                events = events.Where(x => x.EventDate <= endDate.Value).ToList();
            }
            
            return mapper.Map<List<EventViewModel>>(events);
        }
    }
}