using PharmacyApp.Application.Queries.Modules;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IModuleRepository : IGenericRepository<Modules>
    {
        Task<List<ModulesVM>> GetByPaging(GetModulesByFilterQuery filterQuery);
        Task<List<ModulesVM>> GetListForAll();
        Task<bool> SaveEntityOrModel(int type);
        Task<string> SaveColumns(string entityname, int type=0);
    }
}
