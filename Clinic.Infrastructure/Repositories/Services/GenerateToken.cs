
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Clinic.Infrastructure.Repositories.Services
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration configuration;

        public GenerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetAndCreateToken(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(ClaimTypes.Email, appUser.Email),
                };

            var Security = configuration["Token:Secret"];
            var key = Encoding.ASCII.GetBytes(Security);

            var signingCredentials = new
                SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256);


            SecurityTokenDescriptor tokenDecriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = configuration["Token:Issuer"],
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
               ,
                NotBefore = DateTime.Now,
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDecriptor);

            return handler.WriteToken(token);
        }
    }
}
