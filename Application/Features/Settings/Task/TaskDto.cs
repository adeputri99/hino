namespace SkeletonApi.Application.Features.Settings.Task
{
    public record TaskDto
    {
        public Guid OperatorId { get; set; }
        public string? TaskName { get; set; }
        public int TaskNo { get; set; }
        public int TaskDuration { get; set; }
    }
    public sealed record CreateTaskResponseDto : TaskDto { }
}