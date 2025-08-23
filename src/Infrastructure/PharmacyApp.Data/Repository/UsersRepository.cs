using Dapper;
using PharmacyApp.Application.Queries.Userss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<UsersVM>> GetByPaging(GetUsersByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
           
            DynamicParameters parameters = new DynamicParameters();

            sb.Append("Select COUNT(1) OVER() as TotalRecord, U.*,R.RoleName from Users U inner join Roles R on U.RoleId=R.RoleId where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.FullName))
            {
                sb.Append(" And FullName like @FullName");
                parameters.Add("FullName", "%" + filterQuery.FullName + "%");
            }
            
            if (!string.IsNullOrEmpty(filterQuery.Email))
            {
                sb.Append(" And Email = @Email");
                parameters.Add("Email", filterQuery.Email);
            }
            if (!string.IsNullOrEmpty(filterQuery.MobileNo))
            {
                sb.Append(" And MobileNo = @MobileNo");
                parameters.Add("MobileNo", filterQuery.MobileNo);
            }
            if (!string.IsNullOrEmpty(filterQuery.UserName))
            {
                sb.Append(" And UserName = @UserName");
                parameters.Add("UserName", filterQuery.UserName);
            }
            if (filterQuery.UserId > 0)
            {
                sb.Append(" And UserId = @UserId");
                parameters.Add("UserId", filterQuery.UserId);
            }
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);

            return (await _DbConnection.QueryAsync<UsersVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

