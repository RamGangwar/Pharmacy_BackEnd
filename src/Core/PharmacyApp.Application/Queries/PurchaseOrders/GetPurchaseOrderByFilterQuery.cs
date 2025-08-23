using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseOrders
{
    public class GetPurchaseOrderByFilterQuery : PagingRquestModel, IRequest<PagingModel<PurchaseOrderVM>>
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime? PurchaseDateFrom { get; set; }
        public DateTime? PurchaseDateTo { get; set; }
    }
}
