using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Application.Commands.PurchaseDetails;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseOrders

{
    public class CreatePurchaseOrderCommand : IRequest<ResponseModel<PurchaseOrderVM>>
    {
        [Required(ErrorMessage = "PurchaseId is required")]
        public int PurchaseId { get; set; }
        [Required(ErrorMessage = "SupplierId is required")]
        public int SupplierId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<CreatePurchaseDetailCommand> Detail { get; set; }
    }
}
