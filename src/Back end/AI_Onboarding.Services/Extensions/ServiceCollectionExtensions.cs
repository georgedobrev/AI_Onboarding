using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AI_Onboarding.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("YourConnectionString");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
