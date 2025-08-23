using PharmacyApp.Application.Queries.Roless;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IRolesRepository : IGenericRepository<Roles>
    {
        Task<IEnumerable<RolesVM>> GetByPaging(GetRolesByFilterQuery filterQuery);
    }
}

