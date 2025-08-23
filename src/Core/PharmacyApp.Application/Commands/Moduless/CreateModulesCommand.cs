using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Moduless {
 public class CreateModulesCommand : IRequest<ResponseModel<ModulesVM>>
{[Required(ErrorMessage = "ModuleId is required")] public int ModuleId {get; set;}
[Required(ErrorMessage = "ModuleName is required")] public string ModuleName {get; set;}
[Required(ErrorMessage = "PageURL is required")] public string PageURL {get; set;}
 public string IconURL {get; set;}
 public int ParentId {get; set;}
[Required(ErrorMessage = "DisplayOrder is required")] public int DisplayOrder {get; set;}
[Required(ErrorMessage = "IsActive is required")] public bool IsActive {get; set;}
}
}
