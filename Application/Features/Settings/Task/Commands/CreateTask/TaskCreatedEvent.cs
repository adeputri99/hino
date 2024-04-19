using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.CreateTask
{
    public class TaskCreatedEvent : BaseEvent
    {
        public SettingTask SettingTask { get; set; }

        public TaskCreatedEvent(SettingTask settingTask)
        {
            SettingTask = settingTask;
        }
    }
}