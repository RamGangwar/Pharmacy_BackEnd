using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Inventorys

{
    public class DeleteInventoryCommand : IRequest<ResponseModel>
    {
       public int InventoryId { get; set; }
    }
}
