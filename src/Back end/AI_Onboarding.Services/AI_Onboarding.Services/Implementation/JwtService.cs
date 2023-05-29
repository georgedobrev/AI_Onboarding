using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AI_Onboarding.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private const int EXPIRATION_MINUTES = 5;

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateToken(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            return new AuthenticationResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration,
                RefreshToken = refreshToken,
                RefreshTokenValidTo = DateTime.Now.AddDays(refreshTokenValidityInDays)
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

        private Claim[] CreateClaims(User user)
        {
            return new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
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

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

