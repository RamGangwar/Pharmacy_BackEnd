using MediatR;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ResponseModel<AuthenticationResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IJWTService _jWTService;

        public RefreshTokenHandler(IUnitofWork unitofWork, IJWTService jWTService)
        {
            _unitofWork = unitofWork;
            _jWTService = jWTService;
        }

        public async Task<ResponseModel<AuthenticationResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {

            
            var userRefreshToken = await _unitofWork.UserRefreshToken.GetDetail(request.RefreshToken, request.IpAddress);
            var token = GetJwtToken(userRefreshToken.AccessToken);
            AuthenticationResponse response = await ValidateDetails(token, userRefreshToken);
            if (!response.Succeeded)
                return new ResponseModel<AuthenticationResponse> { Data = response,  Message = response.Message };

            var userName = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var authResponse = await _jWTService.GetRefreshTokenAsync(request.IpAddress, userRefreshToken.UserId,
                userName);
            return new ResponseModel<AuthenticationResponse> { Data = authResponse,  Succeeded = true, Message = "success" };
        }
        private async Task<AuthenticationResponse> ValidateDetails(JwtSecurityToken token, UserRefreshToken userRefreshToken)
        {
            if (userRefreshToken == null)
                return new AuthenticationResponse { Succeeded = false, Message = "Invalid Token Details." };
            if (token.ValidTo > DateTime.Now)
                return new AuthenticationResponse { Succeeded = false, Message = "Token not expired." };
            if (userRefreshToken.ExpiredDate < DateTime.Now)
            {
                userRefreshToken.IsInvalidated = true;
                await _unitofWork.UserRefreshToken.Update(userRefreshToken);
                return new AuthenticationResponse { Succeeded = false, Message = "Refresh Token Expired" };
            }
            return new AuthenticationResponse { Succeeded = true };
        }

        private JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }
}
