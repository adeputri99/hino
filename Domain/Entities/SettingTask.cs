using SkeletonApi.Domain.Common.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? TaskNo { get; set; }

        public Operators Operator { get; set; }
    }
}