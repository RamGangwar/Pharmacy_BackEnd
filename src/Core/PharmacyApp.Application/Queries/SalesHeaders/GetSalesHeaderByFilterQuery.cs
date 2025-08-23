using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesHeaders
{
    public class GetSalesHeaderByFilterQuery : PagingRquestModel, IRequest<PagingModel<SalesHeaderVM>>
    {
        public int HeaderId { get; set; }
        public string HeaderNumber { get; set; }
        public int PatientId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
