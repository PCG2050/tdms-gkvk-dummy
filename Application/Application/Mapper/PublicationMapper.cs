namespace Application.Mapper
{
    [Mapper]
    public partial class PublicationMapper
    {
        // ----------------------------
        // Basic Entity ⇆ DTO Mappings
        // ----------------------------
        public partial PublicationDto MapToDto(Publication entity);
        public partial CompletePublicationDto MapToCompleteDto(Publication entity);
        public partial PublisherDetailsDto MapToDto(PublisherDetails entity);
        public partial ExtensionLiteratureDto MapToDto(ExtensionLiterature entity);

        public partial Publication MapToEntity(PublicationCreateDto dto);
        public partial PublisherDetails MapToEntity(PublisherDetailsCreateDto dto);
        public partial ExtensionLiterature MapToEntity(ExtensionLiteratureCreateDto dto);

        // ----------------------------
        // Helper methods for nested mappings (avoids compile-time deep path errors)
        // ----------------------------
        private string? GetUnitName(OrganizationUnitLocation? location)
            => location?.Unit?.Name;

        private string? GetDistrictName(OrganizationUnitLocation? location)
            => location?.District?.Name;

        private string? GetStateName(OrganizationUnitLocation? location)
            => location?.District?.State?.Name;

        // ----------------------------
        // Mapping with Navigation Details
        // ----------------------------
        [MapProperty(nameof(Publication.UnitLocation), nameof(PublicationDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(Publication.UnitLocation), nameof(PublicationDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(Publication.UnitLocation), nameof(PublicationDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(Publication.Organization.Name), nameof(PublicationDto.OrganizationName))]
        [MapProperty(nameof(Publication.Category.Name), nameof(PublicationDto.CategoryName))]
        [MapProperty(nameof(Publication.Mode.Name), nameof(PublicationDto.ModeName))]
        [MapProperty(nameof(Publication.Region.Name), nameof(PublicationDto.RegionName))]
        [MapProperty(nameof(Publication.Source.Name), nameof(PublicationDto.SourceName))]
        public partial PublicationDto MapToDtoWithDetails(Publication entity);

        // ----------------------------
        // Manual Mapping for Update DTO
        // ----------------------------
        public static void MapUpdateDtoToEntity(PublicationUpdateDto dto, Publication entity)
        {
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PublicationDate.HasValue) entity.PublicationDate = dto.PublicationDate;
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate.Value;
            if (dto.CategoryId.HasValue) entity.CategoryId = dto.CategoryId;
            if (dto.OtherPublication != null) entity.OtherPublication = dto.OtherPublication;
            if (dto.ModeId.HasValue) entity.ModeId = dto.ModeId;
            if (dto.ModePublication != null) entity.ModePublication = dto.ModePublication;
            if (dto.RegionId.HasValue) entity.RegionId = dto.RegionId;
            if (dto.SourceId.HasValue) entity.SourceId = dto.SourceId;
            if (dto.MJASFormat != null) entity.MJASFormat = dto.MJASFormat;
            if (dto.PublicationTitle != null) entity.PublicationTitle = dto.PublicationTitle;
            if (dto.PublicationJournalTitle != null) entity.PublicationJournalTitle = dto.PublicationJournalTitle;
            if (dto.PublicationYear.HasValue) entity.PublicationYear = dto.PublicationYear;
            if (dto.PublicationVolume.HasValue) entity.PublicationVolume = dto.PublicationVolume;
            if (dto.PublicationIssue != null) entity.PublicationIssue = dto.PublicationIssue;
            if (dto.PublicationPagesFrom.HasValue) entity.PublicationPagesFrom = dto.PublicationPagesFrom;
            if (dto.PublicationPagesTo.HasValue) entity.PublicationPagesTo = dto.PublicationPagesTo;
            if (dto.PublicationISBN != null) entity.PublicationISBN = dto.PublicationISBN;
            if (dto.PublicationUniNumber.HasValue) entity.PublicationUniNumber = dto.PublicationUniNumber;
            if (dto.PublicationNAAS.HasValue) entity.PublicationNAAS = dto.PublicationNAAS;
            if (dto.PublicationImpact.HasValue) entity.PublicationImpact = dto.PublicationImpact;
            if (dto.PublicationWebLink != null) entity.PublicationWebLink = dto.PublicationWebLink;
            if (dto.PublicationCover != null) entity.PublicationCover = dto.PublicationCover;
            if (dto.PublicationWhole != null) entity.PublicationWhole = dto.PublicationWhole;
            if (dto.Funds.HasValue) entity.Funds = dto.Funds;
            if (dto.SponsorDetails != null) entity.SponsorDetails = dto.SponsorDetails;
            if (dto.PermissionLetterDate.HasValue) entity.PermissionLetterDate = dto.PermissionLetterDate;
            if (dto.PermissionLetterUrl != null) entity.PermissionLetterUrl = dto.PermissionLetterUrl;
        }
    }
}
