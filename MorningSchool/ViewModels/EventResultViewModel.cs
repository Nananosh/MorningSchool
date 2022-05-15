using MorningSchool.Models;

namespace MorningSchool.ViewModels
{
    public class EventResultViewModel
    {
        public int Id { get; set; }
        public EventViewModel Event { get; set; }
        public int EventId { get; set; }
        public string ImageUrl { get; set; }
    }
}