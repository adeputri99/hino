using MediatR;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.UpdateType
{
    public record UpdateTypeRequest : IRequest<Result<Types>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TaskDuration { get; set; }
        public Guid ZonaId { get; set; }
    }
}