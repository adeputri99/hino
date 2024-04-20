using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.Repairs.Queries.GetRepairWithPagination
{
    public class GetRepairWithPaginationDto : IMapFrom<Repair>
    {
        [JsonPropertyName("frame_number")]
        public string FrameNumber { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("entry")]
        public DateTime? Entry { get; set; }

        [JsonPropertyName("finish")]
        public DateTime? Finish { get; set; }
    }
}
