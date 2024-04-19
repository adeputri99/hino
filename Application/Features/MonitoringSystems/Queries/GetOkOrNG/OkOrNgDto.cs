﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG
{
    public class OkOrNgDto
    {
        [JsonIgnore]
        public string Vid { get; set; }

        [JsonPropertyName("data")]
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }
    }
}
