using Microsoft.AspNetCore.SignalR;
using RxSignalrStreams.Extensions;
using SkeletonApi.IotHub.Model;
using SkeletonApi.IotHub.Services.Handler;
using System.Threading.Channels;

namespace SkeletonApi.IotHub.Hubs
{
    public class TaskHub : Hub<ITaskHub>
    {
        private readonly IotHubMachineHealthEventHandler _machineHealthEventHandler;

        public TaskHub(IotHubMachineHealthEventHandler machineHealthEventHandler)
        {
            _machineHealthEventHandler = machineHealthEventHandler;
        }

        public ChannelReader<IEnumerable<MachineHealthModel>> RealtimeTask()
        {
            return _machineHealthEventHandler.Observe().ToNewestValueStream(Context.ConnectionAborted);
        }
    }
}