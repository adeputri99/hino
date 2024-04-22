using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Repairs.Commands.UpdateRepairs
{
    public class RepairUpdateEvent : BaseEvent
    {
        public Repair Repair { get; set; }

        public RepairUpdateEvent(Repair repair)
        {
            Repair = repair;
        }
    }
}