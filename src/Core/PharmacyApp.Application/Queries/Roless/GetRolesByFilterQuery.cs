using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Roless
{
    public class GetRolesByFilterQuery : PagingRquestModel, IRequest<PagingModel<RolesVM>>
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
