using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseOrders
{
    public class GetPurchaseOrderByIdQuery : IRequest<ResponseModel<PurchaseOrderVM>>
    {
        public int PurchaseId { get; set; }
    }
}
