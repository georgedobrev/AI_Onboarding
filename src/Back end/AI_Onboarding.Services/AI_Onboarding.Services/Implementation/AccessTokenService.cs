using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AI_Onboarding.Services.Implementation
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly RefreshTokenService _refreshTokenService;

        public AccessTokenService(RefreshTokenService refreshTokenService, IConfiguration configuration)
        {
            _refreshTokenService = refreshTokenService;
            _configuration = configuration;
        }

        public TokenResponse CreateToken(TokenRequest request)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInMinutes);

            var expiration = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);

            var token = CreateJwtToken(
                CreateClaims(request),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = _refreshTokenService.GenerateToken(request.Username)
            };
        }

        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }

        private Claim[] CreateClaims(TokenRequest request)
        {
            return new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, request.Username)
            };
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                    ),
                    SecurityAlgorithms.HmacSha256
                );
        }
    }
}

