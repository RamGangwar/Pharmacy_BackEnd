using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Supplierss
{
    public class GetSuppliersByFilterQuery : PagingRquestModel, IRequest<PagingModel<SuppliersVM>>
    {
        public int? SupplierId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
    }
}
