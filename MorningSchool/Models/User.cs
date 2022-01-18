using Microsoft.AspNetCore.Identity;

namespace MorningSchool.Models
{
    public class User : IdentityUser
    {
        public string UserImage { get; set; }
    }
}