using System.ComponentModel.DataAnnotations;

namespace MorningSchool.Models
{
    public class ClassroomTeacherViewModel
    {
        public int Id { get; set; }
        [MaxLength(100)] 
        public string Name { get; set; }
        [MaxLength(100)] 
        public string Surname { get; set; }
        [MaxLength(100)] 
        public string Lastname { get; set; }
        [MaxLength(20)] 
        public string TelephoneNumber { get; set; }
    }
}