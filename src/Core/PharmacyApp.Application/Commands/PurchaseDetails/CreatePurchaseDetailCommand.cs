using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseDetails

{
    public class CreatePurchaseDetailCommand : IRequest<ResponseModel<PurchaseDetailVM>>
    {
        public int DetailId { get; set; }
        [Required(ErrorMessage = "PurchaseId is required")]
        public int PurchaseId { get; set; }
        [Required(ErrorMessage = "MedicineId is required")]
        public int MedicineId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
