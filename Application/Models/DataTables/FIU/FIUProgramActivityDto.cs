using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.DataTables.FIU
{
    public class FIUProgramActivityDto
    {
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public int? FIUActivitiesId { get; set; }
        public int? Number { get; set; }
        public string? UploadMediaUrl { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }


        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        // Approval tracking
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

    }
}
