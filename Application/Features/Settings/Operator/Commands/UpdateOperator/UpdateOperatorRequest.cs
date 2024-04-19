using MediatR;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.UpdateOperator
{
    public record UpdateOperatorRequest : IRequest<Result<Operators>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ZonaId { get; set; }
    }
}