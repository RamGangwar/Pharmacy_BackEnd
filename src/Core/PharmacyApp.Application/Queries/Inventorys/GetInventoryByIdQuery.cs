using MediatR;
using PharmacyApp.Domain.Model; 
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Inventorys    
{        
public class GetInventoryByIdQuery : IRequest<ResponseModel<InventoryVM>>     
{public int InventoryId {get; set;}
}
}
