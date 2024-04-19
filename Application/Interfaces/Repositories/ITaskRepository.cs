using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<bool> ValidateData(SettingTask task);
    }
}