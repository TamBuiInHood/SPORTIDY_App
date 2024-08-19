using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Utils
{
    public static class GenerateJWTToken
    {
        public static JwtSecurityToken CreateAccessToken(List<Claim> authClaims, IConfiguration configuration, DateTime currentTime)
        {
            var authSigningToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: currentTime.AddMinutes(tokenValidityInMinutes),
                signingCredentials: new SigningCredentials(authSigningToken, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static JwtSecurityToken CreateRefreshToken(List<Claim> authClaims, IConfiguration configuration, DateTime currentTime)
        {
            var authSigningToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: currentTime.AddDays(tokenValidityTime),
                signingCredentials: new SigningCredentials(authSigningToken, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}
