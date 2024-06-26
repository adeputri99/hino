﻿using SkeletonApi.Domain.Common.Abstracts.Tsdb;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkeletonApi.Domain.Entities.Tsdb
{
    public class DeviceData : TsdbEntity
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("quality")]
        public bool Quality { get; set; }

        [Column("time")]
        public long Time { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; }
    }

    public record MqttRawValueEntity
    {
        public string Vid { get; init; }
        public virtual object Value { get; init; }
        public bool Quality { get; init; }

        public long Time { get; init; }

        public DateTime Datetime { get; init; }
    }
}