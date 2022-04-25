using System.ComponentModel.DataAnnotations;

namespace MorningSchool.Models
{
    public class Class
    {
        public int Id { get; set; }
        [MaxLength(20)] 
        public string ClassName { get; set; }
        public ClassroomTeacher ClassroomTeacher { get; set; }
        public int ClassroomTeacherId { get; set; }
    }
}