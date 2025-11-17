
namespace Domain.Entities.FTI
{
    public class FtiParticipantDemographics : AuditableBaseEntity
    {
        // FK to TableKVKProgramDetails
        [Required]
<<<<<<< Updated upstream
        public int FTIProgramDetailsID { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails? ProgramDetails { get; set; }
=======
        public int? FtiProgramDetailsId { get; set; }

        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
>>>>>>> Stashed changes

        // Participant name/label
        [MaxLength(200)]
        public int? Participant { get; set; }

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
    }
}
