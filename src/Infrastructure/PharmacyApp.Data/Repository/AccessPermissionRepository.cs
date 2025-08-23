using Dapper;
using PharmacyApp.Application.Queries.AccessPermissions;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class AccessPermissionRepository : GenericRepository<AccessPermission>, IAccessPermissionRepository
    {
        public AccessPermissionRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<AccessPermissionVM>> GetByPaging(GetAccessPermissionByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from AccessPermission  where 1=1 ");
            if (filterQuery.PermissionId > 0)
            {
                sb.Append(" And PermissionId = @PermissionId");
                parameters.Add("PermissionId", filterQuery.PermissionId);
            }
            if (filterQuery.RoleId > 0)
            {
                sb.Append(" And RoleId = @RoleId");
                parameters.Add("RoleId", filterQuery.RoleId);
            }
           

            sb.Append(" ORDER BY ModuleId Asc ");
            return (await _DbConnection.QueryAsync<AccessPermissionVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

