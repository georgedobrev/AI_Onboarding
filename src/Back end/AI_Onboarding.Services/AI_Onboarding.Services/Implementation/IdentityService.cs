using AI_Onboarding.Common;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.ResponseModels;
using AI_Onboarding.ViewModels.UserModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace AI_Onboarding.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<UserRole> _repositoryUserRole;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityService> _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public IdentityService(IRepository<User> repository, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenService tokenService, IConfiguration configuration, ILogger<IdentityService> logger, IRepository<Role> repositoryRole,
            IRepository<UserRole> repositoryUserRole, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor, LinkGenerator link)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _logger = logger;
            _repositoryRole = repositoryRole;
            _repositoryUserRole = repositoryUserRole;
            _urlHelper = urlHelper;
            _contextAccessor = httpContextAccessor;
            _linkGenerator = link;
        }

        public async Task<BaseResponseViewModel> RegisterAsync(UserRegistrationViewModel viewUser)
        {
            var messages = "";

            var user = _mapper.Map<User>(viewUser);
            try
            {
                var result = await _userManager.CreateAsync(user, viewUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Employee);
                    return new BaseResponseViewModel { Success = true, ErrorMessage = "" }; ;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        messages += $"{error.Description}%";
                    }

                    return new BaseResponseViewModel { Success = false, ErrorMessage = messages.Remove(messages.Length - 1) };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                messages += $"{ex.Message}";
                return new BaseResponseViewModel { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<TokensResponseViewModel> LoginAsync(UserLoginViewModel user)
        {
            var messages = "";

            try
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

                if (result.Succeeded)
                {
                    var dbUser = _repository.FindByCondition(u => u.Email == user.Email);

                    int id = dbUser.Id;

                    var name = dbUser.FirstName + " " + dbUser.LastName;

                    int roleId = _repositoryUserRole.FindByCondition(ur => ur.UserId == id).RoleId;

                    string[] roleNames = _repositoryRole.FindAllByCondition(r => r.Id == roleId).Select(r => r.Name).ToArray();

                    return new TokensResponseViewModel { Success = true, ErrorMessage = "", Tokens = _tokenService.GenerateAccessToken(user.Email, name, id, roleNames, true) };
                }
                else
                {
                    return new TokensResponseViewModel { Success = false, ErrorMessage = "Invalid email or password", Tokens = null };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                messages += $"{ex.Message}";
                return new TokensResponseViewModel { Success = false, ErrorMessage = messages, Tokens = null };
            }
        }

        public TokensResponseViewModel RefreshTokenAsync(TokenViewModel tokens)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(tokens.Token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                }, out securityToken);
                var validatedToken = securityToken as JwtSecurityToken;

                if (validatedToken?.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return new TokensResponseViewModel { Success = false, ErrorMessage = "Invalid algorithm", Tokens = null };
                }

                var nameIdentifier = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

                var user = _repository.FindByCondition(u => u.Id == nameIdentifier && u.RefreshToken == Uri.UnescapeDataString(tokens.RefreshToken));

                if (user is null || user?.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    return new TokensResponseViewModel { Success = false, ErrorMessage = "Invalid token", Tokens = null };
                }

                int roleId = _repositoryUserRole.FindByCondition(ur => ur.UserId == user.Id).RoleId;

                string[] roleNames = _repositoryRole.FindAllByCondition(r => r.Id == roleId).Select(r => r.Name).ToArray();

                var name = user.FirstName + " " + user.LastName;

                var newTokens = _tokenService.GenerateAccessToken(user.Email, name, user.Id, roleNames);
                return new TokensResponseViewModel { Success = true, ErrorMessage = "", Tokens = newTokens };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return new TokensResponseViewModel { Success = false, ErrorMessage = ex.Message, Tokens = null };
            }
        }

        public async Task<TokensResponseViewModel> GoogleLoginAsync(string token)
        {

            using (var httpClient = new HttpClient())
            {

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims;

                var firstName = claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                var lastName = claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
                var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;

                var user = _repository.FindByCondition(u => u.Email == email);

                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false);

                    int roleId = _repositoryUserRole.FindByCondition(ur => ur.UserId == user.Id).RoleId;

                    string[] roleNames = _repositoryRole.FindAllByCondition(r => r.Id == roleId).Select(r => r.Name).ToArray();

                    var name = user.FirstName + " " + user.LastName;

                    var tokens = _tokenService.GenerateAccessToken(user.Email, name, user.Id, roleNames, true);

                    return new TokensResponseViewModel { Success = true, ErrorMessage = "", Tokens = tokens };
                }
                else
                {
                    var defaultPassword = _configuration["GoogleAuth:DefaulfPasswordHash"];
                    var newUser = new UserRegistrationViewModel { Email = email, Password = defaultPassword, FirstName = firstName, LastName = lastName };
                    var resultRegister = await RegisterAsync(newUser);

                    var loginCredentials = new UserLoginViewModel { Email = email, Password = defaultPassword };
                    var resultLogin = await LoginAsync(loginCredentials);

                    if (resultLogin.Success && resultRegister.Success)
                    {
                        return new TokensResponseViewModel { Success = true, ErrorMessage = "", Tokens = resultLogin.Tokens };
                    }
                    else
                    {
                        return new TokensResponseViewModel { Success = false, ErrorMessage = "Registration failed", Tokens = null };
                    }
                }
            }
        }

        public async Task<BaseResponseViewModel> SendPasswordResetEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var emailSettings = _configuration.GetSection("EmailSettings");
                    var apiKey = emailSettings["ApiKey"];
                    var senderEmail = emailSettings["SenderEmail"];
                    var senderName = emailSettings["Sendername"];

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    string resetUrl = $"{Constants.ResetPasswordBaseUrl}?token={HttpUtility.UrlEncode(token)}&email={HttpUtility.UrlEncode(email)}";

                    var emailTemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../AI_Onboarding.Common/EmailTemplate/PasswordResetEmailTemplate.html");
                    emailTemplatePath = Path.GetFullPath(emailTemplatePath);

                    var emailTemplate = await File.ReadAllTextAsync(emailTemplatePath);

                    var emailBodyWithUrl = emailTemplate.Replace("{{action_url}}", resetUrl);
                    var emailBody = emailBodyWithUrl.Replace("{{name}}", user.FirstName);


                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress(senderEmail, senderName);
                    var to = new EmailAddress(user.Email);
                    var subject = "Password Reset";

                    var msg = MailHelper.CreateSingleEmail(from, to, subject, " ", emailBody);
                    var response = await client.SendEmailAsync(msg);

                    return new BaseResponseViewModel { Success = true, ErrorMessage = string.Empty };
                }
                else
                {
                    return new BaseResponseViewModel { Success = false, ErrorMessage = "User not found" };
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the password reset email.");
                return new BaseResponseViewModel { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}