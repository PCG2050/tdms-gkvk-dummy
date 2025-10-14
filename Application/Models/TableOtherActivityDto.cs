using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TableOtherActivityDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UploadPath { get; set; }
        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
    }

    public class TableOtherActivityCreateDto
    {
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? UploadPath { get; set; }
    }

    public class TableOtherActivityUpdateDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UploadPath { get; set; }
        public string? FormStatus { get; set; }
        public string? FormStatusRemarks { get; set; }
    }
}
