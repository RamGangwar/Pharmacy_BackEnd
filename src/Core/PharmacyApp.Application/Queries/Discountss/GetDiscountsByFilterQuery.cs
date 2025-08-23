using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Discountss
{
    public class GetDiscountsByFilterQuery : PagingRquestModel, IRequest<PagingModel<DiscountsVM>>
    {
        public int DiscountId { get; set; }
        public string DiscountType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MedicineId { get; set; }
    }
}
