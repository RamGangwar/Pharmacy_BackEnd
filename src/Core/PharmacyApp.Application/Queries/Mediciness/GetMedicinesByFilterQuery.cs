using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Mediciness
{
    public class GetMedicinesByFilterQuery : PagingRquestModel, IRequest<PagingModel<MedicinesVM>>
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string Manufacturer { get; set; }
        public int SupplierId { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
