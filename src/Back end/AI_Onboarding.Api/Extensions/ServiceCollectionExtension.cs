using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data;
using AI_Onboarding.Api.Filter;
using Serilog;
using AI_Onboarding.Api.Filter.IExceptionFilter;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.Services.Implementation;
using AI_Onboarding.ViewModels.UserModels.UserProfiles;
using AI_Onboarding.Services.Abstract;
using Microsoft.AspNetCore.Identity;

public static class ServiceCollectionExtension 
{
    public static IServiceCollection RegisterDbContext(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("<from appsettings.{Environment}.json>")));
    }

   public static IServiceCollection RegisterFilters(this IServiceCollection services)
    {

        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
        });

        return services;
    }

}

        services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();

        if (environment.IsDevelopment())
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
        }

        return services;
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddAutoMapper(typeof(UserProfile));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddAuthorization();

        return services;
    }
}