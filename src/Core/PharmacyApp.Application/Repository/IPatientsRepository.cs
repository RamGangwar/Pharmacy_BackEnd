using PharmacyApp.Application.Queries.Patientss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IPatientsRepository : IGenericRepository<Patients>
    {
        Task<IEnumerable<PatientsVM>> GetByPaging(GetPatientsByFilterQuery filterQuery);
    }
}

