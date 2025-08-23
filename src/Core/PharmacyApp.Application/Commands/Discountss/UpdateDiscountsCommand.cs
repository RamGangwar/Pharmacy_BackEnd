using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Discountss

{
    public class UpdateDiscountsCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "DiscountId is required")] public int DiscountId { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MedicineId { get; set; }
    }
}
