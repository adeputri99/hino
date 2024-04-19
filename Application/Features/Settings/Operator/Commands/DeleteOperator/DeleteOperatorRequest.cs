using MediatR;
using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.DeleteOperator
{
    public class DeleteOperatorRequest : IRequest<Result<Guid>>, IMapFrom<Operators>
    {
        public Guid Id { get; set; }
        public DeleteOperatorRequest(Guid id)
        {
            Id = id;
        }
        public DeleteOperatorRequest()
        {
            
        }
    }
}