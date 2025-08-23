using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Mediciness
{
    public class GetMedicinesByIdQuery : IRequest<ResponseModel<MedicinesVM>>
    {
        public int MedicineId { get; set; }
    }
}
