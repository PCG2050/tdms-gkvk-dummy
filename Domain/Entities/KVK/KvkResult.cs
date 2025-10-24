using Domain.Entities.MasterData;

namespace Domain.Entities.KVK
{
    public class KvkResult : AuditableBaseEntity
    {
        public int KvkProgramDetailsId { get; set; }

        [ForeignKey(nameof(KvkProgramDetailsId))]
        public KvkProgramDetails? ProgramDetails { get; set; }
        public ICollection<KvkFldResult>? FldResults { get; set; }
        public ICollection<KvkOftResult>? OftResults { get; set; }
        public string? UploadExcelUrl { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
