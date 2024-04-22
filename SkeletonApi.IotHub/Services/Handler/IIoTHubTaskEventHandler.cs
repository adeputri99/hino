using SkeletonApi.IotHub.Model;

namespace SkeletonApi.IotHub.Services.Handler
{
    public interface IIoTHubTaskEventHandler
    {
        public IObservable<IEnumerable<TaskModel>> Observe();
    }
}