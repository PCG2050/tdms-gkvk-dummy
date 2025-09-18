using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
   
        public class PublicationDto
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public DateOnly? PublicationDate { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public string? Attachments { get; set; }
            public int UnitLocationId { get; set; }
            public string UnitLocationName { get; set; } = string.Empty;

            // Master Data
            public int? CategoryId { get; set; }
            public string? CategoryName { get; set; }
            public int? ModeId { get; set; }
            public string? ModeName { get; set; }
            public int? RegionId { get; set; }
            public string? RegionName { get; set; }
            public int? SourceId { get; set; }
            public string? SourceName { get; set; }

            // Publication Details
            public string? MJASFormat { get; set; }
            public string? PublicationTitle { get; set; }
            public string? PublicationJournalTitle { get; set; }
            public DateOnly? PublicationYear { get; set; }
            public int? PublicationVolume { get; set; }
            public string? PublicationIssue { get; set; }
            public int? PublicationPages { get; set; }
            public string? PublicationISBN { get; set; }
            public int? PublicationUniNumber { get; set; }
            public int? PublicationNAAS { get; set; }
            public int? PublicationImpact { get; set; }
            public string? PublicationWebLink { get; set; }
            public string? PublicationCover { get; set; }
            public string? PublicationWhole { get; set; }

            // Funding & Permission
            public int? Funds { get; set; }
            public string? SponsorDetails { get; set; }
            public DateOnly? PermissionLetterDate { get; set; }
            public string? PermissionLetterUrl { get; set; }

            // Publisher Details
            public string? PublisherBrochure { get; set; }
            public string? PublisherName { get; set; }
            public string? PublisherInstitutionName { get; set; }
            public string? PublisherAddress { get; set; }

            // Extension Literature (if linked)
            public int? ExtensionLiteratureId { get; set; }
            public string? ExtensionLiteratureName { get; set; }
        }

        public class PublicationCreateDto
        {
            public string? Title { get; set; }
            public DateOnly? PublicationDate { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public string? Attachments { get; set; }
            public int UnitLocationId { get; set; }

            // Master Data IDs
            public int? CategoryId { get; set; }
            public int? ModeId { get; set; }
            public int? RegionId { get; set; }
            public int? SourceId { get; set; }
            public int? ExtensionLiteratureId { get; set; }

            // All other publication fields...
            public string? MJASFormat { get; set; }
            public string? PublicationTitle { get; set; }
            public string? PublicationJournalTitle { get; set; }
            public DateOnly? PublicationYear { get; set; }
            public int? PublicationVolume { get; set; }
            public string? PublicationIssue { get; set; }
            public int? PublicationPages { get; set; }
            public string? PublicationISBN { get; set; }
            public int? PublicationUniNumber { get; set; }
            public int? PublicationNAAS { get; set; }
            public int? PublicationImpact { get; set; }
            public string? PublicationWebLink { get; set; }
            public string? PublicationCover { get; set; }
            public string? PublicationWhole { get; set; }
            public int? Funds { get; set; }
            public string? SponsorDetails { get; set; }
            public DateOnly? PermissionLetterDate { get; set; }
            public string? PermissionLetterUrl { get; set; }
            public string? PublisherBrochure { get; set; }
            public string? PublisherName { get; set; }
            public string? PublisherInstitutionName { get; set; }
            public string? PublisherAddress { get; set; }
        }

        public class PublicationUpdateDto
        {
            public string? Title { get; set; }
            public DateOnly? PublicationDate { get; set; }
            public DateOnly? StartDate { get; set; }
            public DateOnly? EndDate { get; set; }
            public string? Attachments { get; set; }

            // Optional updates for all fields
            public int? CategoryId { get; set; }
            public int? ModeId { get; set; }
            public int? RegionId { get; set; }
            public int? SourceId { get; set; }
            public int? ExtensionLiteratureId { get; set; }

            // Other fields as needed...
            public string? MJASFormat { get; set; }
            public string? PublicationTitle { get; set; }
        
        }

        // Master Data DTOs
        public class CategoryDto
        {
            public int Id { get; set; }
            public string CategoryName { get; set; } = string.Empty;
        }

        public class SourceDto
        {
            public int Id { get; set; }
            public string SourceName { get; set; } = string.Empty;
        }

        public class ModeDto
        {
            public int Id { get; set; }
            public string ModeName { get; set; } = string.Empty;
        }

        public class RegionDto
        {
            public int Id { get; set; }
            public string RegionName { get; set; } = string.Empty;
        }

        public class ExtensionLiteratureDto
        {
            public int Id { get; set; }
            public DateOnly? ExtensionDate { get; set; }
            public string? PublicationName { get; set; }
            public int NumberSold { get; set; }
            public int TotalFarmers { get; set; }
        }
    
}
