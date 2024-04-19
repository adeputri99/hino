namespace SkeletonApi.Application.Features.Settings.Break
{
    public record BreakDto
    {
        public string? BreakeName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
    public sealed record CreateBreakeResponseDto : BreakDto { }
}