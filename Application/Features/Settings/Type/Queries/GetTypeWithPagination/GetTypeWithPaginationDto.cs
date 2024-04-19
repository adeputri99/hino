using SkeletonApi.Application.Common.Mappings;

namespace SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeWithPagination
{
    public class GetTypeWithPaginationDto : IMapFrom<GetTypeWithPaginationDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public int TaskDuration { get; set; }
    }
}