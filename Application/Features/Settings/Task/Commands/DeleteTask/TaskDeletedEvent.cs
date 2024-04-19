using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.DeleteTask
{
    public class TaskDeletedEvent : BaseEvent
    {
        public SettingTask SettingTask { get; set; }

        public TaskDeletedEvent(SettingTask settingTask)
        {
            SettingTask = settingTask;
        }
    }
}