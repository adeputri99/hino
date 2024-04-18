using SkeletonApi.Domain.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Domain.Entities
{
    public class Zone : BaseAuditableEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("task_duration")]
        public int TaskDuration { get; set; }

        public ICollection<Operator> Operators { get; set; } = new List<Operator>();
    }
}
