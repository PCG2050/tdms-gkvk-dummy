using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.GenericTables.Service;
using Domain.Entities.Junction;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper
{
    [Mapper]
    public partial class TblServiceMapper
    {
        // ----------------------------
        // Basic Entity ⇆ DTO mappings
        // ----------------------------
        public partial TblServicesDto MapToDto(TblService entity);
        public partial CompleteTblServicesDto MapToCompleteDto(TblService entity);

        public partial TableHostelDto MapToDto(TableHostel entity);
        public partial RevolvingFundStatusDto MapToDto(RevolvingFundStatus entity);
        public partial VisitorDetailDto MapToDto(VisitorDetail entity);

        public partial TblService MapToEntity(TblServiceCreateDto dto);
        public partial TableHostel MapToEntity(TableHostelCreateDto dto);
        public partial RevolvingFundStatus MapToEntity(RevolvingFundStatusCreateDto dto);
        public partial VisitorDetail MapToEntity(VisitorDetailCreateDto dto);

        // ----------------------------
        // Navigation-based mappings
        // ----------------------------
        [MapProperty(nameof(TblService.UnitLocation), nameof(TblServicesDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(TblService.UnitLocation), nameof(TblServicesDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(TblService.UnitLocation), nameof(TblServicesDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(TblService.Organization.Name), nameof(TblServicesDto.OrganizationName))]
        [MapProperty(nameof(TblService.Category.Name), nameof(TblServicesDto.CategoryName))]
        [MapProperty(nameof(TblService.Theme.Name), nameof(TblServicesDto.ThemeName))]
        [MapProperty(nameof(TblService.SourceOfFund.Name), nameof(TblServicesDto.SourceOfFundName))]
        [MapProperty(nameof(TblService.QuantityUnit.Name), nameof(TblServicesDto.QuantityUnitName))]
        [MapProperty(nameof(TblService.ApprovedBy.FirstName), nameof(TblServicesDto.ApprovedByName))]
        [MapProperty(nameof(TblService.CreatedBy.FirstName), nameof(TblServicesDto.CreatedByName))]
        
        public partial TblServicesDto MapToDtoWithDetails(TblService entity);

        // ----------------------------
        // Helper methods for related data
        // ----------------------------
        private string? GetUnitName(OrganizationUnitLocation? location)
            => location?.Unit?.Name;

        private string? GetDistrictName(OrganizationUnitLocation? location)
            => location?.District?.Name;

        private string? GetStateName(OrganizationUnitLocation? location)
            => location?.District?.State?.Name;

        // ----------------------------
        // Manual mapping for updates
        // ----------------------------
        public void MapUpdateDtoToEntity(TblServiceUpdateDto dto, TblService entity)
        {
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate;
            if (dto.CategoryId.HasValue) entity.CategoryId = dto.CategoryId;
            if (dto.OtherCategory != null) entity.OtherCategory = dto.OtherCategory;
            if (dto.ThemeId.HasValue) entity.ThemeId = dto.ThemeId;
            if (dto.OtherTheme != null) entity.OtherTheme = dto.OtherTheme;
            if (dto.CropPlantProductName != null) entity.CropPlantProductName = dto.CropPlantProductName;
            if (dto.Variety != null) entity.Variety = dto.Variety;
            if (dto.SourceOfFundId.HasValue) entity.SourceOfFundId = dto.SourceOfFundId;
            if (dto.OtherSourceOfFund != null) entity.OtherSourceOfFund = dto.OtherSourceOfFund;
            if (dto.Component != null) entity.Component = dto.Component;
            if (dto.QuantityUnitId.HasValue) entity.QuantityUnitId = dto.QuantityUnitId;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
            if (dto.AmountGenerated.HasValue) entity.AmountGenerated = dto.AmountGenerated.Value;
            if (dto.AmountReleased.HasValue) entity.AmountReleased = dto.AmountReleased.Value;
            if (dto.Date.HasValue) entity.Date = dto.Date.Value;
            if (dto.RentedTo != null) entity.RentedTo = dto.RentedTo;
            if (dto.TitleOfActivityConducted != null) entity.TitleOfActivityConducted = dto.TitleOfActivityConducted;
            if (dto.FormStatus != null) entity.FormStatus = dto.FormStatus;
            if (dto.FormStatusRemarks != null) entity.FormStatusRemarks = dto.FormStatusRemarks;
        }
        // ----------------------------
        // Manual mapping for TableHostel updates
        // ----------------------------
        public void MapUpdateDtoToEntity(TableHostelCreateDto dto, TableHostel entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            entity.Male_SC = dto.Male_SC;
            entity.Male_ST = dto.Male_ST;
            entity.Male_OBC = dto.Male_OBC;
            entity.Male_GEN = dto.Male_GEN;
            entity.Female_SC = dto.Female_SC;
            entity.Female_ST = dto.Female_ST;
            entity.Female_OBC = dto.Female_OBC;
            entity.Female_GEN = dto.Female_GEN;
            entity.NumberOfDaysStayed = dto.NumberOfDaysStayed;
            entity.VillageOrTaluk = dto.VillageOrTaluk;
            entity.Purpose = dto.Purpose;
            entity.AmountGenerated = dto.AmountGenerated;
            if (dto.SubmittedDate.HasValue) entity.SubmittedDate = dto.SubmittedDate.Value;
        }
        // ----------------------------
        // Manual mapping for RevolvingFundStatus updates
        // ----------------------------
        public void MapUpdateDtoToEntity(RevolvingFundStatusCreateDto dto, RevolvingFundStatus entity)
        {
            entity.OpeningBalance = dto.OpeningBalance;
            entity.Receipt = dto.Receipt;
            entity.Expenditure = dto.Expenditure;
            entity.ClosingBalance = dto.ClosingBalance;
        }

        // ----------------------------
        // Manual mapping for VisitorDetail updates
        // ----------------------------
        public void MapUpdateDtoToEntity(VisitorDetailCreateDto dto, VisitorDetail entity)
        {
            entity.VisitorId = dto.VisitorId;
            entity.Name = dto.Name;
            entity.MobileNo = dto.MobileNo;
            entity.Date = dto.Date;
            entity.Location = dto.Location;
            entity.Purpose = dto.Purpose;
            entity.PurposeOfVisit = dto.PurposeOfVisit;
        }
    }
}
