using PharmacyApp.Application.Queries.SalesHeaders;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface ISalesHeaderRepository : IGenericRepository<SalesHeader>
    {
        Task<IEnumerable<SalesHeaderVM>> GetByPaging(GetSalesHeaderByFilterQuery filterQuery);
    }
}

