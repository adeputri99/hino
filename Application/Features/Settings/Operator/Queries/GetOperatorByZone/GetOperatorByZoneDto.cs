using SkeletonApi.Application.Common.Mappings;

namespace SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorByZone
{
    public class GetOperatorByZoneDto : IMapFrom<GetOperatorByZoneDto>
    {
        public string Name { get; set; }
    }
}