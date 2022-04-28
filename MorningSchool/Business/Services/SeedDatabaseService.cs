using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MorningSchool.Business.Interfaces;
using MorningSchool.Constants;
using MorningSchool.Migrations;
using MorningSchool.Models;

namespace MorningSchool.Business.Services
{
    public class SeedDatabaseService : ISeedDatabaseService
    {
        private readonly ApplicationContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public SeedDatabaseService(ApplicationContext db, RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.db = db;
            this.userManager = userManager;
        }

        public async Task CreateStartAdmin()
        {
            if (db.Users.Any(x => x.UserName == ConstantsMessages.RoleAdmin))
            {
                Console.WriteLine(ConstantsMessages.AdminAlreadyExists);
            }
            else
            {
                var user = new User
                {
                    Email = "admin@morningschool.com", UserName = ConstantsMessages.RoleAdmin,
                    UserImage = "https://img.icons8.com/material-outlined/200/000000/user--v1.png"
                };

                await userManager.CreateAsync(user, ConstantsMessages.RoleAdminPassword);
                await db.SaveChangesAsync();
                await userManager.AddToRoleAsync(user, ConstantsMessages.RoleAdmin);
                Console.WriteLine(ConstantsMessages.AdminSuccessfullyCreated);
            }
        }

        public async Task CreateStartRole()
        {
            if (db.Roles.Any(x => x.Name == "Admin"))
            {
                Console.WriteLine("Роль админа есть");
            }
            else
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                Console.WriteLine("Роль админа создана");
            }
        }
    }
}