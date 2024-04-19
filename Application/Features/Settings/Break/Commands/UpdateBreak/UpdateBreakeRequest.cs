using MediatR;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.UpdateBreak
{
    public class UpdateBreakeRequest : IRequest<Result<SettingBreak>>
    {
        public Guid Id { get; set; }
        public string? BreakName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}