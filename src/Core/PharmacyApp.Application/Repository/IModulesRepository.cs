using PharmacyApp.Application.Queries.Modules;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IModulesRepository : IGenericRepository<Modules>
    {
        Task<IEnumerable<ModulesVM>> GetByPaging(GetModulesByFilterQuery filterQuery);
    }
}

