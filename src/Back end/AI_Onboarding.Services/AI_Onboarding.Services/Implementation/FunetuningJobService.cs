using AI_Onboarding.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AI_Onboarding.Services.Implementation
{
    public class FunetuningJobService : BackgroundService, IFunetuningJobService
    {

        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromDays(1));
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public FunetuningJobService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IAIService scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IAIService>();

                    scopedProcessingService.RunScript(_configuration["PythonScript:TrainModelPath"]);
                }
            }
        }
    }
}

