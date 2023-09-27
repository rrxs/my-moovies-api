using Microsoft.IdentityModel.Tokens;
using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMooviesApi.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        private JwtConfig _jwtConfig;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtConfig = _configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
        }

        public string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfig.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
