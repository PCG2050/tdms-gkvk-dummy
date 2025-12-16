using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables
{
    // The comprehensive DTO containing all fields and all 10 child collections
    public class NominationRewardDto
    {
        public int Id { get; set; }
        [Required]
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // === Foreign Keys ===
        public int? TypeId { get; set; }
        public int? RegionId { get; set; }

        // === Other Fields ===
        public string? OtherType { get; set; }
        public string? OtherRegion { get; set; }

        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        // Navigation DTOs (Derived/Display fields)
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? TypeName { get; set; }
        public string? RegionName { get; set; }

        // All 10 Child Collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
        public List<AchievementDto> Achievements { get; set; } = new();
        public List<AwardRecognitionDto> AwardRecognitions { get; set; } = new();
        public List<UniversitySanctionLetterPaperPosterDto> UniversitySanctionLetterPaperPosters { get; set; } = new();
        public List<AwardPhotoDto> AwardPhotos { get; set; } = new();
    }

    // --- Original 6 Child DTOs ---

    public class NominationRewardIFSFarmerDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardIFSEnterpreneurDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardFarmerInnovationDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardEntrepreneurInnovationDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicFarmerDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicEntrepreneurDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    // --- New 4 Child DTOs ---

    public class AchievementDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AchievementDetail { get; set; }
    }

    public class AwardRecognitionDto
    {
        public int Id { get; set; }
        public string? AwardName { get; set; }
        public int? ContributionId { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionAddress { get; set; }
    }

    public class UniversitySanctionLetterPaperPosterDto
    {
        public int Id { get; set; }
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
    }

    public class AwardPhotoDto
    {
        public int Id { get; set; }
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
    }

    // --- Create DTOs (for hybrid create - no Id field) ---

    public class NominationRewardIFSFarmerCreateDto
    {
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardIFSEnterpreneurCreateDto
    {
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardFarmerInnovationCreateDto
    {
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardEntrepreneurInnovationCreateDto
    {
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicFarmerCreateDto
    {
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicEntrepreneurCreateDto
    {
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class AchievementCreateDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AchievementDetail { get; set; }
    }

    public class AwardRecognitionCreateDto
    {
        public string? AwardName { get; set; }
        public int? ContributionId { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionAddress { get; set; }
    }

    public class UniversitySanctionLetterPaperPosterCreateDto
    {
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
    }

    public class AwardPhotoCreateDto
    {
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
    }

    // --- Update DTOs (for hybrid update - with nullable Id) ---

    public class NominationRewardIFSFarmerUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardIFSEnterpreneurUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
    }

    public class NominationRewardFarmerInnovationUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardEntrepreneurInnovationUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicFarmerUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class NominationRewardOrganicEntrepreneurUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
        public int? PhoneNumber { get; set; }
    }

    public class AchievementUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AchievementDetail { get; set; }
    }

    public class AwardRecognitionUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? AwardName { get; set; }
        public int? ContributionId { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionAddress { get; set; }
    }

    public class UniversitySanctionLetterPaperPosterUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
    }

    public class AwardPhotoUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
    }

    // --- Summary/List DTO ---

    public class CompleteNominationRewardDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }

        // Navigation DTOs
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? TypeName { get; set; }
        public string? RegionName { get; set; }

        // All 10 child collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
        public List<AchievementDto> Achievements { get; set; } = new();
        public List<AwardRecognitionDto> AwardRecognitions { get; set; } = new();
        public List<UniversitySanctionLetterPaperPosterDto> UniversitySanctionLetterPaperPosters { get; set; } = new();
        public List<AwardPhotoDto> AwardPhotos { get; set; } = new();
    }

    // ===== HYBRID DTOs =====

    /// <summary>
    /// Hybrid create DTO - creates parent and all 10 children in one request
    /// </summary>
    public class NominationRewardHybridCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // === Foreign Keys ===
        public int? TypeId { get; set; }
        public int? RegionId { get; set; }

        // === Other Fields ===
        public string? OtherType { get; set; }
        public string? OtherRegion { get; set; }

        // === All 10 Child Collections ===
        public List<NominationRewardIFSFarmerCreateDto>? IFSFarmers { get; set; }
        public List<NominationRewardFarmerInnovationCreateDto>? FarmerInnovations { get; set; }
        public List<NominationRewardOrganicFarmerCreateDto>? OrganicFarmers { get; set; }
        public List<NominationRewardIFSEnterpreneurCreateDto>? IFSEntrepreneurs { get; set; }
        public List<NominationRewardEntrepreneurInnovationCreateDto>? EntrepreneurInnovations { get; set; }
        public List<NominationRewardOrganicEntrepreneurCreateDto>? OrganicEntrepreneurs { get; set; }
        public List<AchievementCreateDto>? Achievements { get; set; }
        public List<AwardRecognitionCreateDto>? AwardRecognitions { get; set; }
        public List<UniversitySanctionLetterPaperPosterCreateDto>? UniversitySanctionLetterPaperPosters { get; set; }
        public List<AwardPhotoCreateDto>? AwardPhotos { get; set; }
    }

    /// <summary>
    /// Hybrid update DTO - updates parent and manages all 10 children (create/update/delete) in one request
    /// Child items with Id = 0 or null will be created
    /// Child items with Id > 0 will be updated
    /// Child items not in the lists will be deleted (cascade)
    /// </summary>
    public class NominationRewardHybridUpdateDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // === Foreign Keys ===
        public int? TypeId { get; set; }
        public int? RegionId { get; set; }

        // === Other Fields ===
        public string? OtherType { get; set; }
        public string? OtherRegion { get; set; }

        // === All 10 Child Collections ===
        public List<NominationRewardIFSFarmerUpdateDto>? IFSFarmers { get; set; }
        public List<NominationRewardFarmerInnovationUpdateDto>? FarmerInnovations { get; set; }
        public List<NominationRewardOrganicFarmerUpdateDto>? OrganicFarmers { get; set; }
        public List<NominationRewardIFSEnterpreneurUpdateDto>? IFSEntrepreneurs { get; set; }
        public List<NominationRewardEntrepreneurInnovationUpdateDto>? EntrepreneurInnovations { get; set; }
        public List<NominationRewardOrganicEntrepreneurUpdateDto>? OrganicEntrepreneurs { get; set; }
        public List<AchievementUpdateDto>? Achievements { get; set; }
        public List<AwardRecognitionUpdateDto>? AwardRecognitions { get; set; }
        public List<UniversitySanctionLetterPaperPosterUpdateDto>? UniversitySanctionLetterPaperPosters { get; set; }
        public List<AwardPhotoUpdateDto>? AwardPhotos { get; set; }
    }
}
