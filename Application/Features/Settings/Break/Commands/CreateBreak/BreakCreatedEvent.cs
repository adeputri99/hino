using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.CreateBreak
{
    public class BreakCreatedEvent : BaseEvent
    {
        public SettingBreak Breaks { get; set; }

        public BreakCreatedEvent(SettingBreak breaks)
        {
            Breaks = breaks;
        }
    }
}