using Application.Contracts.Service;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public RegistrationCheckService(IServiceScopeFactory scopeFactory, IHubContext<RegistrationHub> hub)
        {
            _scopeFactory = scopeFactory;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var carService = scope.ServiceProvider.GetRequiredService<ICarService>();
                    var statuses = await carService.CheckRegistrationStatusAsync();
                    await _hub.Clients.All.SendAsync("UpdateRegistration", statuses);
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
