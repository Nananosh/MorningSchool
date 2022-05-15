using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Business.Interfaces;
using MorningSchool.Migrations;
using MorningSchool.Models;
using MorningSchool.ViewModels;

namespace MorningSchool.Business.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;

        public NotificationService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> SubscribeEvent(EventSubscriptionViewModel model)
        {
            var eventSubscribe = _mapper.Map<EventSubscription>(model);
            if (await _db.EventSubscriptions.AnyAsync(x => x.Email == eventSubscribe.Email))
            {
                return "Вы уже оформили подписку!";
            }

            await _db.EventSubscriptions.AddAsync(new EventSubscription {Email = eventSubscribe.Email});
            await _db.SaveChangesAsync();
            
            return "Подписка оформлена!";
        }

        public async Task SendNotificationAfterCreateEvent(EventViewModel model)
        {
            EmailConfirm emailService = new EmailConfirm();
            var subscriptionsEvents = _db.EventSubscriptions.ToList();
            foreach (var subscription in subscriptionsEvents)
            {
                await emailService.SendEmailDefault(subscription.Email, $"Приглашаем вас на мероприятие \"{model.EventsName}\".",
                    $"Доброго времени суток. Хотим сообщить вам, что {model.EventDate} будет проходить мероприятие под названием \"{model.EventsName}\". " +
                    $"Подробную информацию вы можете узнать по <a Href='https://localhost:5001/Event/Info/{model.Id}'>ссылке</a>.");
            }
        }
    }
}