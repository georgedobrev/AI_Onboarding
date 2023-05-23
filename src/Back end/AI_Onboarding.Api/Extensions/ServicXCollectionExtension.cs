namespace AI_Onboarding.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


public static class ServiceCollectionExtension
{
    public static void AddAIOnboardingServices(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionstring = "ourConnectionString";
        RegisterDbContext(services, connectionstring);



    }

    public static void RegisterDbContext(IServiceCollection services, string connectionstring)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("YourConnectionString")));
    }
}

