using SkeletonApi.Application.Common.Mappings;

namespace SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorWithPagination
{
    public class GetOperatorWithPaginationDto : IMapFrom<GetOperatorWithPaginationDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
    }
}