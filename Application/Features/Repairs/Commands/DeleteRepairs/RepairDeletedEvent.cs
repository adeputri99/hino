using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.Repairs.Commands.DeleteRepairs
{
    public class RepairDeletedEvent : BaseEvent
    {
        public Repair Repair { get; set; }

        public RepairDeletedEvent (Repair repair)
        {
            Repair = repair;
        }
    }
}
