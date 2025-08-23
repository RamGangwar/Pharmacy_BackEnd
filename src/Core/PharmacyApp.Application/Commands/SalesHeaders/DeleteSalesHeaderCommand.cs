using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesHeaders {
 public class DeleteSalesHeaderCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "HeaderId is required")] 
 public int HeaderId {get; set;}
}
}
