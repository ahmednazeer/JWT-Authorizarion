using Jwt.API.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration.Assemblies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt.API.Helpers
{
    public static class JwtHelper
    {
        internal static string GenerateJwtToekn(IdentityUser user,string key)
        {
            var KeyEncoded=Encoding.UTF8.GetBytes(key);
            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1).ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyEncoded), SecurityAlgorithms.HmacSha256)
            };
            var token=jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
