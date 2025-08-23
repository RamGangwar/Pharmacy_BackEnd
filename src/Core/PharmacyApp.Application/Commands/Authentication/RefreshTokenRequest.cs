using MediatR;
using Newtonsoft.Json;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Application.Commands.Authentication
{
    public class RefreshTokenRequest : IRequest<ResponseModel<AuthenticationResponse>>
    {
        //[Required(ErrorMessage = "ExpiredToken is required")]
        //public string ExpiredToken { get; set; }
        [Required(ErrorMessage = "RefreshToken is required")]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public string? IpAddress { get; set; }


    }
}
