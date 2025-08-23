using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Userss

{
    public class UpdateUsersCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
