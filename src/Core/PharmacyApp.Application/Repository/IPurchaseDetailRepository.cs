using PharmacyApp.Application.Queries.PurchaseDetails;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IPurchaseDetailRepository : IGenericRepository<PurchaseDetail>
    {
        Task<IEnumerable<PurchaseDetailVM>> GetByPaging(GetPurchaseDetailByFilterQuery filterQuery);
    }
}

