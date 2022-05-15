using System;
using System.ComponentModel.DataAnnotations;
using MorningSchool.ViewModels;

namespace MorningSchool.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public ThemeViewModel Theme { get; set; }
        public int ThemeId { get; set; }
        public ClassViewModel? Class { get; set; }
        public int? ClassId { get; set; }
        public CabinetViewModel? Cabinet { get; set; }
        public int? CabinetId { get; set; }
        [MaxLength(300)] 
        public string EventsName { get; set; }
        public string Description { get; set; }
        [MaxLength(200)] 
        public string EventManager { get; set; }
        public DateTime EventDate { get; set; }
        public string ImgUrl { get; set; }
    }
}