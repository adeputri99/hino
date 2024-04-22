using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Zone.Queries.GetZoneAll
{
    public class GetZoneAllDto : IMapFrom<Zones>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}