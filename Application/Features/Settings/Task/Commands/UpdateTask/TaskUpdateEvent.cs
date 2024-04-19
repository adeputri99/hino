using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.UpdateTask
{
    public class TaskUpdateEvent : BaseEvent
    {
        public SettingTask SettingTask { get; set; }

        public TaskUpdateEvent(SettingTask settingTask)
        {
            SettingTask = settingTask;
        }
    }
}