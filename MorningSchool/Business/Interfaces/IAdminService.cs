using System.Collections.Generic;
using System.Threading.Tasks;
using MorningSchool.Models;

namespace MorningSchool.Business.Interfaces
{
    public interface IAdminService
    {
        Task<List<Class>> GetAllClasses();
        Task<Class> EditClass(Class cClass);
        Task<Class> AddClass(Class cClass);
        Task DeleteClass(Class cClass);
    }
}