using AI_Onboarding.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AI_Onboarding.Services.Implementation
{
    public class FinetuningJobService : BackgroundService, IFinetuningJobService
    {

        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromDays(1));
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinetuningJobService> _logger;

        public FinetuningJobService(ILogger<FinetuningJobService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
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

                    var result = scopedProcessingService.RunScript(_configuration["PythonScript:TrainModelPath"]);
                    if (!result.Success)
                    {
                        _logger.LogError(result.ErrorMessage);
                    }
                }
            }
        }
    }
}

