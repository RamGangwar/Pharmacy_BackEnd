using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Patientss

{
    public class UpdatePatientsCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "PatientId is required")] 
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime? Reg_Date { get; set; }
    }
}
