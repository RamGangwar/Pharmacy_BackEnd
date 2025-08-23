using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Prescriptionss
{
    public class GetPrescriptionsByIdQuery : IRequest<ResponseModel<PrescriptionsVM>>
    {
        public int prescription_id { get; set; }
    }
}
