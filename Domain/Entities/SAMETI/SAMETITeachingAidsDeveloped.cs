namespace Domain.Entities.SAMETI
{
    public class SametiTeachingAidsDeveloped : AuditableBaseEntity
    {

        [Required]
        public int? SametiProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(SametiProgramContentAndResourcesId))]
        public SametiProgramContentAndResources? ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public int? TypeOfAidId { get; set; }
        [JsonIgnore]
        public TypeOfAid? TypeOfAid { get; set; }

        [MaxLength(200)]
        public string? OtherTypeOfAid { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
