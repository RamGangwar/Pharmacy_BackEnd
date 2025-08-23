using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.UserRefreshTokens {
 public class DeleteUserRefreshTokenCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "UserRefreshTokenId is required")] public int UserRefreshTokenId {get; set;}
}
}
