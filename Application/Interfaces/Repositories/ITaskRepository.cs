using SkeletonApi.Application.DTOs.TaskRealtime;
using SkeletonApi.Application.Features.Settings.Task;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<bool> ValidateData(SettingTask task);

        Task<IEnumerable<SettingTask>> GetAllTasks();
    }
}