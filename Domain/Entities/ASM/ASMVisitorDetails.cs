using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ASM
{
    public class ASMVisitorDetails : AuditableBaseEntity
    {
        [Required]
        [StringLength(200)]
        public string InstituteName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }

        [Required]
        public DateTime SubmittedDate { get; set; }

        // Optional: store a status or action type if needed
        [StringLength(100)]
        public string? Actions { get; set; }

        [MaxLength(100)]
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }
    }
}
