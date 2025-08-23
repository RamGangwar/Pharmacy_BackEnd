using PharmacyApp.Application.Commands.Authentication;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Repository
{
    public interface IJWTService 
    {
        Task<AuthenticationResponse> GetTokenAsync(AuthenticationRequest authRequest, string ipAddress);
        Task<AuthenticationResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName);
        Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
}
