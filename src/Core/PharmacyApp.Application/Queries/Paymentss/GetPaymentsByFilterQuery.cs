using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Paymentss
{
    public class GetPaymentsByFilterQuery : PagingRquestModel, IRequest<PagingModel<PaymentsVM>>
    {
        public int PaymentId { get; set; }
        public int HeaderId { get; set; }
        public DateTime? PaymentDateFrom { get; set; }
        public DateTime? PaymentDateTo { get; set; }
        public string PaymentMethod { get; set; }
    }
}
