using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.AccessPermissions

{
    public class DeleteAccessPermissionCommand : IRequest<ResponseModel>
    {
        public int PermissionId { get; set; }
    }
}
