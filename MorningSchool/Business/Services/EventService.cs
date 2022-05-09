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

        public IEnumerable<EventViewModel> GetEvents()
        {
            var events = db.Events;
            
            return mapper.Map<IEnumerable<EventViewModel>>(events);
        }
    }
}