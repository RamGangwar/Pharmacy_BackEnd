using Dapper;
using PharmacyApp.Application.Queries.PurchaseDetails;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class PurchaseDetailRepository : GenericRepository<PurchaseDetail>, IPurchaseDetailRepository
    {
        public PurchaseDetailRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<PurchaseDetailVM>> GetByPaging(GetPurchaseDetailByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from PurchaseDetail  where 1=1 ");
            if (filterQuery.DetailId>0)
            {
                sb.Append(" And DetailId = @DetailId");
                parameters.Add("DetailId",  filterQuery.DetailId);
            }
            if (filterQuery.PurchaseId>0)
            {
                sb.Append(" And PurchaseId = @PurchaseId");
                parameters.Add("PurchaseId",  filterQuery.PurchaseId);
            }
            if (filterQuery.MedicineId>0)
            {
                sb.Append(" And MedicineId = @MedicineId");
                parameters.Add("MedicineId",  filterQuery.MedicineId);
            }
                   
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<PurchaseDetailVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

