using System.Threading.Tasks;

namespace MorningSchool.Business.Interfaces
{
    public interface ISeedDatabaseService
    {
        public Task CreateStartAdmin();
        public Task CreateStartRole();
    }
}