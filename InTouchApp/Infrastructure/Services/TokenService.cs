using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InTouchApi.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var created = DateTime.UtcNow;
            var expires = created.AddDays(int.Parse(_configuration["Jwt:ExpiresInDays"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials,
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenDto
            {
                Token = $"bearer {tokenHandler.WriteToken(token)}",
                Created = created,
                Expires = expires
            };
        }
    }
}
