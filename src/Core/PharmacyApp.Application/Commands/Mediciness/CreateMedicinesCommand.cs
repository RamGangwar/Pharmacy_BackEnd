using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Mediciness

{
    public class CreateMedicinesCommand : IRequest<ResponseModel<MedicinesVM>>
    {
        [Required(ErrorMessage = "Medicine Name Required")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Generic Name Required")]
        public string GenericName { get; set; }
        [Required(ErrorMessage = "Manufacturer Required")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Supplier Required")]
        public int SupplierId { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Price Required")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [Required(ErrorMessage = "Expire Date Required")]
        public DateTime ExpiryDate { get; set; }
    }
}
