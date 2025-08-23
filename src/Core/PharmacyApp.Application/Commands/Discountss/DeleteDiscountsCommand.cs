using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Discountss

{
    public class DeleteDiscountsCommand : IRequest<ResponseModel>
    {
        public int DiscountId { get; set; }
    }
}
