using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Userss

{
    public class DeleteUsersCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "User Id is required")]
        public int UserId { get; set; }
    }
}
