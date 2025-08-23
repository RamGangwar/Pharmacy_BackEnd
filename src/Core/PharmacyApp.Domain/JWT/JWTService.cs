using Microsoft.IdentityModel.Tokens;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyApp.Domain.JWT
{
    public class JWTService
   {
        //public JwtSecurityToken GenerateJWToken(Users user, JWTSetting _jwtSettings)
        //{
            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);

            //var roleClaims = new List<Claim>();

            //for (int i = 0; i < roles.Count; i++)
            //{
            //    roleClaims.Add(new Claim("roles", roles[i]));
            //}

           // string ipAddress = null;

            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, user.FirstName + " " + user.LastName),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            //    new Claim("UserId", user.UserId.ToString()),
            //    new Claim("Expire", DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes).ToString())
            //};
            ////.Union(userClaims)
            ////.Union(roleClaims);

            //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //var jwtSecurityToken = new JwtSecurityToken(
            //    issuer: _jwtSettings.Issuer,
            //    audience: _jwtSettings.Audience,
            //    claims: claims,
              
            //    expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            //    signingCredentials: signingCredentials);
            //return jwtSecurityToken;
        //}


        public string GenRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
       
       


    }
}
