using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Prescriptionss {
 public class DeletePrescriptionsCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "prescription_id is required")] 
 public int prescription_id {get; set;}
}
}
