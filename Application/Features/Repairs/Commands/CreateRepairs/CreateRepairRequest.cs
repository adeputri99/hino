using MediatR;
using SkeletonApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.Repairs.Commands.CreateRepairs
{
    public sealed record CreateRepairRequest : IRequest<Result<CreateRepairResponseDto>>
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
