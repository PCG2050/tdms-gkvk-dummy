
using Application.Interface;
using Domain.Entities.MasterData;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.DataTables.KVK
{
    // ============================
    // PROGRAM DETAILS DTOs
    // ============================
    public class KvkProgramDetailsDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public string? ProgramTypeName { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeName { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaName { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? ModeName { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public string? SourceOfFundName { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }
        public decimal? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? SourceName { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
        public string? Attachements { get; set; }

        // New fields for collaborator and program details
        public int? CollaboratorId { get; set; }
        public string? CollaboratorName { get; set; }
        public string? CollaboratorOther { get; set; }
        public int? CollaborativeProgramOptionId { get; set; }
        public string? CollaborativeProgramOptionName { get; set; }
        public string? CollaborativeProgramOptionOther { get; set; }
        public string? T01 { get; set; }
        public string? T02 { get; set; }
        public string? T03 { get; set; }
        public string? T04 { get; set; }
        public string? T05 { get; set; }
        public string? StageOfCrop { get; set; }
        public int? NoOfDemos { get; set; }
        public int? NoOfTrails { get; set; }
        public int? NoOfChecks { get; set; }
        public int? NoOfVisits { get; set; }
        public int? ParticipatedAsId { get; set; }
        public string? ParticipatedAsName { get; set; }
        public string? ParticipantFileUpload { get; set; }

        // Status tracking
        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

        // Audit
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? CreatedByName { get; set; }

        // Navigation
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }
    }

    public class KvkProgramCreateDto
    {
        [Required]
        public int UnitLocationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }

        // Collaborator fields
        public int? CollaboratorId { get; set; }
        public string? CollaboratorOther { get; set; }
        public int? CollaborativeProgramOptionId { get; set; }
        public string? CollaborativeProgramOptionOther { get; set; }

        //newly added 
        public string? T01 { get; set; }
        public string? T02 { get; set; }
        public string? T03 { get; set; }
        public string? T04 { get; set; }
        public string? T05 { get; set; }

        public string? StageOfCrop { get; set; }

        public int? NoOfDemos { get; set; }

        public int? NoOfTrails { get; set; }

        public int? NoOfChecks { get; set; }

        public int? NoOfVisits { get; set; }


        public int? ParticipatedAsId { get; set; }

        public string? ParticipantFileUpload { get; set; }
        public decimal? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
        public string? Attachements { get; set; }

     
    }

    public class KvkProgramUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }
        public int? Area { get; set; }

        // Collaborator fields
        public int? CollaboratorId { get; set; }
        public string? CollaboratorOther { get; set; }
        public int? CollaborativeProgramOptionId { get; set; }
        public string? CollaborativeProgramOptionOther { get; set; }

        public string? T01 { get; set; }

        public string? T02 { get; set; }

        public string? T03 { get; set; }

        public string? T04 { get; set; }

        public string? T05 { get; set; }

        public string? StageOfCrop { get; set; }

        public int? NoOfDemos { get; set; }

        public int? NoOfTrails { get; set; }

        public int? NoOfChecks { get; set; }

        public int? NoOfVisits { get; set; }


        public int? ParticipatedAsId { get; set; }

        public string? ParticipantFileUpload { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
        public string? Attachements { get; set; }
    }

    public class KvkProgramCompleteDto
    {
        public KvkProgramDetailsDto ProgramDetails { get; set; }
        public List<KvkParticipantDemographicsDto>? Demographics { get; set; }
        public List<KvkProgramContentDto>? ProgramContent { get; set; }
        public KvkAdvisoryServicesDto? AdvisoryServices { get; set; }
        public KvkResultDto? Results { get; set; } // For FLD/OFT categories
        public KvkReportDto? Report { get; set; } // For other categories
        public KvkRecommendationDto? Recommendation { get; set; }
    }

    /// <summary>
    /// Lightweight DTO for list/search results
    /// </summary>
    public class KvkProgramListItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? CategoryName { get; set; }
        public string? TypeName { get; set; }
        public string? Location { get; set; }
        public string FormStatus { get; set; } = "Draft";
        public string? CreatedByName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? UnitName { get; set; }
    }

    // ============================
    // DEMOGRAPHICS DTOs
    // ============================
    public class KvkParticipantDemographicsDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public int? ParticipantId { get; set; }
        public string? ParticipantName { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    public class KvkParticipantDemographicsCreateDto
    {
        public int? ParticipantId { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    public class KvkParticipantDemographicsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? ParticipantId { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    // ============================
    // PROGRAM CONTENT DTOs
    // ============================
    public class KvkProgramContentDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<KvkResourcePersonDto>? ResourcePersons { get; set; }
        public List<KvkTopicsCoveredDto>? TopicsCovered { get; set; }
        public List<KvkTeachingAidsDto>? TeachingAids { get; set; }
        public List<KvkFieldVisitDto>? FieldVisits { get; set; }
        public List<KvkFieldDayDto>? FieldDays { get; set; }
        public List<KvkFarmerScientistInteractionDto>? FarmerScientistInteractions { get; set; }
    }

    public class KvkProgramContentCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    /// <summary>
    /// Composite DTO for creating KvkProgramContentAndResources along with all child entities in a single transaction
    /// </summary>
    public class KvkProgramContentWithChildrenCreateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<KvkResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<KvkTopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<KvkTeachingAidsCreateDto>? TeachingAids { get; set; }
        public List<KvkFieldVisitCreateDto>? FieldVisits { get; set; }
        public List<KvkFieldDayCreateDto>? FieldDays { get; set; }
        public List<KvkFarmerScientistInteractionCreateDto>? FarmerScientistInteractions { get; set; }
    }

    /// <summary>
    /// Hybrid item for Resource Person - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class KvkResourcePersonHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    /// <summary>
    /// Hybrid item for Topics Covered
    /// </summary>
    public class KvkTopicsCoveredHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Teaching Aids
    /// </summary>
    public class KvkTeachingAidsHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating KvkProgramContentAndResources with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id: CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class KvkProgramContentWithChildrenUpdateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id: update it
        // If item has no Id: create it
        // If existing item not in array: delete it
        public List<KvkResourcePersonHybridDto>? ResourcePersons { get; set; }
        public List<KvkTopicsCoveredHybridDto>? TopicsCovered { get; set; }
        public List<KvkTeachingAidsHybridDto>? TeachingAids { get; set; }
        public List<KvkFieldVisitHybridDto>? FieldVisits { get; set; }
        public List<KvkFieldDayHybridDto>? FieldDays { get; set; }
        public List<KvkFarmerScientistInteractionHybridDto>? FarmerScientistInteractions { get; set; }
    }

    // Resource Person DTOs
    public class KvkResourcePersonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class KvkResourcePersonCreateDto
    {
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class KvkResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    // Topics Covered DTOs
    public class KvkTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkTopicsCoveredCreateDto
    {
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    // Teaching Aids DTOs
    public class KvkTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? TypeOfAidName { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class KvkTeachingAidsCreateDto
    {
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class KvkTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ============================
    // FIELD VISIT DTOs
    // ============================
    public class KvkFieldVisitDto
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerVisitedName { get; set; }
        public string? Purpose { get; set; }
        public int? NoOfFieldsCovered { get; set; }
        public int? NoOfFarmerCovered { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkFieldVisitCreateDto
    {
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerVisitedName { get; set; }
        public string? Purpose { get; set; }
        public int? NoOfFieldsCovered { get; set; }
        public int? NoOfFarmerCovered { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkFieldVisitUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerVisitedName { get; set; }
        public string? Purpose { get; set; }
        public int? NoOfFieldsCovered { get; set; }
        public int? NoOfFarmerCovered { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Field Visits
    /// </summary>
    public class KvkFieldVisitHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerVisitedName { get; set; }
        public string? Purpose { get; set; }
        public int? NoOfFieldsCovered { get; set; }
        public int? NoOfFarmerCovered { get; set; }
        public string? PhotoUpload { get; set; }
    }

    // ============================
    // FIELD DAY DTOs
    // ============================
    public class KvkFieldDayDto
    {
        public int Id { get; set; }
        // Add KvkFieldDay fields based on your entity
    }

    public class KvkFieldDayCreateDto
    {
        // Add KvkFieldDay fields based on your entity
    }

    public class KvkFieldDayUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        // Add KvkFieldDay fields based on your entity
    }

    /// <summary>
    /// Hybrid item for Field Days
    /// </summary>
    public class KvkFieldDayHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        // Add KvkFieldDay fields based on your entity
    }

    // ============================
    // FARMER SCIENTIST INTERACTION DTOs
    // ============================
    public class KvkFarmerScientistInteractionDto
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkFarmerScientistInteractionCreateDto
    {
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkFarmerScientistInteractionUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Farmer Scientist Interactions
    /// </summary>
    public class KvkFarmerScientistInteractionHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
    }

    // ============================
    // ADVISORY SERVICES DTOs
    // ============================
    public class KvkAdvisoryServicesDto
    {
        public int Id { get; set; }
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
        public List<KvkCriticalInputsDistributedDto>? CriticalInputsDistributed { get; set; }
    }

    public class KvkAdvisoryServicesCreateDto
    {
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }

    public class KvkAdvisoryServicesUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }

    /// <summary>
    /// Hybrid create DTO - creates advisory services with all critical inputs in one request
    /// </summary>
    public class KvkAdvisoryServicesHybridCreateDto
    {
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }

        public List<KvkCriticalInputsDistributedCreateDto>? CriticalInputsDistributed { get; set; }
    }

    /// <summary>
    /// Hybrid update DTO - updates advisory services and manages all critical inputs (create/update/delete) in one request
    /// </summary>
    public class KvkAdvisoryServicesHybridUpdateDto
    {
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }

        public List<KvkCriticalInputsDistributedHybridDto>? CriticalInputsDistributed { get; set; }
    }

    // ============================
    // CRITICAL INPUTS DISTRIBUTED DTOs
    // ============================
    public class KvkCriticalInputsDistributedDto
    {
        public int Id { get; set; }
        public string? InputName { get; set; }
        public int? QuantityDistributed { get; set; }
        public int? NoOfRecipients { get; set; }
    }

    public class KvkCriticalInputsDistributedCreateDto
    {
        public string? InputName { get; set; }
        public int? QuantityDistributed { get; set; }
        public int? NoOfRecipients { get; set; }
    }

    public class KvkCriticalInputsDistributedUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? InputName { get; set; }
        public int? QuantityDistributed { get; set; }
        public int? NoOfRecipients { get; set; }
    }

    /// <summary>
    /// Hybrid item for Critical Inputs Distributed
    /// </summary>
    public class KvkCriticalInputsDistributedHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public string? InputName { get; set; }
        public int? QuantityDistributed { get; set; }
        public int? NoOfRecipients { get; set; }
    }

    // ============================
    // RESULT DTOs (FLD/OFT)
    // ============================
    public class KvkResultDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public string? UploadExcelUrl { get; set; }
        public List<KvkFldResultDto>? FldResults { get; set; }
        public List<KvkOftResultDto>? OftResults { get; set; }
    }

    public class KvkFldResultDto
    {
        public int Id { get; set; }
        public int KvkResultId { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? DetailsOfDemoName { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkFldResultCreateDto
    {
        public int DetailsOfDemoId { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkFldResultUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int DetailsOfDemoId { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultDto
    {
        public int Id { get; set; }
        public int KvkResultId { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? DetailsOfDemoName { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultCreateDto
    {
        public int DetailsOfDemoId { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    /// <summary>
    /// Composite DTO for creating KvkResult along with all child entities (FldResults and OftResults) in a single transaction
    /// </summary>
    public class KvkResultWithChildrenCreateDto
    {
        // Parent fields
        public string? UploadExcelUrl { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<KvkFldResultCreateDto>? FldResults { get; set; }
        public List<KvkOftResultCreateDto>? OftResults { get; set; }
    }

    /// <summary>
    /// Hybrid item for FLD Result - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class KvkFldResultHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, has value = update existing
        public int DetailsOfDemoId { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    /// <summary>
    /// Hybrid item for OFT Result
    /// </summary>
    public class KvkOftResultHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, has value = update existing
        public int DetailsOfDemoId { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating KvkResult with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id (null or 0): CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class KvkResultWithChildrenUpdateDto
    {
        // Parent fields
        public string? UploadExcelUrl { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id > 0: update it
        // If item has no Id (null or 0): create it
        // If existing item not in array: delete it
        public List<KvkFldResultHybridDto>? FldResults { get; set; }
        public List<KvkOftResultHybridDto>? OftResults { get; set; }
    }

    // ============================
    // REPORT DTOs (Non-FLD/OFT)
    // ============================
    public class KvkReportDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }

        public string? ReportingYear { get; set; }
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }

    public class KvkReportCreateDto
    {
        public string? ProgressReportReportingYear { get; set; }

        //public string? ReportingYear { get; set; }
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }

    public class KvkReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        //public string? ReportingYear { get; set; }
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }

    // ============================
    // RECOMMENDATION DTOs
    // ============================
    public class KvkRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }

        public string? UploadVideoUrl { get; set; }
    }

    public class KvkRecommendationCreateDto
    {
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }

        public string? UploadVideoUrl { get; set; }
    }

    public class KvkRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
        public string? UploadVideoUrl { get; set; }
    }
}