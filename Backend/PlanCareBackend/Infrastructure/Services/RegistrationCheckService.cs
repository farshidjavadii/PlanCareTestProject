using Application.Contracts.Service;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RegistrationCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<RegistrationHub> _hub;
        private readonly ILogger<RegistrationCheckService> _logger;

        public RegistrationCheckService(IServiceScopeFactory scopeFactory, IHubContext<RegistrationHub> hub, ILogger<RegistrationCheckService> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var carService = scope.ServiceProvider.GetRequiredService<ICarService>();
                        var statuses = await carService.CheckRegistrationStatusAsync();
                        await _hub.Clients.All.SendAsync("UpdateRegistration", statuses);
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        _logger.LogError(ex, "An error occurred during scheduled task execution.");
                    }
                    catch { }
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
