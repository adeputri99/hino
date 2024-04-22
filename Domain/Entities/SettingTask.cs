using SkeletonApi.Domain.Common.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkeletonApi.Domain.Entities
{
    public class SettingTask : BaseAuditableEntity
    {
        [NotMapped]
        public string Zona { get; set; }

        [NotMapped]
        public string Type { get; set; }

        [NotMapped]
        public int TaskDurations { get; set; }

        [NotMapped]
        public List<SettingOperators> Operators { get; set; }

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

    [NotMapped]
    public class SettingOperators
    {
        public string OperatorNumber { get; set; }
        public string TaskNumber { get; set; }
        public string TaskName { get; set; }
    }
}