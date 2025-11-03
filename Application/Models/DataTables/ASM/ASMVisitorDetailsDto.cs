using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables.ASM
{
    public class ASMVisitorDetailsDto
    {
        public string? InstituteName { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }
        public DateTime SubmittedDate { get; set; }

        public int UnitLocationId { get; set; }

        public int OrganizationId { get; set; }
        public string FormStatus { get; set; } = "Draft";

        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? CreatedByName { get; set; }

        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

    }
}
