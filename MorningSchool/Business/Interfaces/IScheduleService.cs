using System.Collections.Generic;
using System.Threading.Tasks;
using MorningSchool.Models;

namespace MorningSchool.Business.Interfaces
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedulesByClassId(int id);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Schedule> EditSchedule(Schedule schedule);
        Task DeleteSchedule(Schedule schedule);
    }
}