using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesHeaders

{
    public class UpdateSalesHeaderCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "HeaderId is required")]
        public int HeaderId { get; set; }
        public string HeaderNUmber { get; set; }
        [Required(ErrorMessage = "PatientId is required")]
        public int PatientId { get; set; }
        public DateTime? CeatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
    }
}
