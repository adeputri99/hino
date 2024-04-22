using AutoMapper;
using SkeletonApi.Application.Features.Settings.Task;
using SkeletonApi.Application.Interfaces.Repositories.Configuration.Dapper;
using SkeletonApi.Domain.Entities.Tsdb;
using SkeletonApi.IotHub.Model;
using SkeletonApi.IotHub.Services.Handler;
using SkeletonApi.IotHub.Services.Store;
using System.Text.Json;

namespace SkeletonApi.IotHub.Services
{
    public class PersistedConsumer : BackgroundService
    {
        private readonly IIoTHubEventHandler<MqttRawDataEncapsulation> _mqttStoreEventHandler;
        private readonly IotHubMachineHealthEventHandler _machineHealthEventHandler;
        private readonly IoTHubTaskEventHandler _taskEventHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly StatusMachineStore _StatusStore;
        private readonly TaskStore _taskStore;

        public PersistedConsumer(IIoTHubEventHandler<MqttRawDataEncapsulation> mqttStoreEventHandler,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            IotHubMachineHealthEventHandler machineHealthEventHandler,
            IoTHubTaskEventHandler taskEventHandler,
            StatusMachineStore machineStore,
            TaskStore taskStore
            )
        {
            _StatusStore = machineStore;
            _mqttStoreEventHandler = mqttStoreEventHandler;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _machineHealthEventHandler = machineHealthEventHandler;
            _taskStore = taskStore;
            _taskEventHandler = taskEventHandler;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _mqttStoreEventHandler.Subscribe(
                subscriberName: typeof(PersistedConsumer).Name,
                action: async (val) =>
            {
                if (val.mqttRawData != null && val.topics != null)
                {
                    switch (val.topics)
                    {
                        case string a when a.Contains("TEST"):
                            await PersistTaskAsync(val.mqttRawData);
                            //await PersistDeviceDataToDBAsync(val.mqttRawData);
                            break;

                        default:

                            break;
                    }
                }
            });

            return Task.CompletedTask;
        }

        private async Task PersistTaskAsync(MqttRawData value)
        {
            var mclist = _taskStore.GetAllTask();
            var map = _mapper.Map<IEnumerable<TaskModel>>(mclist);

            if (value.Values is not null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    //var machineHealthList = from vls in value.Values.Where(x => x.Vid.Contains("STATUS") && x.Quality == true)
                    //                        join ids in mclist on vls.Vid equals ids.Vid
                    //                        where vls.Vid == ids.Vid
                    //                        group new { vls, ids } by vls.Vid into g
                    //                        orderby g.Key descending
                    //                        select new MachineHealthModel
                    //                        {
                    //                            Id = g.Key,
                    //                            Name = g.Last().ids.Name,
                    //                            Value = int.Parse(g.Last().vls.Value.ToString()),
                    //                            Datetime = DateTimeOffset.FromUnixTimeMilliseconds(g.Last().vls.Time).DateTime
                    //                        };
                    //machineHealthList.OrderBy(x => x.Name).ToList();
                    _taskEventHandler.Dispatch(map);
                }
            }
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