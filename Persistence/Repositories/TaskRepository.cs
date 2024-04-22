using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.DTOs.TaskRealtime;
using SkeletonApi.Application.Features.Settings.Task;
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

        public async Task<IEnumerable<SettingTask>> GetAllTasks()
        {
            var data = _settingTaskRepository.Entities.Include(o => o.Operator).GroupBy(o => o.OperatorId).Select(o => new SettingTask
            {
                Zona = "zona",
                Type = "type",
                TaskDuration = 1000,
                Operators = o.Select(k => new SettingOperators
                {
                    OperatorNumber = k.Operator.Name,
                    TaskNumber = k.TaskNo,
                    TaskName = k.TaskName,
                }).ToList(),
            }).ToList();
            return data;
        }

        public async Task<bool> ValidateData(SettingTask task)
        {
            var x = _settingTaskRepository.FindByCondition(o => task.TaskName.ToLower() == o.TaskName.ToLower() && task.TaskNo.ToLower() == o.TaskNo.ToLower()).Count();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}