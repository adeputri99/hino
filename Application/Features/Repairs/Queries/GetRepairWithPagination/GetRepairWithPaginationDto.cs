using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using System.Text.Json.Serialization;

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