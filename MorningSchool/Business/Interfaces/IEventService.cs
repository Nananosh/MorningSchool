using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MorningSchool.Models;
using MorningSchool.ViewModels;

namespace MorningSchool.Business.Interfaces
{
    public interface IEventService
    {
        Task<List<EventViewModel>> GetLastEvents(int count);
        Task<EventViewModel> GetEventById(int id);
        List<EventViewModel> GetEvents(DateTime? startDate, DateTime? endDate);
        List<Rating> RatingByClass(DateTime? startDate, DateTime? endDate);
        List<Rating> RatingByThemes(DateTime? startDate, DateTime? endDate);
        Task<List<EventViewModel>> GetEventsByName(string name);
    }
}