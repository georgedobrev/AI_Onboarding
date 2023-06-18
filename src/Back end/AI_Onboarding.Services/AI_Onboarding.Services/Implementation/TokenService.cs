using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AI_Onboarding.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _repository;

        public TokenService(IRepository<User> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public TokenViewModel GenerateAccessToken(string email, string name, int id, string[] roleNames, bool isLogin = false)
        {
            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expiration = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);

            var accessToken = CreateJwtToken(
                CreateClaims(email, name, id, roleNames),
                CreateSigningCredentials(),
                expiration
            );

            int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

            var refreshToken = CreateRefreshTokenJwt(
                CreateSigningCredentials(),
                refreshTokenExpiration
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            var dbUser = _repository.Find(id);

            if (dbUser.RefreshTokenExpiryTime < DateTime.UtcNow || dbUser.RefreshToken is null || isLogin)
            {
                dbUser.RefreshToken = refreshTokenString;
                dbUser.RefreshTokenExpiryTime = refreshTokenExpiration;
            }

            _repository.Update(dbUser);
            _repository.SaveChanges();

            return new TokenViewModel
            {
                Token = accessTokenString,
                RefreshToken = refreshTokenString
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

        private JwtSecurityToken CreateRefreshTokenJwt(SigningCredentials credentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires: expiration,
                signingCredentials: credentials
            );
        }

        private Claim[] CreateClaims(string email, string name, int id, string[] roleNames)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, name)
            };

            foreach (var roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            return claims.ToArray();
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
