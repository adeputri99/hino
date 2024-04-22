using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Features.Repairs.Commands.DeleteRepairs
{
    public class RepairDeletedEvent : BaseEvent
    {
        public Repair Repair { get; set; }

        public RepairDeletedEvent(Repair repair)
        {
            Repair = repair;
        }
    }
}