using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Roless {
 public class DeleteRolesCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "RoleId is required")] public int RoleId {get; set;}
}
}
