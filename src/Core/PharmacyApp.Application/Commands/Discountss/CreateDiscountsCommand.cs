using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Discountss

{
    public class CreateDiscountsCommand : IRequest<ResponseModel<DiscountsVM>>
    {
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MedicineId { get; set; }
    }
}
