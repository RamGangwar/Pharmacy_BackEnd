using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Patientss

{
    public class DeletePatientsCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Patient Id is required")]
        public int PatientId { get; set; }
    }
}
