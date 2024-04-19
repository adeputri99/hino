using SkeletonApi.Application.Common.Mappings;

namespace SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeByZone
{
    public class GetTypeByZoneDto : IMapFrom<GetTypeByZoneDto>
    {
        public string? Name { get; set; }
    }
}