using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Patientss
{
    public class GetPatientsByIdQuery : IRequest<ResponseModel<PatientsVM>>
    {
        public int PatientId { get; set; }
    }
}
