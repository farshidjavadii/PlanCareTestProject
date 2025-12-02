using Application.Contracts.Service;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal class RegistrationCheckService : BackgroundService
    {
        private readonly ICarService _service;
        private readonly IHubContext<RegistrationHub> _hub;

        public RegistrationCheckService(ICarService service, IHubContext<RegistrationHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var statuses = await _service.CheckRegistrationStatusAsync();
                await _hub.Clients.All.SendAsync("UpdateRegistration", statuses);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
