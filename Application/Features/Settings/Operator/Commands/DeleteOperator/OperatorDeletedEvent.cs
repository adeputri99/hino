using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.DeleteOperator
{
    public class OperatorDeletedEvent : BaseEvent
    {
        public Operators Operators { get; set; }

        public OperatorDeletedEvent(Operators operators)
        {
            Operators = operators;
        }
    }
}