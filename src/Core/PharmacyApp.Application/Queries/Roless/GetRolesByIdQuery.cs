using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Roless
{
    public class GetRolesByIdQuery : IRequest<ResponseModel<RolesVM>>
    {
        public int RoleId { get; set; }
    }
}
