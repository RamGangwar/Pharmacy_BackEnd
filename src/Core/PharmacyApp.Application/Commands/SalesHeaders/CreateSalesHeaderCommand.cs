using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Application.Commands.SalesDetails;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesHeaders

{
    public class CreateSalesHeaderCommand : IRequest<ResponseModel<SalesHeaderVM>>
    {
        public int HeaderId { get; set; }
        [Required(ErrorMessage = "PatientId is required")]
        public int PatientId { get; set; }
        public DateTime? CeatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<CreateSalesDetailCommand> DetailList { get; set; }
    }
}
