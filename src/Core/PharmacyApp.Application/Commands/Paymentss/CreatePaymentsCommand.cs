using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Paymentss

{
    public class CreatePaymentsCommand : IRequest<ResponseModel<PaymentsVM>>
    {
        public int HeaderId { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
    }
}
