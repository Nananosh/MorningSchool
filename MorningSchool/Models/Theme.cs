using System.ComponentModel.DataAnnotations;

namespace MorningSchool.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [MaxLength(200)] 
        public string ThemeName { get; set; }
    }
}