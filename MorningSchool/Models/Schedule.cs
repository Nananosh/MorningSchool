using System;

namespace MorningSchool.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string RecurrenceRule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}