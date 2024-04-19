using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Break.Queries.GetBreakeWithPagination
{
    public class GetBreakeWithPaginationDto : IMapFrom<SettingBreak>
    {
        public string? BreakeName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}