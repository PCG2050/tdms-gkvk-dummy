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

        [MapperIgnoreSource(nameof(PublicationCreateDto.KannadaNewsPaperIds))]
        [MapperIgnoreSource(nameof(PublicationCreateDto.EnglishNewsPaperIds))]
        public partial Publication MapToEntity(PublicationCreateDto dto);

        public partial PublisherDetails MapToEntity(PublisherDetailsCreateDto dto);
        public partial ExtensionLiterature MapToEntity(ExtensionLiteratureCreateDto dto);

        // Helper method to map newspaper IDs to junction entities after creation
        public static void MapNewspaperIdsToEntity(PublicationCreateDto dto, Publication entity)
        {
            if (dto.KannadaNewsPaperIds != null && dto.KannadaNewsPaperIds.Any())
            {
                foreach (var newspaperId in dto.KannadaNewsPaperIds)
                {
                    entity.PublicationKannadaNewsPapers.Add(new PublicationKannadaNewsPaper
                    {
                        KannadaNewsPaperId = newspaperId
                    });
                }
            }

            if (dto.EnglishNewsPaperIds != null && dto.EnglishNewsPaperIds.Any())
            {
                foreach (var newspaperId in dto.EnglishNewsPaperIds)
                {
                    entity.PublicationEnglishNewsPapers.Add(new PublicationEnglishNewsPaper
                    {
                        EnglishNewsPaperId = newspaperId
                    });
                }
            }
        }

        // ----------------------------
        // Helper methods for nested mappings (avoids compile-time deep path errors)
        // ----------------------------
        private string? GetUnitName(OrganizationUnitLocation? location)
            => location?.Unit?.Name;

        private string? GetDistrictName(OrganizationUnitLocation? location)
            => location?.District?.Name;

        private string? GetStateName(OrganizationUnitLocation? location)
            => location?.District?.State?.Name;

        // Helper methods for newspaper many-to-many mappings
        private List<int> GetKannadaNewsPaperIds(Publication publication)
            => publication.PublicationKannadaNewsPapers?.Select(p => p.KannadaNewsPaperId).ToList() ?? new List<int>();

        private List<string> GetKannadaNewsPaperNames(Publication publication)
            => publication.PublicationKannadaNewsPapers?.Select(p => p.KannadaNewsPaper?.NewsPaperName ?? string.Empty).ToList() ?? new List<string>();

        private List<int> GetEnglishNewsPaperIds(Publication publication)
            => publication.PublicationEnglishNewsPapers?.Select(p => p.EnglishNewsPaperId).ToList() ?? new List<int>();

        private List<string> GetEnglishNewsPaperNames(Publication publication)
            => publication.PublicationEnglishNewsPapers?.Select(p => p.EnglishNewsPaper?.NewsPaperName ?? string.Empty).ToList() ?? new List<string>();

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
        [MapProperty(nameof(Publication), nameof(PublicationDto.KannadaNewsPaperIds), Use = nameof(GetKannadaNewsPaperIds))]
        [MapProperty(nameof(Publication), nameof(PublicationDto.KannadaNewsPaperNames), Use = nameof(GetKannadaNewsPaperNames))]
        [MapProperty(nameof(Publication), nameof(PublicationDto.EnglishNewsPaperIds), Use = nameof(GetEnglishNewsPaperIds))]
        [MapProperty(nameof(Publication), nameof(PublicationDto.EnglishNewsPaperNames), Use = nameof(GetEnglishNewsPaperNames))]
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

            // Handle many-to-many newspaper relationships
            // Note: Null means no change, empty list clears all, populated list replaces all
            if (dto.KannadaNewsPaperIds != null)
            {
                entity.PublicationKannadaNewsPapers.Clear();
                foreach (var newspaperId in dto.KannadaNewsPaperIds)
                {
                    entity.PublicationKannadaNewsPapers.Add(new PublicationKannadaNewsPaper
                    {
                        PublicationId = entity.Id,
                        KannadaNewsPaperId = newspaperId
                    });
                }
            }

            if (dto.EnglishNewsPaperIds != null)
            {
                entity.PublicationEnglishNewsPapers.Clear();
                foreach (var newspaperId in dto.EnglishNewsPaperIds)
                {
                    entity.PublicationEnglishNewsPapers.Add(new PublicationEnglishNewsPaper
                    {
                        PublicationId = entity.Id,
                        EnglishNewsPaperId = newspaperId
                    });
                }
            }

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
