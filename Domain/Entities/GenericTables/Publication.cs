using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables
{

    public class Publication : ReportEntryBaseEntity
    {

        public int? CategoryId { get; set; }
        [JsonIgnore]
        public PublicationCategory? Category { get; set; }
        public string? OtherPublication { get; set; }
        public string? Title { get; set; }

        public int? ModeId { get; set; }
        [JsonIgnore]
        public Mode? Mode { get; set; }

        public string? ModePublication { get; set; }

        public int? RegionId { get; set; }
        [JsonIgnore]
        public Region? Region { get; set; }
        public string? MJASFormat { get; set; }
        public string? PublicationTitle { get; set; }
        public string? PublicationJournalTitle { get; set; }

        public int? PublicationYear { get; set; }
        public int? PublicationVolume { get; set; }
        public string? PublicationIssue { get; set; }
        public int? PublicationPagesFrom { get; set; }
        public int? PublicationPagesTo { get; set; }
        public string? PublicationISBN { get; set; }
        public int? PublicationUniNumber { get; set; }
        public int? PublicationNAAS { get; set; }
        public int? PublicationImpact { get; set; }
        public string? PublicationWebLink { get; set; }

        public string? PublicationCover { get; set; }
        public string? PublicationWhole { get; set; }

        public int? SourceId { get; set; }
        [JsonIgnore]
        public ParticipatedSource? Source { get; set; }

        public int? Funds { get; set; }
        public string? SponsorDetails { get; set; }
        public DateOnly? PermissionLetterDate { get; set; }
        public string? PermissionLetterUrl { get; set; }
        public DateOnly? PublicationDate { get; set; }
       
        public ICollection<ExtensionLiterature> ExtensionLiteratures { get; set; } = [];
    }
    public class PublisherDetails : AuditableBaseEntity
    {
 
        public int? PublicationId { get; set; }
        [JsonIgnore]
        public Publication? Publication { get; set; }
        public string? PublisherBrochure { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherInstitutionName { get; set; }
        public string? PublisherAddress { get; set; }
     
    }
    public class ExtensionLiterature : AuditableBaseEntity
    {
 
        public int? PublicationId { get; set; }
        [JsonIgnore]
        public Publication? Publication { get; set; } 
        public DateOnly? Date { get; set; }
        public string? AmountPerCopy { get; set; }
        public string? NumberOfCopies { get; set; }
        public string? TotalAmount { get; set; }
    }
}