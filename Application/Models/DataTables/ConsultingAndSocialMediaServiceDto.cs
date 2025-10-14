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
    }

    // Complete DTO with all related entities
    public class CompleteConsultingServiceDto : ConsultingServiceDto
    {
        public List<ModeAndOutreachDto> ModeAndOutreaches { get; set; } = new();
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
}