using SkeletonApi.Domain.Common.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkeletonApi.Domain.Entities
{
    public class Zones : BaseAuditableEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("task_duration")]
        public int TaskDuration { get; set; }

        [NotMapped]
        public Types Type { get; set; }

        public ICollection<Operators> Operators { get; set; } = new List<Operators>();

        public ICollection<Types> Types { get; set; } = new List<Types>();
    }
}