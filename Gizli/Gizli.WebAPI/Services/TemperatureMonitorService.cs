using Gizli.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Gizli.WebAPI.Services
{
    internal sealed class TemperatureMonitorService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<TemperatureMonitorService> _logger;
        private readonly Random _random = new();

        public TemperatureMonitorService(IServiceProvider provider, ILogger<TemperatureMonitorService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Temperature monitor service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                // Generate a temperature between 20 and 100
                var temp = Math.Round(20 + _random.NextDouble() * (100 - 20), 2);
                _logger.LogInformation("Generated temperature: {Temp}", temp);

                if (temp > 80)
                {
                    try
                    {
                        using var scope = _provider.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var id = Guid.NewGuid();
                        var now = DateTime.UtcNow;

                        // Use raw SQL to insert alarm log
                        await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO AlarmLogs (Id, Temperature, CreatedAt) VALUES ({id}, {temp}, {now})", stoppingToken);

                        _logger.LogWarning("Alarm logged. Temperature {Temp} at {Now}", temp, now);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to log alarm");
                    }
                }

                // Wait 5 seconds
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Temperature monitor service stopping.");
        }
    }
}
