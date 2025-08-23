using Dapper;
using PharmacyApp.Application.Queries.PurchaseOrders;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<PurchaseOrderVM>> GetByPaging(GetPurchaseOrderByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from PurchaseOrder  where 1=1 ");
            if (filterQuery.PurchaseId>0)
            {
                sb.Append(" And PurchaseId = @PurchaseId");
                parameters.Add("PurchaseId",  filterQuery.PurchaseId);
            }
            if (filterQuery.SupplierId>0)
            {
                sb.Append(" And SupplierId = @SupplierId");
                parameters.Add("SupplierId",  filterQuery.SupplierId);
            }
            if (!string.IsNullOrEmpty(filterQuery.PurchaseNumber))
            {
                sb.Append(" And PurchaseNumber like @PurchaseNumber");
                parameters.Add("PurchaseNumber", "%" + filterQuery.PurchaseNumber + "%");
            }
            if (filterQuery.PurchaseDateFrom != null)
            {
                sb.Append(" And PurchaseDate >= @PurchaseDateFrom");
                parameters.Add("PurchaseDateFrom", filterQuery.PurchaseDateFrom);
            }
            if (filterQuery.PurchaseDateTo != null)
            {
                sb.Append(" And PurchaseDate <= @PurchaseDateTo");
                parameters.Add("PurchaseDateTo", filterQuery.PurchaseDateTo);
            }

            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<PurchaseOrderVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

