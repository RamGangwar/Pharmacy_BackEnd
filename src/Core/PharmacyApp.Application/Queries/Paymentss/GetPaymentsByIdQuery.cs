using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Paymentss
{
    public class GetPaymentsByIdQuery : IRequest<ResponseModel<PaymentsVM>>
    {
        public int PaymentId { get; set; }
    }
}
