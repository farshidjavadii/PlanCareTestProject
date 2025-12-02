using Application.Contracts.Service;
using Application.Mapper;
using Infrastructure.Data;
using Infrastructure.DI;
using Infrastructure.Hubs;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices();
builder.Services.AddSignalR();

builder.Services.AddHostedService<RegistrationCheckService>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<CarMapper>();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<RegistrationHub>("/registrationHub");

app.Run();
