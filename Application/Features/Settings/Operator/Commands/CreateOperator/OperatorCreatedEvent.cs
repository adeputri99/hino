using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.CreateOperator
{
    public class OperatorCreatedEvent : BaseEvent
    {
        public Operators Operator { get; set; }

        public OperatorCreatedEvent(Operators operators)
        {
            Operator = operators;
        }
    }
}