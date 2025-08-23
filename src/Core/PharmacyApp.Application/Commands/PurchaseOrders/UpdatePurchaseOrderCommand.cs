using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseOrders

{
    public class UpdatePurchaseOrderCommand : IRequest<ResponseModel>
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; }
        [Required(ErrorMessage = "SupplierId is required")]
        public int SupplierId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
