using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Discountss
{
    public class GetDiscountsByIdQuery : IRequest<ResponseModel<DiscountsVM>>
    {
        public int DiscountId { get; set; }
    }
}
