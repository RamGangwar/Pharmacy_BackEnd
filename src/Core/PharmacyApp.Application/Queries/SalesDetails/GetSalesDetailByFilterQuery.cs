using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesDetails
{
    public class GetSalesDetailByFilterQuery : PagingRquestModel, IRequest<PagingModel<SalesDetailVM>>
    {
        public int DetailId { get; set; }
        public int HeaderId { get; set; }
        public int MedicineId { get; set; }
    }
}
