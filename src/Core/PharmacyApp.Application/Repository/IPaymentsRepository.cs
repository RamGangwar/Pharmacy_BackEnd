using PharmacyApp.Application.Queries.Paymentss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IPaymentsRepository : IGenericRepository<Payments>
    {
        Task<IEnumerable<PaymentsVM>> GetByPaging(GetPaymentsByFilterQuery filterQuery);
    }
}

