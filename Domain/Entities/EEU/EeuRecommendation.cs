using Domain.Entities.DEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuRecommendation : AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramDetailsId { get; set; }

        [ForeignKey(nameof(EeuProgramDetailsId))]
        public EeuProgramDetails ProgramDetails { get; set; }

        // Recommendation fields
        [MaxLength(1000)]
        public string ProblemsIdentified { get; set; }

        [MaxLength(1000)]
        public string Recommendation { get; set; }

        [MaxLength(1000)]
        public string ActionTaken { get; set; }

        [MaxLength(1000)]
        public string SignificantAchievement { get; set; }

        [MaxLength(1000)]
        public string SuccessStories { get; set; }

        [MaxLength(1000)]
        public string ImpactOutcome { get; set; }
    }
}
