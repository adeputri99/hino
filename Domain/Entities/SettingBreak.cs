using SkeletonApi.Domain.Common.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkeletonApi.Domain.Entities
{
    public class SettingBreak : BaseAuditableEntity
    {
        [Column("breake_name")]
        public string? BreakeName { get; set; }

        [Column("start_time")]
        public string? StartTime { get; set; }

        [Column("end_time")]
        public string? EndTime { get; set; }
    }
}