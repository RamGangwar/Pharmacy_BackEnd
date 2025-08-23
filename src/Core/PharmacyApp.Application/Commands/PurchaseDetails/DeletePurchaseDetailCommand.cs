using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseDetails

{
    public class DeletePurchaseDetailCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "DetailId is required")]
        public int DetailId { get; set; }
    }
}
