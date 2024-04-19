using MediatR;
using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.DeleteType
{
    public record DeleteTypeRequest : IRequest<Result<Guid>>, IMapFrom<Types>
    {
        public Guid Id { get; set; }
        public DeleteTypeRequest(Guid id)
        {
            Id = id;
        }
        public DeleteTypeRequest()
        {
        }
    }
}