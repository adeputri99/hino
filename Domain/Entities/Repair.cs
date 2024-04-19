using SkeletonApi.Domain.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Domain.Entities
{
    public class Repair : BaseAuditableEntity
    {

        [Column("frame_number")]
        public string FrameNumber { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("entry")]
        public DateTime? Entry { get; set; }

        [Column("finish")]
        public DateTime? Finish { get; set; }

    }
}
