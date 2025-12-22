using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables.Service
{
    public class TableHostel : AuditableBaseEntity
    {
        

        // Foreign key to Service
        public int ServiceId { get; set; }
        [JsonIgnore]
        [ForeignKey("ServiceId")]
        public TblService? TblService { get; set; }

        public DateOnly? Date { get; set; }

        // Male counts
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }

        // Female counts
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }

        public int NumberOfDaysStayed { get; set; }

        [MaxLength(200)]
        public string? VillageOrTaluk { get; set; }

        [MaxLength(250)]
        public string? Purpose { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountGenerated { get; set; }

        public DateOnly SubmittedDate { get; set; }

    }
}
