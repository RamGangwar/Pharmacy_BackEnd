using PharmacyApp.Application.Queries.SalesDetails;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface ISalesDetailRepository : IGenericRepository<SalesDetail>
    {
        Task<IEnumerable<SalesDetailVM>> GetByPaging(GetSalesDetailByFilterQuery filterQuery);
    }
}

