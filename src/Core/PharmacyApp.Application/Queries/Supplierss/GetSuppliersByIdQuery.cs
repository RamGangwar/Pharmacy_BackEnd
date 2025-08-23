using MediatR;
using PharmacyApp.Domain.Model; 
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Supplierss    
{        
public class GetSuppliersByIdQuery : IRequest<ResponseModel<SuppliersVM>>     
{public int SupplierId {get; set;}
}
}
