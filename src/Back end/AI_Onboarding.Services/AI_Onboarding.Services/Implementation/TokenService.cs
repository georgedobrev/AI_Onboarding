﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.UserModels;
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

        public TokenViewModel GenerateAccessToken(string email, int id)
        {
            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expiration = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);

            var token = CreateJwtToken(
                CreateClaims(email, id),
                CreateSigningCredentials(),
                expiration
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var dbUser = _repository.Find(id);

            if (dbUser.RefreshTokenExpiryTime < DateTime.UtcNow || dbUser.RefreshToken is null)
            {
                dbUser.RefreshToken = GenerateRefreshToken();
                int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                dbUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays); ;
            }

            _repository.Update(dbUser);
            _repository.SaveChanges();

            return new TokenViewModel
            {
                Token = accessToken,
                RefreshToken = dbUser.RefreshToken

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

        private Claim[] CreateClaims(string email, int id)
        {
            return new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
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

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);

            return token;
        }
    }
}