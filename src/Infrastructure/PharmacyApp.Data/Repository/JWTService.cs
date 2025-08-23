using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmacyApp.Application.Commands.Authentication;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.JWT;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Data.Repository
{
    public class JWTService : IJWTService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IEncryptRepository _encryptRepository;
        private readonly JWTSetting _jwtSettings;
        public JWTService(IUnitofWork unitofWork, IEncryptRepository encryptRepository, IOptionsMonitor<JWTSetting> optionsMonitor)
        {
            _unitofWork = unitofWork;
            _encryptRepository = encryptRepository;
            _jwtSettings = optionsMonitor.CurrentValue;
        }

        public async Task<AuthenticationResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName)
        {
            var refreshToken = await GenerateRefreshToken();
            var accessToken = await GenerateToken(userName, userId);
            return await SaveTokenDetails(ipAddress, userId, accessToken, refreshToken);
        }

        public async Task<AuthenticationResponse> GetTokenAsync(AuthenticationRequest AuthenticationRequest, string ipAddress)
        {
            try
            {
                var user = await _unitofWork.Users.GetEntityAsync(u => u.UserName == AuthenticationRequest.Username);
                AuthenticationResponse response = new AuthenticationResponse();
                var pass = _encryptRepository.Decrypt(user.Password);
                if (user != null && AuthenticationRequest.Password == pass)
                {
                    string tokenString = await GenerateToken(user.UserName, user.UserId);
                    string refreshToken = await GenerateRefreshToken();
                    //await _unitofWork.UserStatus.UpdateUserStatusCode(user.UserId, "Available", false, DateTime.Now);
                    return await SaveTokenDetails(ipAddress, user.UserId, tokenString, refreshToken);
                }
                return new AuthenticationResponse { };
            }
            catch (Exception ee)
            {
                return new AuthenticationResponse { };
            }
        }

        private async Task<AuthenticationResponse> SaveTokenDetails(string ipAddress, int userId, string tokenString, string refreshToken)
        {
            var res = await _unitofWork.UserRefreshToken.UpdateInValidateToken(userId);
            var user = await _unitofWork.Users.GetById(userId);
            var userRefreshToken = new UserRefreshToken
            {
                CreatedDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokanDuration),
                IpAddress = ipAddress,
                IsInvalidated = false,
                RefreshToken = refreshToken,
                AccessToken = tokenString,
                UserId = userId
            };
            await _unitofWork.UserRefreshToken.Add(userRefreshToken);
            var userresponse = user.Adapt<UserBaseModel>();
            return new AuthenticationResponse { User = userresponse, AccessToken = tokenString, RefreshToken = refreshToken, Succeeded = !string.IsNullOrEmpty(tokenString) ? true : false,Message= !string.IsNullOrEmpty(tokenString) ?"Token Generated":"" };
        }

        private async Task<string> GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(byteArray);

                return Convert.ToBase64String(byteArray);
            }
        }

        private async Task<string> GenerateToken(string userName, int userId)
        {
            var jwtKey = _jwtSettings.Key;
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim("UserId", userId.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
               SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public async Task<bool> IsTokenValid(string accessToken, string ipAddress)
        {
            var isValid = await _unitofWork.UserRefreshToken.GetByToken(accessToken, ipAddress) != null;
            return await Task.FromResult(isValid);
        }

    }
}
