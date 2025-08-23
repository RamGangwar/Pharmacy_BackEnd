using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Inventorys

{
    public class CreateInventoryCommand : IRequest<ResponseModel<InventoryVM>>
    {
        public int InventoryId { get; set; }
        public int MedicineId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string SourceNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
