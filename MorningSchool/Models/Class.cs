using System.Collections.Generic;
using Newtonsoft.Json;

namespace MorningSchool.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        [JsonIgnore]
        public List<Schedule> Schedules { get; set; }
    }
}