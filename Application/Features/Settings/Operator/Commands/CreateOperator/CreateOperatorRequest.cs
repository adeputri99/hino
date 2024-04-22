using MediatR;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.CreateOperator
{
    public class CreateOperatorRequest : IRequest<Result<CreateOperatorResponseDto>>
    {
        public string Name { get; set; }
        public Guid ZoneId { get; set; }
    }
}