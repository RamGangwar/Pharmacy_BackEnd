using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Roless

{
    public class CreateRolesCommand : IRequest<ResponseModel<RolesVM>>
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
