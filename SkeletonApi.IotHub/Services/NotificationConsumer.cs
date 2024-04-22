﻿using AutoMapper;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.IotHub.Model;
using SkeletonApi.IotHub.Services.Handler;
using SkeletonApi.IotHub.Services.Store;

namespace SkeletonApi.IotHub.Services
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly IIoTHubEventHandler<MqttRawDataEncapsulation> _mqttStoreEventHandler;
        private readonly IotHubNotificationEventHandler _notificationEventHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly NotificationStore _notificationStore;

        public NotificationConsumer(IIoTHubEventHandler<MqttRawDataEncapsulation> mqttStoreEventHandler,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            IotHubNotificationEventHandler notificationEventHandler,
            NotificationStore notificationStore

            )
        {
            _notificationStore = notificationStore;
            _mqttStoreEventHandler = mqttStoreEventHandler;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _notificationEventHandler = notificationEventHandler;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _mqttStoreEventHandler.Subscribe(
                subscriberName: typeof(NotificationConsumer).Name,
                action: async (val) =>
                {
                    if (val.mqttRawData != null && val.topics != null)
                    {
                        await PersistNotificationToDBAsync(val.mqttRawData);
                    }
                });

            return Task.CompletedTask;
        }

        private async Task PersistNotificationToDBAsync(MqttRawData value)
        {
            if (value.Values is not null)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var notificationList = from vls in value.Values.Where(x => x.Quality == true)
                                               group new { vls } by vls.Vid into g
                                               orderby g.Key descending
                                               select new NotificationModel
                                               {
                                                   MachineName = g.Last().vls.Vid,
                                                   Message = g.Last().vls.Value.ToString(),
                                                   Datetime = DateTimeOffset.FromUnixTimeMilliseconds(g.Last().vls.Time).DateTime
                                               };

                        var notification = _notificationStore.GetAllSetting().Where(x => (x.SubjectName == notificationList.FirstOrDefault().MachineName
                        && Convert.ToDecimal(notificationList.FirstOrDefault().Message) > x.Maximum && notificationList.FirstOrDefault().Message != "0") ||
                        (x.SubjectName == notificationList.FirstOrDefault().MachineName && Convert.ToDecimal(notificationList.FirstOrDefault().Message) < x.Minimum
                        && notificationList.FirstOrDefault().Message != "0"));

                        if (notification.Count() != 0)
                        {
                            var dataNotification = notificationList.Select(g => new NotificationModel
                            {
                                MachineName = g.MachineName,
                                Message = $"ABNORMAL VALUE, CURRENT VALUE IS {g.Message} IN {g.MachineName}",
                                Datetime = g.Datetime,
                                Status = false
                            });
                            _notificationEventHandler.Dispatch(dataNotification);

                            var scoped = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                            var mqttRawValueEntities = _mapper.Map<IEnumerable<Notifications>>(dataNotification);
                            scoped.Creates(mqttRawValueEntities);
                        }
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