using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Domain.Entities.Publications
    {
        public class Publication : ReportEntryBaseEntity
        {
            public string? Title { get; set; }
            public DateOnly? PublicationDate { get; set; }

            // Foreign Keys
            public int? CategoryId { get; set; }
            public int? ModeId { get; set; }
            public int? RegionId { get; set; }
            public int? SourceId { get; set; }
            public int? ExtensionLiteratureId { get; set; }

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

            // Navigation Properties
            public Category? Category { get; set; }
            public Mode? Mode { get; set; }
            public Region? Region { get; set; }
            public Source? Source { get; set; }
            public ExtensionLiterature? ExtensionLiterature { get; set; }
        }
    }

