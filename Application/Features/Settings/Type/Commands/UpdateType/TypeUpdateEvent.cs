using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.UpdateType
{
    public class TypeUpdateEvent : BaseEvent
    {
        public Types Types { get; set; }

        public TypeUpdateEvent(Types types)
        {
            Types = types;
        }
    }
}