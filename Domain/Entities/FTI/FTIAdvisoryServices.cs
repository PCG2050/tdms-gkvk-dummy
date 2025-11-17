namespace Domain.Entities.FTI
{
    public class FtiAdvisoryServices : AuditableBaseEntity
    {
        [Required]
<<<<<<< Updated upstream
        public int? FTIProgramDetailsID { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails? ProgramDetails { get; set; }
=======
        public int? FtiProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
>>>>>>> Stashed changes

        // Advisory service metrics
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }
}
