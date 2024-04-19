using MediatR;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.CreateType
{
    public record CreateTypeRequest : IRequest<Result<CreateTypeResponseDto>>
    {
        public string? Name { get; set; }
        public Guid ZoneId { get; set; }    
        public int TaskDuration { get; set; }
    }
}