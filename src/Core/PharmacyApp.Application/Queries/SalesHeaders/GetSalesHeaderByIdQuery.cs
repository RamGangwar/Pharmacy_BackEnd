using MediatR;
using PharmacyApp.Domain.Model; 
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesHeaders    
{        
public class GetSalesHeaderByIdQuery : IRequest<ResponseModel<SalesHeaderVM>>     
{public int HeaderId {get; set;}
}
}
