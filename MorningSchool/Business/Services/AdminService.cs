using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MorningSchool.Business.Interfaces;
using MorningSchool.Migrations;
using MorningSchool.Models;

namespace MorningSchool.Business.Services
{
    public class AdminService : IAdminService
    {
        private ApplicationContext db;

        public AdminService(ApplicationContext db)
        {
            this.db = db;
        }
        
        public async Task<List<Class>> GetAllClasses()
        {
            var classes = await db.Classes.ToListAsync();
            return classes;
        }

        public async Task<Class> EditClass(Class cClass)
        {
            var editClass = await db.Classes.FirstOrDefaultAsync(x => x.Id == cClass.Id);
            editClass.ClassName = cClass.ClassName;
            await db.SaveChangesAsync();
            var editedClass = await db.Classes.FirstOrDefaultAsync(x => x.Id == editClass.Id);
            return editedClass;
        }

        public async Task<Class> AddClass(Class cClass)
        {
            await db.Classes.AddAsync(cClass);
            await db.SaveChangesAsync();
            return cClass;
        }

        public async Task DeleteClass(Class cClass)
        {
            var deleteClass = await db.Classes.FirstOrDefaultAsync(x => x.Id == cClass.Id);
            db.Remove(deleteClass);
            await db.SaveChangesAsync();
        }
    }
}