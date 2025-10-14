using Domain.Entities.EEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.IBTVA
{
    public class IbtvaReport : AuditableBaseEntity
    {
        [Required]
        public int? IbtvaProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(IbtvaProgramDetailsId))]
        public IbtvaProgramDetails? ProgramDetails { get; set; }

        // Report details
        [MaxLength(150)]
        public string? ProgressReportReportingYear { get; set; }

        public DateTime? Date { get; set; }

        // File / media paths or URLs
        [MaxLength(500)]
        public string? UploadPhoto { get; set; }

        [MaxLength(500)]
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }

        [MaxLength(500)]
        public string? UploadVideo { get; set; }

        [MaxLength(1000)]
        public string? SignificantOutcome { get; set; }
    }
}
