using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Supplierss

{
    public class DeleteSuppliersCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Supplier Id is required")] 
        public int SupplierId { get; set; }
    }
}
