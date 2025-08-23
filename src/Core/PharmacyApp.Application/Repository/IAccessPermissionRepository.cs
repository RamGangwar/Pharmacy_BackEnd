using PharmacyApp.Application.Queries.AccessPermissions;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IAccessPermissionRepository : IGenericRepository<AccessPermission>
    {
        Task<IEnumerable<AccessPermissionVM>> GetByPaging(GetAccessPermissionByFilterQuery filterQuery);
    }
}

