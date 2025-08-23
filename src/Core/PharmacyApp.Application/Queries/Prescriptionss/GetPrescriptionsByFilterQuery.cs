using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Prescriptionss
{
    public class GetPrescriptionsByFilterQuery : PagingRquestModel, IRequest<PagingModel<PrescriptionsVM>>
    {
        public int prescription_id { get; set; }
        public int PatientId { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public string DoctorName { get; set; }
        public string DoctorContact { get; set; }
    }
}
