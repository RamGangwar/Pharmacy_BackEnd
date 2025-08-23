using Dapper;
using PharmacyApp.Application.Queries.Inventorys;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<InventoryVM>> GetByPaging(GetInventoryByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from Inventory  where 1=1 ");
            if (filterQuery.InventoryId > 0)
            {
                sb.Append(" And InventoryId = @InventoryId");
                parameters.Add("InventoryId", filterQuery.InventoryId);
            }
            if (filterQuery.MedicineId > 0)
            {
                sb.Append(" And MedicineId = @MedicineId");
                parameters.Add("MedicineId", filterQuery.MedicineId);
            }
            if (!string.IsNullOrEmpty(filterQuery.Type))
            {
                sb.Append(" And Type like @Type");
                parameters.Add("Type", "%" + filterQuery.Type + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.SourceNumber))
            {
                sb.Append(" And SourceNumber like @SourceNumber");
                parameters.Add("SourceNumber", "%" + filterQuery.SourceNumber + "%");
            }
            if(filterQuery.TransactionDateFrom!=null)
            {
                sb.Append(" And TransactionDate >= @TransactionDateFrom");
                parameters.Add("TransactionDateFrom", filterQuery.TransactionDateFrom);
            }
            if (filterQuery.TransactionDateTo != null)
            {
                sb.Append(" And TransactionDate <= @TransactionDateTo");
                parameters.Add("TransactionDateTo", filterQuery.TransactionDateTo);
            }


            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<InventoryVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

