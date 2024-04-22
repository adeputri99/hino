using SkeletonApi.Application.Common.Mappings;

namespace SkeletonApi.Application.Features.Settings.Task.Queries.GetTaskWithPagination
{
    public class GetTaskWithPaginationDto : IMapFrom<GetTaskWithPaginationDto>
    {
        public Guid Id { get; set; }
        public string? Zone { get; set; }
        public string? TypeName { get; set; }
        public string? OperatorNumber { get; set; }
        public string? TaskNumber { get; set; }
        public string? TaskName { get; set; }
        public int DurationTask { get; set; }
    }
}