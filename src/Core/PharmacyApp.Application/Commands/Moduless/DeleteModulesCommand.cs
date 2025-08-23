using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Moduless {
 public class DeleteModulesCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "ModuleId is required")] public int ModuleId {get; set;}
}
}
