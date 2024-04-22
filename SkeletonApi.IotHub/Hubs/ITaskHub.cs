using SkeletonApi.IotHub.Model;
using System.Threading.Channels;

namespace SkeletonApi.IotHub.Hubs
{
    public interface ITaskHub
    {
        public ChannelReader<IEnumerable<TaskModel>> RealtimeTask();
    }
}