using MediatR;
using Newtonsoft.Json;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Application.Commands.Authentication
{
    public class AuthenticationRequest : IRequest<ResponseModel<AuthenticationResponse>>
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}


