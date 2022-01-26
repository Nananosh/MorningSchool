using System;
using MorningSchool.Models;

namespace MorningSchool.ViewModels.Schedule
{
    public class ScheduleViewModel
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