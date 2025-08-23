using PharmacyApp.Application.Queries.PurchaseOrders;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrder>
    {
        Task<IEnumerable<PurchaseOrderVM>> GetByPaging(GetPurchaseOrderByFilterQuery filterQuery);
    }
}

