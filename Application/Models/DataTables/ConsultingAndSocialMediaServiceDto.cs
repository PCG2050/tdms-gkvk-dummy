using System;
using System.Collections.Generic;

namespace Application.Models.DataTables
{
    // Base DTO for display
    public class ConsultingServiceDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int? RelatedToId { get; set; }
        public string? RelatedToName { get; set; }
        public string? RelatedToDisciplineOther { get; set; }

        public int? ExtensionActivityId { get; set; }
        public string? ExtensionActivityName { get; set; }

        public int? ParticularsId { get; set; }
        public string? ParticularsName { get; set; }
        public string? ParticularsOthers { get; set; }

        public int? ModeOrOutreachId { get; set; }
        public string? ModeOutreachName { get; set; }

        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        public string? Publisher { get; set; }
        public string? WeblinkOrApplink { get; set; }
        public string? PublishedDocUrl { get; set; }

        public int PhoneCalls { get; set; }
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }

        public int FacebookPostCount { get; set; }
        public int NoOfSms { get; set; }
        public int NoOfWhatsappGroup { get; set; }
        public int NoOfWhatsappMessage { get; set; }
        public int CountOfAnsweredQueries { get; set; }
        public int FaceToFace { get; set; }
        public int GroupDiscussion { get; set; }
        public int NoOfEmailSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public int NoOfPressVisit { get; set; }
        public int NoOfPressMeet { get; set; }
        public int NoOfPressCoverage { get; set; }
        public int NoOfTweets { get; set; }

        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public string? CreatedByName { get; set; }

        // Child entities - populated when using GetWithDetails or with-children endpoints
        public List<ModeAndOutreachDto> ModeAndOutreaches { get; set; } = new();
    }

    // Complete DTO with all related entities
    // Note: This class is kept for backward compatibility, but now just inherits from base
    public class CompleteConsultingServiceDto : ConsultingServiceDto
    {
    }

    // Create DTO
    public class ConsultingServiceCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? RelatedToId { get; set; }
        public string? RelatedToDisciplineOther { get; set; }
        public int? ExtensionActivityId { get; set; }
        public int? ParticularsId { get; set; }
        public string? ParticularsOthers { get; set; }
        public int? ModeOrOutreachId { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        public string? Publisher { get; set; }
        public string? WeblinkOrApplink { get; set; }
        public string? PublishedDocUrl { get; set; }
        public int PhoneCalls { get; set; }
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }
        public int FacebookPostCount { get; set; }
        public int NoOfSms { get; set; }
        public int NoOfWhatsappGroup { get; set; }
        public int NoOfWhatsappMessage { get; set; }
        public int CountOfAnsweredQueries { get; set; }
        public int FaceToFace { get; set; }
        public int GroupDiscussion { get; set; }
        public int NoOfEmailSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public int NoOfPressVisit { get; set; }
        public int NoOfPressMeet { get; set; }
        public int NoOfPressCoverage { get; set; }
        public int NoOfTweets { get; set; }
    }

    // Update DTO (nullable fields for partial updates)
    public class ConsultingServiceUpdateDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? RelatedToId { get; set; }
        public string? RelatedToDisciplineOther { get; set; }
        public int? ExtensionActivityId { get; set; }
        public int? ParticularsId { get; set; }
        public string? ParticularsOthers { get; set; }
        public int? ModeOrOutreachId { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public string? Publisher { get; set; }
        public string? WeblinkOrApplink { get; set; }
        public string? PublishedDocUrl { get; set; }
        public int? PhoneCalls { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? FacebookPostCount { get; set; }
        public int? NoOfSms { get; set; }
        public int? NoOfWhatsappGroup { get; set; }
        public int? NoOfWhatsappMessage { get; set; }
        public int? CountOfAnsweredQueries { get; set; }
        public int? FaceToFace { get; set; }
        public int? GroupDiscussion { get; set; }
        public int? NoOfEmailSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
        public int? NoOfPressVisit { get; set; }
        public int? NoOfPressMeet { get; set; }
        public int? NoOfPressCoverage { get; set; }
        public int? NoOfTweets { get; set; }
    }

    // ModeAndOutreach DTOs
    public class ModeAndOutreachDto
    {
        public int Id { get; set; }
        public int ConsultingServiceId { get; set; }
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public bool Gender { get; set; }
    }

    public class ModeAndOutreachCreateDto
    {
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public bool Gender { get; set; }
    }

    // ============================
    // HYBRID DTOs FOR UPDATE OPERATIONS
    // ============================

    /// <summary>
    /// Hybrid DTO for ModeAndOutreach - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class ModeAndOutreachHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, > 0 = update existing
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public bool Gender { get; set; }
    }

    /// <summary>
    /// Composite DTO for creating ConsultingService with all child entities in a single transaction
    /// </summary>
    public class ConsultingServiceWithChildrenCreateDto
    {
        // Parent fields
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? RelatedToId { get; set; }
        public string? RelatedToDisciplineOther { get; set; }
        public int? ExtensionActivityId { get; set; }
        public int? ParticularsId { get; set; }
        public string? ParticularsOthers { get; set; }
        public int? ModeOrOutreachId { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        public string? Publisher { get; set; }
        public string? WeblinkOrApplink { get; set; }
        public string? PublishedDocUrl { get; set; }
        public int PhoneCalls { get; set; }
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }
        public int FacebookPostCount { get; set; }
        public int NoOfSms { get; set; }
        public int NoOfWhatsappGroup { get; set; }
        public int NoOfWhatsappMessage { get; set; }
        public int CountOfAnsweredQueries { get; set; }
        public int FaceToFace { get; set; }
        public int GroupDiscussion { get; set; }
        public int NoOfEmailSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public int NoOfPressVisit { get; set; }
        public int NoOfPressMeet { get; set; }
        public int NoOfPressCoverage { get; set; }
        public int NoOfTweets { get; set; }

        // Child collection (optional - can be null if UI doesn't have data yet)
        public List<ModeAndOutreachCreateDto>? ModeAndOutreaches { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating ConsultingService with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id > 0: UPDATE existing
    /// - Items WITHOUT Id (null or 0): CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// Perfect for "Save & Next" button with inline editing
    /// </summary>
    public class ConsultingServiceWithChildrenUpdateDto
    {
        // Parent fields
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? RelatedToId { get; set; }
        public string? RelatedToDisciplineOther { get; set; }
        public int? ExtensionActivityId { get; set; }
        public int? ParticularsId { get; set; }
        public string? ParticularsOthers { get; set; }
        public int? ModeOrOutreachId { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public string? Publisher { get; set; }
        public string? WeblinkOrApplink { get; set; }
        public string? PublishedDocUrl { get; set; }
        public int? PhoneCalls { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? FacebookPostCount { get; set; }
        public int? NoOfSms { get; set; }
        public int? NoOfWhatsappGroup { get; set; }
        public int? NoOfWhatsappMessage { get; set; }
        public int? CountOfAnsweredQueries { get; set; }
        public int? FaceToFace { get; set; }
        public int? GroupDiscussion { get; set; }
        public int? NoOfEmailSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
        public int? NoOfPressVisit { get; set; }
        public int? NoOfPressMeet { get; set; }
        public int? NoOfPressCoverage { get; set; }
        public int? NoOfTweets { get; set; }

        // Child collection - Hybrid Pattern
        // If item has Id > 0: update it
        // If item has no Id (null or 0): create it
        // If existing item not in array: delete it
        public List<ModeAndOutreachHybridDto>? ModeAndOutreaches { get; set; }
    }
}