using MediatR;
using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.Repairs.Commands.DeleteRepairs
{
    public record DeleteRepairRequest : IRequest<Result<Guid>>, IMapFrom<Repair>
    {
        public Guid Id { get; set; }

        public DeleteRepairRequest(Guid id)
        {
            Id = id;
        }

        public DeleteRepairRequest()
        {
        }
    }
}
