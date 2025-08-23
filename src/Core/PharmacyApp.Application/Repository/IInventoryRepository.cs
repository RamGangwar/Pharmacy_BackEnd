using PharmacyApp.Application.Queries.Inventorys;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<IEnumerable<InventoryVM>> GetByPaging(GetInventoryByFilterQuery filterQuery);
    }
}

