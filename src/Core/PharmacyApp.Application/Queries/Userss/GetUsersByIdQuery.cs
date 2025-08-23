using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Userss
{
    public class GetUsersByIdQuery : IRequest<ResponseModel<UsersVM>>
    {
        public int UserId { get; set; }
    }
}
