using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Inventorys
{
    public class GetInventoryByFilterQuery : PagingRquestModel, IRequest<PagingModel<InventoryVM>>
    {
        public int InventoryId { get; set; }
        public int MedicineId { get; set; }
        public string Type { get; set; }
        public string SourceNumber { get; set; }
        public DateTime? TransactionDateFrom { get; set; }
        public DateTime? TransactionDateTo { get; set; }
    }
}
