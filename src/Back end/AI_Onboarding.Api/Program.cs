using Serilog;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.MSSqlServer;


namespace AI_Onboarding.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                connectionString: "YourdatabaseConnectionString",
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" },
                columnOptions: new ColumnOptions(),
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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