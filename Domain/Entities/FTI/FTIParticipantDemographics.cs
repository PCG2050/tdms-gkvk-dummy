using Domain.Entities.ATIC;
using Domain.Entities.FIU;
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
    public class FTIParticipantDemographics : AuditableBaseEntity
    {
        // FK to TableKVKProgramDetails
        [Required]
        public int? FTIProgramDetailsId { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsId))]
        public FTIProgramDetails? ProgramDetails { get; set; }

        // Participant name/label
        [MaxLength(200)]
        public int? ParticipantId { get; set; }
        public ParticipantDealer? Participant { get; set; }

        // Male counts
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }

        // Male stayed in hostel
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }

        // Female counts
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }

        // Female stayed in hostel
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }

        // Total participants (store or compute)
        public int? Total { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
