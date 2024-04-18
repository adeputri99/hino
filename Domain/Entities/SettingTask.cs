using SkeletonApi.Domain.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Domain.Entities
{
    public class SettingTask : BaseAuditableEntity
    {
        [Column("task_name")]
        public string TaskName { get; set; }

        [Column("operator_id")]
        public Guid? OperatorId { get; set; }

        [Column("task_duration")]
        public int TaskDuration { get; set; }

        [Column("task_no")]
        public string TaskNo { get; set; }

        public Operator Operator { get; set; }
    }
}
