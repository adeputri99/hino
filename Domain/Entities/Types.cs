using SkeletonApi.Domain.Common.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkeletonApi.Domain.Entities
{
    public class Types : BaseAuditableEntity
    {
        [Column("name")]
        public string TypeName { get; set; }

        [Column("task_duration")]
        public int TaskDuration { get; set; }

        [Column("zone_id")]
        public Guid? ZoneId { get; set; }

        public Zones Zone { get; set; }
    }
}