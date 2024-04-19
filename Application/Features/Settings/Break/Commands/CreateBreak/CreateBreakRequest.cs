using MediatR;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.CreateBreak
{
    public class CreateBreakRequest : IRequest<Result<CreateBreakeResponseDto>>
    {
        public string? BreakeName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}