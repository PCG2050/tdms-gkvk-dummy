using Domain.Entities.MasterData;

namespace Domain.Entities.EEU
{
    public class EeuResult : AuditableBaseEntity
    {
        public int EeuProgramDetailsId { get; set; }

        [ForeignKey(nameof(EeuProgramDetailsId))]
        public EeuProgramDetails? ProgramDetails { get; set; }
        public ICollection<EeuFldResult>? FldResults { get; set; }
        public ICollection<EeuOftResult>? OftResults { get; set; }
        public string? UploadExcelUrl { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
