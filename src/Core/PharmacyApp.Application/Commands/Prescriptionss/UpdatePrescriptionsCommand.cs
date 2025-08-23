using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Prescriptionss

{
    public class UpdatePrescriptionsCommand : IRequest<ResponseModel>
    {
        public int prescription_id { get; set; }
        public int PatientId { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public string DoctorName { get; set; }
        public string DoctorContact { get; set; }
        public string Notes { get; set; }
    }
}
