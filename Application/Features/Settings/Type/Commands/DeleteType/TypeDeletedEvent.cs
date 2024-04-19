using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.DeleteType
{
    public class TypeDeletedEvent : BaseEvent
    {
        public Types Types { get; set; }

        public TypeDeletedEvent(Types types)
        {
            Types = types;
        }
    }
}