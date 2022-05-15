using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Models;

namespace MorningSchool.Migrations
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassroomTeacher> ClassroomTeachers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<EventResult> EventResults { get; set; }
        public DbSet<EventSubscription> EventSubscriptions { get; set; }
    }
}