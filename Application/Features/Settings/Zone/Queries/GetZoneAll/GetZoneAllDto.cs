using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.Settings.Zone.Queries.GetZoneAll
{
    public class GetZoneAllDto : IMapFrom<Zones>
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
    }
}
