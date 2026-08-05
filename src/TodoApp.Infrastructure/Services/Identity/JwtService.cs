using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TodoApp.Application.Contracts.Generator;
using TodoApp.Infrastructure.Options;
using TodoApp.Shared.Domains.Auth;

namespace TodoApp.Infrastructure.Services.Identity
{
    internal class JwtService : IJwtService
    {
        private readonly JwtConfiguration _jwtConfig;

        public JwtService(IOptions<JwtConfiguration> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public TokenResult GenerateToken(IEnumerable<Claim> claims)
        {
            return GenerateIdentityToken(claims);
        }

        private TokenResult GenerateIdentityToken(IEnumerable<Claim> claims)
        {
            DateTime expiry = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryMinutes);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtConfig.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims,
                expires: expiry,
                signingCredentials: signingCredentials);

            return new TokenResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = GenerateRefreshToken(),
                Expiry = expiry
            };
        }

        private string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
