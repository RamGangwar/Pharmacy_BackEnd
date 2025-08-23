using Dapper;
using PharmacyApp.Application.Queries.Supplierss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class SuppliersRepository : GenericRepository<Suppliers>, ISuppliersRepository
    {
        public SuppliersRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<SuppliersVM>> GetByPaging(GetSuppliersByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, * from Suppliers where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.CompanyName))
            {
                sb.Append(" And CompanyName like @CompanyName");
                parameters.Add("CompanyName", "%" + filterQuery.CompanyName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.ContactName))
            {
                sb.Append(" And ContactName like @ContactName");
                parameters.Add("ContactName", "%" + filterQuery.ContactName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.Address))
            {
                sb.Append(" And Address like @Address");
                parameters.Add("Address", "%" + filterQuery.Address + "%");
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
            if (filterQuery.SupplierId > 0)
            {
                sb.Append(" And SupplierId = @SupplierId");
                parameters.Add("SupplierId", filterQuery.SupplierId);
            }
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<SuppliersVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

