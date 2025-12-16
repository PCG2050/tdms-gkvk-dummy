
namespace Domain.Entities.EEU
{
    public class EeuFldResult : AuditableBaseEntity
    {
        public int EeuResultId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuResultId))]
        public EeuResult? EeuResult { get; set; }
        public int DetailsOfDemoId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(DetailsOfDemoId))]
        public FLDResult? DetailsOfDemo { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }

        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
