using Application.Models.DataTables;
using Domain.Entities;
using Domain.Entities.GenericTables.Service;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TblServicesDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string? CategoryName { get; set; }

        public int? CategoryId { get; set; }
        [MaxLength(200)]
        public string? OtherCategory { get; set; }

        public int? ThemeId { get; set; }

        public string? ThemeName { get; set; }
        [MaxLength(200)]
        public string? OtherTheme { get; set; }

        [MaxLength(250)]
        public string? CropPlantProductName { get; set; }

        [MaxLength(150)]
        public string? Variety { get; set; }

        public int? SourceOfFundId { get; set; }
        [MaxLength(200)]
        public string? OtherSourceOfFund { get; set; }

        [MaxLength(250)]
        public string? Component { get; set; }

        public int? QuantityUnitId { get; set; }

        public int Number { get; set; }

        public decimal AmountGenerated { get; set; }

        public decimal AmountReleased { get; set; }

        public DateOnly? Date { get; set; }

        [MaxLength(250)]
        public string? RentedTo { get; set; }

        [MaxLength(250)]
        public string? TitleOfActivityConducted { get; set; }


        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedById { get; set; }

        public string? QuantityUnitName { get; set; }
        public string? ApprovedByName { get; set; }

        public string? SourceOfFundName { get; set; }
        public string? CreatedByName { get; set; }

        // New fields
        public int VisitorId { get; set; }
        public string? VisitorName { get; set; }

        public int ParticipationTypeId { get; set; }
        public string? ParticipationTypeName { get; set; }

        // Child entities - populated when using GetWithDetails or with-children endpoints
        public List<TableHostelHybridDto> TableHostels { get; set; } = new();
        public List<RevolvingFundStatusHybridDto> RevolvingFundStatuses { get; set; } = new();
        public List<VisitorDetailHybridDto> VisitorDetails { get; set; } = new();
    }

    // Complete DTO with all related entities
    // Note: This class is kept for backward compatibility, but now just inherits from base
    public class CompleteTblServicesDto : TblServicesDto
    {
    }

    public class TblServiceCreateDto
    {
        [Required]
        public int UnitLocationId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public int? CategoryId { get; set; }
        public string? OtherCategory { get; set; }

        public int? ThemeId { get; set; }


        public string? OtherTheme { get; set; }

        public string? CropPlantProductName { get; set; }
        public string? Variety { get; set; }

        public int? SourceOfFundId { get; set; }
        public string? OtherSourceOfFund { get; set; }

        public string? Component { get; set; }
        public int? QuantityUnitId { get; set; }
        public int? Number { get; set; }

        public decimal? AmountGenerated { get; set; }
        public decimal? AmountReleased { get; set; }

        public DateOnly? Date { get; set; }
        public string? RentedTo { get; set; }
        public string? TitleOfActivityConducted { get; set; }

        // New visitor and participation fields
        public int VisitorId { get; set; }
        public int ParticipationTypeId { get; set; }

        // Optional inline children
        public List<TableHostelCreateDto>? TableHostels { get; set; }
        public List<RevolvingFundStatusCreateDto>? RevolvingFundStatuses { get; set; }
        public List<VisitorDetailCreateDto>? VisitorDetails { get; set; }
    }

    public class TblServiceUpdateDto
    {
        public int Id { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public int? CategoryId { get; set; }
        public string? OtherCategory { get; set; }

        public int? ThemeId { get; set; }
        public string? OtherTheme { get; set; }


        public string? CropPlantProductName { get; set; }
        public string? Variety { get; set; }

        public int? SourceOfFundId { get; set; }
        public string? OtherSourceOfFund { get; set; }

        public string? Component { get; set; }
        public int? QuantityUnitId { get; set; }

        public int? Number { get; set; }
        public decimal? AmountGenerated { get; set; }
        public decimal? AmountReleased { get; set; }

        public DateOnly? Date { get; set; }
        public string? RentedTo { get; set; }
        public string? TitleOfActivityConducted { get; set; }

        // New visitor and participation fields
        public int? VisitorId { get; set; }
        public int? ParticipationTypeId { get; set; }

        public string? FormStatus { get; set; }
        public string? FormStatusRemarks { get; set; }
    }



    public class TableHostelDto
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }

        public DateOnly? Date { get; set; }

        // Male counts
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }

        // Female counts
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }

        public int NumberOfDaysStayed { get; set; }


        public string? VillageOrTaluk { get; set; }


        public string? Purpose { get; set; }


        public decimal AmountGenerated { get; set; }

        public DateOnly SubmittedDate { get; set; }
    }


    public class TableHostelCreateDto
    {
        public DateOnly? Date { get; set; }
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }
        public int NumberOfDaysStayed { get; set; }
        public string? VillageOrTaluk { get; set; }
        public string? Purpose { get; set; }
        public decimal AmountGenerated { get; set; }
        public DateOnly? SubmittedDate { get; set; }
    }


    public class RevolvingFundStatusDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public decimal OpeningBalance { get; set; }
        public int Receipt { get; set; }
        public decimal Expenditure { get; set; }
        public double ClosingBalance { get; set; }
    }

    public class RevolvingFundStatusCreateDto
    {
        public decimal? OpeningBalance { get; set; }
        public int? Receipt { get; set; }
        public decimal? Expenditure { get; set; }
        public double? ClosingBalance { get; set; }
    }


    public class VisitorDetailDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int VisitorId { get; set; }
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public DateOnly? Date { get; set; }
        public string? Location { get; set; }
        public string? Purpose { get; set; }
        public string? PurposeOfVisit { get; set; }

        // Male counts
        public int Male_SC { get; set; } = 0;
        public int Male_ST { get; set; } = 0;
        public int Male_OBC { get; set; } = 0;
        public int Male_GEN { get; set; } = 0;

        // Female counts
        public int Female_SC { get; set; } = 0;
        public int Female_ST { get; set; } = 0;
        public int Female_OBC { get; set; } = 0;
        public int Female_GEN { get; set; } = 0;

        public int Male_Total { get; set; }
        public int Female_Total { get; set; }
        public int Total { get; set; }
    }

    public class VisitorDetailCreateDto
    {
        public int VisitorId { get; set; }
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public DateOnly? Date { get; set; }
        public string? Location { get; set; }
        public string? Purpose { get; set; }
        public string? PurposeOfVisit { get; set; }

        // Male counts
        public int Male_SC { get; set; } = 0;
        public int Male_ST { get; set; } = 0;
        public int Male_OBC { get; set; } = 0;
        public int Male_GEN { get; set; } = 0;

        // Female counts
        public int Female_SC { get; set; } = 0;
        public int Female_ST { get; set; } = 0;
        public int Female_OBC { get; set; } = 0;
        public int Female_GEN { get; set; } = 0;

        public int Male_Total { get; set; }
        public int Female_Total { get; set; }
        public int Total { get; set; }
    }

    // ============================
    // HYBRID DTOs FOR UPDATE OPERATIONS
    // ============================

    /// <summary>
    /// Hybrid DTO for TableHostel - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class TableHostelHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, > 0 = update existing
        public DateOnly? Date { get; set; }
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }
        public int NumberOfDaysStayed { get; set; }
        public string? VillageOrTaluk { get; set; }
        public string? Purpose { get; set; }
        public decimal AmountGenerated { get; set; }
        public DateOnly? SubmittedDate { get; set; }
    }

    /// <summary>
    /// Hybrid DTO for RevolvingFundStatus
    /// </summary>
    public class RevolvingFundStatusHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, > 0 = update existing
        public decimal? OpeningBalance { get; set; }
        public int? Receipt { get; set; }
        public decimal? Expenditure { get; set; }
        public double? ClosingBalance { get; set; }
    }

    /// <summary>
    /// Hybrid DTO for VisitorDetail
    /// </summary>
    public class VisitorDetailHybridDto
    {
        public int? Id { get; set; }  // null or 0 = create new, > 0 = update existing
        public int VisitorId { get; set; }
        public string? Name { get; set; }
        public int? MobileNo { get; set; }
        public DateOnly? Date { get; set; }
        public string? Location { get; set; }
        public string? Purpose { get; set; }
        public string? PurposeOfVisit { get; set; }
        public int Male_SC { get; set; } = 0;
        public int Male_ST { get; set; } = 0;
        public int Male_OBC { get; set; } = 0;
        public int Male_GEN { get; set; } = 0;
        public int Female_SC { get; set; } = 0;
        public int Female_ST { get; set; } = 0;
        public int Female_OBC { get; set; } = 0;
        public int Female_GEN { get; set; } = 0;
        public int Male_Total { get; set; }
        public int Female_Total { get; set; }
        public int Total { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating TblService with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id > 0: UPDATE existing
    /// - Items WITHOUT Id (null or 0): CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// Perfect for "Save & Next" button with inline editing
    /// </summary>
    public class TblServiceWithChildrenUpdateDto
    {
        // Parent fields
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public string? OtherCategory { get; set; }
        public int? ThemeId { get; set; }
        public string? OtherTheme { get; set; }
        public string? CropPlantProductName { get; set; }
        public string? Variety { get; set; }
        public int? SourceOfFundId { get; set; }
        public string? OtherSourceOfFund { get; set; }
        public string? Component { get; set; }
        public int? QuantityUnitId { get; set; }
        public int? Number { get; set; }
        public decimal? AmountGenerated { get; set; }
        public decimal? AmountReleased { get; set; }
        public DateOnly? Date { get; set; }
        public string? RentedTo { get; set; }
        public string? TitleOfActivityConducted { get; set; }

        // New visitor and participation fields
        public int? VisitorId { get; set; }
        public int? ParticipationTypeId { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id > 0: update it
        // If item has no Id (null or 0): create it
        // If existing item not in array: delete it
        public List<TableHostelHybridDto>? TableHostels { get; set; }
        public List<RevolvingFundStatusHybridDto>? RevolvingFundStatuses { get; set; }
        public List<VisitorDetailHybridDto>? VisitorDetails { get; set; }
    }

}
