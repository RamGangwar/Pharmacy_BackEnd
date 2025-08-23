using Dapper;
using PharmacyApp.Application.Queries.SalesDetails;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class SalesDetailRepository : GenericRepository<SalesDetail>, ISalesDetailRepository
    {
        public SalesDetailRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<SalesDetailVM>> GetByPaging(GetSalesDetailByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from SalesDetail  where 1=1 ");
            if (filterQuery.DetailId>0)
            {
                sb.Append(" And DetailId = @DetailId");
                parameters.Add("DetailId",  filterQuery.DetailId);
            }
            if (filterQuery.HeaderId>0)
            {
                sb.Append(" And HeaderId = @HeaderId");
                parameters.Add("HeaderId",  filterQuery.HeaderId);
            }
            if (filterQuery.MedicineId>0)
            {
                sb.Append(" And MedicineId = @MedicineId");
                parameters.Add("MedicineId",  filterQuery.MedicineId);
            }
           
                       
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<SalesDetailVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

