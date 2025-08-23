using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Paymentss

{
    public class DeletePaymentsCommand : IRequest<ResponseModel>
    {
         public int PaymentId { get; set; }
    }
}
