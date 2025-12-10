namespace Domain.Entities.KVK
{
    public class KvkAdvisoryServices : AuditableBaseEntity
    {


        [Required]
        public int? KvkProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramDetailsId))]
        public KvkProgramDetails? ProgramDetails { get; set; }

        // Advisory service metrics
        public int NoOfFacebookSMS { get; set; }
        public int NoOfSMSSentToRegisteredFarmers { get; set; }
        public int NoOfWhatsappGroups { get; set; }
        public int NoOfWhatsappSMS { get; set; }
        public int NoOfAnsweredWhatsappQueries { get; set; }
        public int NoOfPhoneCalls { get; set; }
        public int NoOfFaceToFaceDiscussions { get; set; }
        public int NoOfGroupDiscussions { get; set; }
        public int NoOfEmailsSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        public ICollection<KvkCriticalInputsDistributed>? CriticalInputsDistributed{ get; set; }

    }

    public class KvkCriticalInputsDistributed : AuditableBaseEntity
    {
        public int KvkAdvisoryServicesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(KvkAdvisoryServicesId))]
        public KvkAdvisoryServices? AdvisoryServices { get; set; }
        [MaxLength(200)]
        public string? InputName { get; set; }
        public int? QuantityDistributed { get; set; }
        public int? NoOfRecipients { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
