namespace SkeletonApi.Application.Features.Settings.Operator
{
    public record OperatorDto
    {
        public string? Name { get; set; }
        public Guid ZoneId { get; set; }
    }
    public sealed record CreateOperatorResponseDto : OperatorDto { }
}