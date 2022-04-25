using System;
using System.ComponentModel.DataAnnotations;

namespace MorningSchool.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Theme Theme { get; set; }
        public int ThemeId { get; set; }
        public Class? Class { get; set; }
        public int? ClassId { get; set; }
        public Cabinet? Cabinet { get; set; }
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