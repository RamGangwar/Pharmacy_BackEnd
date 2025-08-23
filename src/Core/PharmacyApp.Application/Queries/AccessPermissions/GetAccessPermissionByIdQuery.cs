using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.AccessPermissions
{
    public class GetAccessPermissionByIdQuery : IRequest<ResponseModel<AccessPermissionVM>>
    {
        public int PermissionId { get; set; }
    }
}
