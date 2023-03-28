using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NakhleNakhoda.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NakhleNakhoda.Services.Security
{
    public class JwtAuth : IJwtAuth
    {
        private readonly IConfiguration _configuration;

        public JwtAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Generate(Member user, IList<string> role)
        {

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!));
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
