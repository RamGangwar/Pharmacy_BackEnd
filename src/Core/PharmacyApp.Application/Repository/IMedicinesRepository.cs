using PharmacyApp.Application.Queries.Mediciness;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IMedicinesRepository : IGenericRepository<Medicines>
    {
        Task<IEnumerable<MedicinesVM>> GetByPaging(GetMedicinesByFilterQuery filterQuery);
    }
}

