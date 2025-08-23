using PharmacyApp.Application.Queries.Prescriptionss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IPrescriptionsRepository : IGenericRepository<Prescriptions>
    {
        Task<IEnumerable<PrescriptionsVM>> GetByPaging(GetPrescriptionsByFilterQuery filterQuery);
    }
}

