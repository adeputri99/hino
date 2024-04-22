using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task Creates(IEnumerable<Notifications> mqttrawValues);
    }
}