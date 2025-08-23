using Dapper;
using PharmacyApp.Application.Queries.Discountss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class DiscountsRepository : GenericRepository<Discounts>, IDiscountsRepository
    {
        public DiscountsRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<DiscountsVM>> GetByPaging(GetDiscountsByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from Discounts  where 1=1 ");
            if (filterQuery.DiscountId > 0)
            {
                sb.Append(" And DiscountId = @DiscountId");
                parameters.Add("DiscountId", filterQuery.DiscountId);
            }
            if (filterQuery.MedicineId > 0)
            {
                sb.Append(" And MedicineId = @MedicineId");
                parameters.Add("MedicineId", filterQuery.MedicineId);
            }
            if (!string.IsNullOrEmpty(filterQuery.DiscountType))
            {
                sb.Append(" And DiscountType like @DiscountType");
                parameters.Add("DiscountType", "%" + filterQuery.DiscountType + "%");
            }
            if (filterQuery.StartDate != null)
            {
                sb.Append(" And StartDate = @StartDate");
                parameters.Add("StartDate", filterQuery.StartDate);
            }
            if (filterQuery.EndDate != null)
            {
                sb.Append(" And EndDate = @EndDate");
                parameters.Add("EndDate", filterQuery.EndDate);
            }

            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<DiscountsVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

