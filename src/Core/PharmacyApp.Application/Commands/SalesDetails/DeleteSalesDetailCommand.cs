using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesDetails

{
    public class DeleteSalesDetailCommand : IRequest<ResponseModel>
    {
        public int DetailId { get; set; }
    }
}
