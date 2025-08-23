using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Modules
{
    public class GetModulesByFilterQuery : IRequest<List<ModulesVM>>
    {
        public int RoleId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsPermission { get; set; } = false;
    }
}
