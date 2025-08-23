using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Mediciness

{
    public class DeleteMedicinesCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Medicine is required")]
        public int MedicineId { get; set; }
    }
}
