using MediatR;
using PharmacyApp.Domain.Model; 
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesDetails    
{        
public class GetSalesDetailByIdQuery : IRequest<ResponseModel<SalesDetailVM>>     
{public int DetailId {get; set;}
}
}
