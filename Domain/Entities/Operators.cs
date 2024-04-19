using SkeletonApi.Domain.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Domain.Entities
{
    public class Operators : BaseAuditableEntity
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("zone_id")]
        public Guid? ZoneId { get; set; }

        public Zones Zone { get; set; }
        public ICollection<SettingTask> SettingTasks { get; set; } = new List<SettingTask>();
    }
}
