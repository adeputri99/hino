using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IGenericRepository<SettingTask> _settingTaskRepository;
        public TaskRepository(IGenericRepository<SettingTask> settingRepository)
        {
            _settingTaskRepository = settingRepository;
        }

        public async Task<bool> ValidateData(SettingTask task)
        {
            var x =  _settingTaskRepository.FindByCondition(o => task.TaskName.ToLower() == o.TaskName.ToLower() && task.TaskNo.ToLower() == o.TaskNo.ToLower()).Count();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}