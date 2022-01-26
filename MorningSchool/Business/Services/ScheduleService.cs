using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Business.Interfaces;
using MorningSchool.Migrations;
using MorningSchool.Models;

namespace MorningSchool.Business.Services
{
    public class ScheduleService : IScheduleService
    {
        private ApplicationContext db;

        public ScheduleService(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<List<Schedule>> GetAllSchedulesByClassId(int id)
        {
            var schedules = await db.Schedules.Where(x => x.ClassId == id).ToListAsync();
            return schedules;
        }

        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            await db.AddAsync(schedule);
            await db.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> EditSchedule(Schedule schedule)
        {
            var editSchedule = await db.Schedules.FirstOrDefaultAsync(x => x.Id == schedule.Id);
            editSchedule.Title = schedule.Title;
            editSchedule.ClassId = schedule.ClassId;
            editSchedule.StartDate = schedule.StartDate;
            editSchedule.EndDate = schedule.EndDate;
            editSchedule.RecurrenceRule = schedule.RecurrenceRule;
            await db.SaveChangesAsync();
            var editedSchedule = await db.Schedules.FirstOrDefaultAsync(x => x.Id == editSchedule.Id);
            return editedSchedule;
        }

        public async Task DeleteSchedule(Schedule schedule)
        {
            var deleteSchedule = await db.Schedules.FirstOrDefaultAsync(x => x.Id == schedule.Id);
            db.Schedules.Remove(deleteSchedule);
            await db.SaveChangesAsync();
        }
    }
}