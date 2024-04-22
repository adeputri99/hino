using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Repairs.Commands.CreateRepairs
{
    public class RepairCreatedEvent : BaseEvent
    {
        public Repair Repair { get; set; }

        public RepairCreatedEvent(Repair repair)
        {
            Repair = repair;
        }
    }
}