using SkeletonApi.Domain.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Domain.Entities
{
    public class Types : BaseAuditableEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("task_duration")]
        public int TaskDuration { get; set; }
        [Column("zone_id")]
        public Guid? ZoneId { get; set; }
        public Zones Zone { get; set; }
    }
}
