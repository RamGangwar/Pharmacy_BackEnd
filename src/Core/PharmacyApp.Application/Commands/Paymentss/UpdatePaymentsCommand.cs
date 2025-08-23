using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Paymentss

{
    public class UpdatePaymentsCommand : IRequest<ResponseModel>
    {
        public int PaymentId { get; set; }
        public int HeaderId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
    }
}
