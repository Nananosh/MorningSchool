using System.ComponentModel.DataAnnotations;

namespace MorningSchool.ViewModels
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        [MaxLength(20)] 
        public string ClassName { get; set; }
        public ClassroomTeacherViewModel ClassroomTeacher { get; set; }
        public int ClassroomTeacherId { get; set; }
    }
}