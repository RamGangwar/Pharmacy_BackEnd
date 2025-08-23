using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseOrders {
 public class DeletePurchaseOrderCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "PurchaseId is required")] 
 public int PurchaseId {get; set;}
}
}
