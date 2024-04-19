using System.ComponentModel.DataAnnotations;

namespace SkeletonApi.Application.Features.Settings.Type
{
    public record TypeDto
    {
        public string? Name { get; set; }
        public int TaskDuration { get; set; }
    }
    public sealed record CreateTypeResponseDto : TypeDto { }
}