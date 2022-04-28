using System.ComponentModel.DataAnnotations;

namespace MorningSchool.Models
{
    public class ThemeViewModel
    {
        public int Id { get; set; }
        [MaxLength(200)] 
        public string ThemeName { get; set; }
    }
}