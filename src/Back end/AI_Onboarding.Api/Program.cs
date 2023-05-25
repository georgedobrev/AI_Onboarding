using Serilog.AspNetCore;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Microsoft.AspNetCore.Builder;

namespace AI_Onboarding.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); 
            builder.Services.AddSwaggerGen();
            builder.Host.UseSerilog((hostingContext, logger) => logger.ReadFrom.Configuration(hostingContext.Configuration));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            Log.CloseAndFlush();
        }

        
    }
}