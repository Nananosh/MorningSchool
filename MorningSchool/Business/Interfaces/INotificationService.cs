using System.Threading.Tasks;
using MorningSchool.Models;
using MorningSchool.ViewModels;

namespace MorningSchool.Business.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAfterCreateEvent(EventViewModel model);
        Task<string> SubscribeEvent(EventSubscriptionViewModel email);
    }
}