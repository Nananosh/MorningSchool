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
        IEnumerable<EventViewModel> GetEvents();
    }
}