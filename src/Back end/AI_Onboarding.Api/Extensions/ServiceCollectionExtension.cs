using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.Services.Implementation;
using System.Reflection;
using NuGet.Protocol;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();

        return services;
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScopedServiceTypes(typeof(TokenService).Assembly, typeof(IService));

        return services;
    }

    public static IServiceCollection ConfigureAuth(IServiceCollection services, IConfiguration configuration)
    {
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


    private static IServiceCollection AddScopedServiceTypes(this IServiceCollection services, Assembly assembly, Type fromType)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.IsClass && !x.IsAbstract && fromType.IsAssignableFrom(x))
            .Select(x => new
            {
                Interface = x.GetInterface($"I{x.Name}"),
                Implementation = x
            });

        foreach (var serviceType in serviceTypes)
        {
            services.AddScoped(serviceType.Interface, serviceType.Implementation);
        }

        return services;
    }

}