using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.AccessPermissions
{
    public class GetAccessPermissionByFilterQuery : IRequest<ResponseModel<IEnumerable<AccessPermissionVM>>>
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
    }
}
