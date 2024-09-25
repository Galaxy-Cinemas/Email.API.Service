using Galaxi.Bus.Message;
using Galaxi.Email.API.Service.IntegrationEvents.Consumers;
using Galaxi.Email.API.Service.Service;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services.BuildServiceProvider();
var configuration = service.GetService<IConfiguration>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailConsumer>();
    x.AddRequestClient<MovieDetails>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(configuration.GetConnectionString("AzureServiceBus"));

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
