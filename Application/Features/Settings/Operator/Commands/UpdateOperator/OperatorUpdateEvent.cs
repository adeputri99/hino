using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.UpdateOperator
{
    public class OperatorUpdateEvent : BaseEvent
    {
        public Operators Operators { get; set; }

        public OperatorUpdateEvent(Operators operators)
        {
            Operators = operators;
        }
    }
}