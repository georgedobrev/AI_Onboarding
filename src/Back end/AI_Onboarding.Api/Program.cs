using AI_Onboarding.Data;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.AspNetCore;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;


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
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Host.UseSerilog((hostingContext, logger) => logger.ReadFrom.Configuration(hostingContext.Configuration));

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

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
        }
    }
}