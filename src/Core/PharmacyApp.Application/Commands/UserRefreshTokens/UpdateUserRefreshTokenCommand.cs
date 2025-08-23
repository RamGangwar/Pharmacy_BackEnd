using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.UserRefreshTokens {
 public class UpdateUserRefreshTokenCommand : IRequest<ResponseModel>
{[Required(ErrorMessage = "UserRefreshTokenId is required")] public int UserRefreshTokenId {get; set;}
[Required(ErrorMessage = "AccessToken is required")] public string AccessToken {get; set;}
[Required(ErrorMessage = "RefreshToken is required")] public string RefreshToken {get; set;}
[Required(ErrorMessage = "IpAddress is required")] public string IpAddress {get; set;}
[Required(ErrorMessage = "IsInvalidated is required")] public bool IsInvalidated {get; set;}
[Required(ErrorMessage = "UserId is required")] public int UserId {get; set;}
[Required(ErrorMessage = "CreatedDate is required")] public DateTime CreatedDate {get; set;}
[Required(ErrorMessage = "ExpiredDate is required")] public DateTime ExpiredDate {get; set;}
}
}
