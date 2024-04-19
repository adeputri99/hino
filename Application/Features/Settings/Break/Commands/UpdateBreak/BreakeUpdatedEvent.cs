using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.UpdateBreak
{
    public class BreakeUpdatedEvent : BaseEvent
    {
        public SettingBreak SettingBreak { get; set; }

        public BreakeUpdatedEvent(SettingBreak settingBreak)
        {
            SettingBreak = settingBreak;
        }
    }
}