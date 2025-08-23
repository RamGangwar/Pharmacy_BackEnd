using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesDetails

{
    public class CreateSalesDetailCommand : IRequest<ResponseModel<SalesDetailVM>>
    {
        [Required(ErrorMessage = "HeaderId is required")]
        public int HeaderId { get; set; }
        [Required(ErrorMessage = "MedicineId is required")]
        public int MedicineId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
