using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FTIRecommendation : AuditableBaseEntity
    {
        // FK to parent KVK program
        [Required]
        public int FTIProgramDetailsID { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails ProgramDetails { get; set; }

        // Recommendation fields
        [MaxLength(1000)]
        public string? ProblemsIdentified { get; set; }

        [MaxLength(1000)]
        public string? Recommendation { get; set; }

        [MaxLength(1000)]
        public string? ActionTaken { get; set; }

        [MaxLength(1000)]
        public string? SignificantAchievement { get; set; }

        [MaxLength(1000)]
        public string? SuccessStories { get; set; }

        [MaxLength(1000)]
        public string? ImpactOutcome { get; set; }
    }
}
