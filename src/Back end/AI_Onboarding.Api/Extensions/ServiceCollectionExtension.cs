using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AI_Onboarding.Data.Repository;

public static class ServiceCollectionExtension
{

    public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
    }

}

