using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseDetails
{
    public class GetPurchaseDetailByFilterQuery : PagingRquestModel, IRequest<PagingModel<PurchaseDetailVM>>
    {
        public int DetailId { get; set; }
        public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
    }
}
