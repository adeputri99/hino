using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

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
