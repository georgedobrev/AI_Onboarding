namespace AI_Onboarding.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data;
using AI_Onboarding.Api.Filter;
using Serilog;
using AI_Onboarding.Api.Filter.IExceptionFilter;

public static class ServiceCollectionExtension 
{
    
    public static IServiceCollection RegisterDbContext(IServiceCollection services, IConfiguration configuration)
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

