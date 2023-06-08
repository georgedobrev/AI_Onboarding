using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data;
using AI_Onboarding.Api.Filter;
using Serilog;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.Services.Implementation;
using System.Reflection;
using AI_Onboarding.ViewModels.UserModels.UserProfiles;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using AI_Onboarding.Data.NoSQLDatabase.Interfaces;
using AI_Onboarding.Data.NoSQLDatabase;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterDbContext(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
  
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

    public static IServiceCollection ConfigureNoSQLDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(options => new MongoClient(configuration["MongoDBSettings:ConnectionString"]));

        return services;
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IDocumentRepository, DocumentRepository>();

        services.AddScopedServiceTypes(typeof(TokenService).Assembly, typeof(IService));
        services.AddAutoMapper(typeof(UserProfile));
        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
        return services;
    }
    public static IServiceCollection ConfigureAuth(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
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

  public static IServiceCollection RegisterFilters(this IServiceCollection services)
    {

        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
        });

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

    public static IServiceCollection RegisterFilters(this IServiceCollection services)
    {

        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
        });

        return services;
    }
}
