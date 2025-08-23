using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.AccessPermissions

{
    public class CreateAccessPermissionCommand : IRequest<ResponseModel<AccessPermissionVM>>
    {
        public int PermissionId { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "ModuleId is required")]
        public int ModuleId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public bool IsActive { get; set; }
    }
}
