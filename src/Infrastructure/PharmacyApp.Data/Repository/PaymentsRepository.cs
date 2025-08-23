using Dapper;
using PharmacyApp.Application.Queries.Paymentss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class PaymentsRepository : GenericRepository<Payments>, IPaymentsRepository
    {
        public PaymentsRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<PaymentsVM>> GetByPaging(GetPaymentsByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from Payments  where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.PaymentMethod))
            {
                sb.Append(" And PaymentMethod like @PaymentMethod");
                parameters.Add("PaymentMethod", "%" + filterQuery.PaymentMethod + "%");
            }
            if (filterQuery.PaymentDateFrom != null)
            {
                sb.Append(" And PaymentDate >= @PaymentDateFrom");
                parameters.Add("PaymentDateFrom", filterQuery.PaymentDateFrom);
            }
            if (filterQuery.PaymentDateTo != null)
            {
                sb.Append(" And PaymentDate <= @PaymentDateTo");
                parameters.Add("PaymentDateTo", filterQuery.PaymentDateTo);
            }

            if (filterQuery.PaymentId > 0)
            {
                sb.Append(" And PaymentId = @PaymentId");
                parameters.Add("PaymentId", filterQuery.PaymentId);
            }
            if (filterQuery.HeaderId > 0)
            {
                sb.Append(" And HeaderId = @HeaderId");
                parameters.Add("HeaderId", filterQuery.HeaderId);
            }

            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<PaymentsVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

