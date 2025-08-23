using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Roless

{
    public class UpdateRolesCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Role Id is required")] 
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Role Name is required")] 
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
