using System.Security.Cryptography;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AI_Onboarding.Services.Implementation
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly Repository<User> _repository;
        private readonly IConfiguration _configuration;

        public RefreshTokenService(Repository<User> repository, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public string GenerateToken(string username)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);

            var user = _repository.FindByCondition(x => x.UserName == username);

            if (user is null)
            {
                return "";
            }

            if (user.RefreshToken is null || DateTime.Compare(user.RefreshTokenExpiryTime, DateTime.Now) > 0)
            {
                user.RefreshToken = token;
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                _repository.Update(user);
                _repository.SaveChanges();
            }

            return token;
        }
    }
}

