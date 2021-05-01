using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Health.V1;
using Grpc.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace gRPCTest.Services.Services
{
    public class StatusService : BackgroundService
    {
        private readonly Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckService _healthCheckService;
        private readonly HealthServiceImpl _healthService;

        public StatusService(HealthServiceImpl healthService, Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckService healthCheckService)
        {
            _healthService = healthService;
            _healthCheckService = healthCheckService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var health = await _healthCheckService.CheckHealthAsync(stoppingToken);

                _healthService.SetStatus("GREETER",
                    health.Status == HealthStatus.Healthy
                        ? HealthCheckResponse.Types.ServingStatus.Serving
                        : HealthCheckResponse.Types.ServingStatus.NotServing);

                _healthService.SetStatus(string.Empty,
                    health.Status == HealthStatus.Healthy
                        ? HealthCheckResponse.Types.ServingStatus.Serving
                        : HealthCheckResponse.Types.ServingStatus.NotServing);

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
