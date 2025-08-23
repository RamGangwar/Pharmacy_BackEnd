using Dapper;
using PharmacyApp.Application.Queries.Roless;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class RolesRepository : GenericRepository<Roles>, IRolesRepository
    {
        public RolesRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<RolesVM>> GetByPaging(GetRolesByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, * from Roles where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.RoleName))
            {
                sb.Append(" And RoleName like @RoleName");
                parameters.Add("RoleName", "%" + filterQuery.RoleName + "%");
            }
            
            if (filterQuery.RoleId > 0)
            {
                sb.Append(" And RoleId = @RoleId");
                parameters.Add("RoleId", filterQuery.RoleId);
            }
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<RolesVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

