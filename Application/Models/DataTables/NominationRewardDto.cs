using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables
{
    // The comprehensive DTO containing all fields, foreign keys, 
    // derived navigation names, and all child collections.
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
        public int? ContributionId { get; set; }
        public int? ModeId { get; set; }
        public int? NominationCategoryId { get; set; }
       

        // === Other Fields ===
        public string? OtherRegion { get; set; }
        public string? AwardName { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? SpecificContributionTitle { get; set; }

        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstituteAddress { get; set; }

        public DateOnly? AwardApplicationDate { get; set; }
        public string? AwardFilePath { get; set; }
        public string? AwardEventTitle { get; set; }
        public DateOnly? AwardEventDate { get; set; }
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }

        public string? InstitutionBoardName { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionDesignation { get; set; }
        public string? InstitutionAddress { get; set; }

        public int? PositionId { get; set; } 

        public DateOnly? PositionFrom { get; set; }
        public DateOnly? PositionTo { get; set; }
        public int? DurationDays { get; set; }

        public DateOnly? NominationDate { get; set; }
        public string? NominationLetterPath { get; set; }

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

        // Child Collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
    }

    // --- Child DTOs ---

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
        // Note: PhoneNumber is defined as int? here. 
        // For actual phone numbers, a string? is usually preferred.
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

    // --- Summary/List DTO (assuming this serves a distinct purpose) ---

    public class CompleteNominationRewardDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public string? AwardName { get; set; }
        public string? SpecificContributionTitle { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }

        // Navigation DTOs
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? TypeName { get; set; }
        public string? RegionName { get; set; }

        // All child collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
    }

    // ===== HYBRID DTOs =====

    /// <summary>
    /// Hybrid create DTO - creates parent and all children in one request
    /// </summary>
    public class NominationRewardHybridCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // === Foreign Keys ===
        public int? TypeId { get; set; }
        public int? RegionId { get; set; }
        public int? ContributionId { get; set; }
        public int? ModeId { get; set; }
        public int? NominationCategoryId { get; set; }

        // === Other Fields ===
        public string? OtherRegion { get; set; }
        public string? AwardName { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? SpecificContributionTitle { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstituteAddress { get; set; }
        public DateOnly? AwardApplicationDate { get; set; }
        public string? AwardFilePath { get; set; }
        public string? AwardEventTitle { get; set; }
        public DateOnly? AwardEventDate { get; set; }
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
        public string? InstitutionBoardName { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionDesignation { get; set; }
        public string? InstitutionAddress { get; set; }
        public int? PositionId { get; set; }
        public DateOnly? PositionFrom { get; set; }
        public DateOnly? PositionTo { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? NominationDate { get; set; }
        public string? NominationLetterPath { get; set; }

        // === Child Collections ===
        public List<NominationRewardIFSFarmerCreateDto>? IFSFarmers { get; set; }
        public List<NominationRewardFarmerInnovationCreateDto>? FarmerInnovations { get; set; }
        public List<NominationRewardOrganicFarmerCreateDto>? OrganicFarmers { get; set; }
        public List<NominationRewardIFSEnterpreneurCreateDto>? IFSEntrepreneurs { get; set; }
        public List<NominationRewardEntrepreneurInnovationCreateDto>? EntrepreneurInnovations { get; set; }
        public List<NominationRewardOrganicEntrepreneurCreateDto>? OrganicEntrepreneurs { get; set; }
    }

    /// <summary>
    /// Hybrid update DTO - updates parent and manages all children (create/update/delete) in one request
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
        public int? ContributionId { get; set; }
        public int? ModeId { get; set; }
        public int? NominationCategoryId { get; set; }

        // === Other Fields ===
        public string? OtherRegion { get; set; }
        public string? AwardName { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? SpecificContributionTitle { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstituteAddress { get; set; }
        public DateOnly? AwardApplicationDate { get; set; }
        public string? AwardFilePath { get; set; }
        public string? AwardEventTitle { get; set; }
        public DateOnly? AwardEventDate { get; set; }
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
        public string? InstitutionBoardName { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionDesignation { get; set; }
        public string? InstitutionAddress { get; set; }
        public int? PositionId { get; set; }
        public DateOnly? PositionFrom { get; set; }
        public DateOnly? PositionTo { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? NominationDate { get; set; }
        public string? NominationLetterPath { get; set; }

        // === Child Collections ===
        public List<NominationRewardIFSFarmerUpdateDto>? IFSFarmers { get; set; }
        public List<NominationRewardFarmerInnovationUpdateDto>? FarmerInnovations { get; set; }
        public List<NominationRewardOrganicFarmerUpdateDto>? OrganicFarmers { get; set; }
        public List<NominationRewardIFSEnterpreneurUpdateDto>? IFSEntrepreneurs { get; set; }
        public List<NominationRewardEntrepreneurInnovationUpdateDto>? EntrepreneurInnovations { get; set; }
        public List<NominationRewardOrganicEntrepreneurUpdateDto>? OrganicEntrepreneurs { get; set; }
    }


}