using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.DeleteBreak
{
    public class BreakeDeletedEvent : BaseEvent
    {
        public SettingBreak SettingBreak { get; set; }
        public BreakeDeletedEvent(SettingBreak settingBreak)
        {
            SettingBreak = settingBreak;
        }
    }
}