using AutoMapper;
using SkeletonApi.IotHub.Model;
using SkeletonApi.IotHub.Services.Handler;
using System.Text.Json;
using SkeletonApi.IotHub.Services.Store;
using SkeletonApi.IotHub.Helpers;
using System.Globalization;
using SkeletonApi.Application.Interfaces.Repositories.Dapper;
using SkeletonApi.Domain.Entities.Tsdb;
using SkeletonApi.Application.Interfaces.Repositories.Configuration.Dapper;

namespace SkeletonApi.IotHub.Services
{
    public class PersistedConsumer : BackgroundService
    {
        private readonly IIoTHubEventHandler<MqttRawDataEncapsulation> _mqttStoreEventHandler;
        private readonly IotHubMachineHealthEventHandler _machineHealthEventHandler;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly StatusMachineStore _StatusStore;

        public PersistedConsumer(IIoTHubEventHandler<MqttRawDataEncapsulation> mqttStoreEventHandler,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            IotHubMachineHealthEventHandler machineHealthEventHandler,
            StatusMachineStore machineStore
            )
        {
            _StatusStore = machineStore;
            _mqttStoreEventHandler = mqttStoreEventHandler;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _machineHealthEventHandler = machineHealthEventHandler;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _mqttStoreEventHandler.Subscribe(
                subscriberName: typeof(PersistedConsumer).Name,
                action: async (val) =>
            {
                if (val.mqttRawData != null && val.topics != null)
                {
                    await PersistDeviceDataToDBAsync(val.mqttRawData);
                }
            });

            return Task.CompletedTask;
        }        
        private async Task PersistDeviceDataToDBAsync(MqttRawData value)
        {
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(value));
            if (value.Values is not null)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var scoped = scope.ServiceProvider.GetRequiredService<IDiviceDateRepository>();
                        List<MqttRawValue> mqttRawValues = new List<MqttRawValue>();
                        foreach (var row in value?.Values)
                        {
                            mqttRawValues.Add(row);
                        }
                        var mqttRawValueEntities = _mapper.Map<IEnumerable<MqttRawValueEntity>>(mqttRawValues);
                        scoped.Creates(mqttRawValueEntities);
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }

        }
    }
}