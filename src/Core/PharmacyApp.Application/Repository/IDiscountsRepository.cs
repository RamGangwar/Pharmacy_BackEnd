using PharmacyApp.Application.Queries.Discountss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IDiscountsRepository : IGenericRepository<Discounts>
    {
        Task<IEnumerable<DiscountsVM>> GetByPaging(GetDiscountsByFilterQuery filterQuery);
    }
}

