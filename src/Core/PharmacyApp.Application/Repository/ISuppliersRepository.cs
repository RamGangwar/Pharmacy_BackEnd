using PharmacyApp.Application.Queries.Supplierss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface ISuppliersRepository : IGenericRepository<Suppliers>
    {
        Task<IEnumerable<SuppliersVM>> GetByPaging(GetSuppliersByFilterQuery filterQuery);
    }
}

