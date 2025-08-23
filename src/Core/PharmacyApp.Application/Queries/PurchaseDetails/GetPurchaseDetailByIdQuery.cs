using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseDetails
{
    public class GetPurchaseDetailByIdQuery : IRequest<ResponseModel<PurchaseDetailVM>>
    {
        public int DetailId { get; set; }
    }
}
