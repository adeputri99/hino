using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.CreateType
{
    public class TypeCreatedEvent : BaseEvent
    {
        public Types Types { get; set; }

        public TypeCreatedEvent(Types types)
        {
            Types = types;
        }
    }
}