using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Mediciness

{
    public class UpdateMedicinesCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Medicine Id is required")]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string Manufacturer { get; set; }
        public int SupplierId { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
