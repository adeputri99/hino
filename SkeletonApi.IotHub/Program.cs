using SkeletonApi.Application.Interfaces;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Infrastructure.Services;
using SkeletonApi.IotHub.Configurations;
using SkeletonApi.IotHub.Extensions;
using SkeletonApi.IotHub.Hubs;
using SkeletonApi.IotHub.Model;
using SkeletonApi.IotHub.Services;
using SkeletonApi.IotHub.Services.Handler;
using SkeletonApi.IotHub.Services.Store;
using SkeletonApi.Persistence.IServiceCollectionExtensions;
using SkeletonApi.Persistence.Repositories.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedMqttClient(builder.Configuration);
builder.Services.AddConfiuredCors(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.ConfigureIdentity();
builder.Services.AddPersistenceLayer(builder.Configuration);

builder.Services.AddScoped<IDapperReadDbConnection, DapperReadDbConnection>();
builder.Services.AddScoped<IDapperWriteDbConnection, DapperWriteDbConnection>();

builder.Services.AddSingleton<StatusMachineStore>();
builder.Services.AddSingleton<TaskStore>();
builder.Services.AddSingleton<NotificationStore>();
builder.Services.AddHttpClient<IRestApiClientService, RestApiClientService>();

builder.Services.AddSingleton<IIoTHubEventHandler<MqttRawDataEncapsulation>, IotHubMqttEventHandler>();
builder.Services.AddSingleton<IotHubMachineHealthEventHandler, IotHubMachineHealthEventHandler>();
builder.Services.AddSingleton<IotHubNotificationEventHandler, IotHubNotificationEventHandler>();
builder.Services.AddSingleton<IoTHubTaskEventHandler, IoTHubTaskEventHandler>();

builder.Services.AddHostedService<PersistedConsumer>();
builder.Services.AddHostedService<NotificationConsumer>();

builder.Services.AddSignalR(opt =>
{
    opt.EnableDetailedErrors = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseWebSockets();
app.UseAuthorization();
app.UseConfiguredSignalR();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<BrokerHub>("/iothub");
    endpoints.MapHub<MachineHealthHub>("/machine-health-hub");
    endpoints.MapHub<TaskHub>("/task-hub");
    endpoints.MapHub<NotificationHub>("/notification-hub");
});

app.MapGet("api/machine-health-subject", (StatusMachineStore machineStore) =>
{
    var data = machineStore.GetAllMachine();
    return data;
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}