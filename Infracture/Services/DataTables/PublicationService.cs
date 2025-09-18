using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables
{
    using Application.Interface;
    using Application.Interface.Repository;
    using Application.Interface.Repository.DataTables;
    using Application.Interface.Services;
    using Application.Interface.Services.Common;
    using Application.Interface.Services.DataTables;
    using Application.Models;
    using Application.Models.DataTables.Application.Models.Publications;

    using Domain.Entities.Publications;

    namespace Infrastructure.Services
    {

        public class PublicationService : IPublicationService
        {
            private readonly IPublicationRepository _publicationRepository;
            private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IEntityPermissionService _entityPermissionService;

            public PublicationService(
                IPublicationRepository publicationRepository,
                ITrainerAssignmentRepository trainerAssignmentRepository,
                ICurrentUserService currentUserService,
                IEntityPermissionService entityPermissionService)
            {
                _publicationRepository = publicationRepository;
                _trainerAssignmentRepository = trainerAssignmentRepository;
                _currentUserService = currentUserService;
                _entityPermissionService = entityPermissionService;
            }

            public async Task<ServiceResult<PublicationDto>> CreateAsync(PublicationCreateDto createDto)
            {
                // Validate user can access this unit location
                if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                    return ServiceResult<PublicationDto>.Failure("Access denied to unit location", ServiceErrorStatus.FORBIDDEN);

                var publication = MapCreateDtoToEntity(createDto);
                publication.CreatedById = _currentUserService.UserId;
                publication.CreatedAt = DateTimeOffset.UtcNow;
                publication.OrganizationId = _currentUserService.OrganizationId;

                var savedPublication = await _publicationRepository.CreateAsync(publication);
                var dto = MapEntityToDto(savedPublication);

                return ServiceResult<PublicationDto>.Success(dto);
            }

            public async Task<ServiceResult<PublicationDto>> UpdateAsync(int id, PublicationUpdateDto updateDto)
            {
                var publication = await _publicationRepository.GetWithDetailsAsync(id);
                if (publication == null)
                    return ServiceResult<PublicationDto>.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

                if (!await _entityPermissionService.CanModify(publication))
                    return ServiceResult<PublicationDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

                MapUpdateDtoToEntity(updateDto, publication);
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;

                await _publicationRepository.UpdateAsync(publication);
                var dto = MapEntityToDto(publication);

                return ServiceResult<PublicationDto>.Success(dto);
            }

            public async Task<ServiceResult> DeleteAsync(int id)
            {
                var publication = await _publicationRepository.GetByIdAsync(id);
                if (publication == null)
                    return ServiceResult.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

                if (!await _entityPermissionService.CanModify(publication))
                    return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

                await _publicationRepository.DeleteAsync(id);
                return ServiceResult.Success();
            }

            public async Task<ServiceResult<PublicationDto>> GetByIdAsync(int id)
            {
                var publication = await _publicationRepository.GetWithDetailsAsync(id);
                if (publication == null)
                    return ServiceResult<PublicationDto>.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

                if (!await _entityPermissionService.CanModify(publication))
                    return ServiceResult<PublicationDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

                var dto = MapEntityToDto(publication);
                return ServiceResult<PublicationDto>.Success(dto);
            }

            public async Task<ServiceResult<PaginatedResult<PublicationDto>>> GetPaginatedAsync(
                int? unitLocationId = null,
                int pageNumber = 1,
                int pageSize = 10,
                DateOnly? startDate = null,
                DateOnly? endDate = null)
            {
                // Get user's accessible unit location IDs
                List<int> accessibleUnitLocationIds;
                if (unitLocationId.HasValue)
                {
                    if (!await CanUserAccessUnitLocationAsync(unitLocationId.Value))
                        return ServiceResult<PaginatedResult<PublicationDto>>.Failure("Access denied to unit location", ServiceErrorStatus.FORBIDDEN);

                    accessibleUnitLocationIds = new List<int> { unitLocationId.Value };
                }
                else
                {
                    var assignments = await _trainerAssignmentRepository.GetByTrainerIdAsync(_currentUserService.UserId);
                    accessibleUnitLocationIds = assignments.Select(a => a.UnitLocationId).ToList();
                }

                var paginatedPublications = await _publicationRepository.GetPaginatedAsync(
                    accessibleUnitLocationIds,
                    pageNumber,
                    pageSize,
                    startDate,
                    endDate);

                var dtos = paginatedPublications.Items.Select(MapEntityToDto).ToList();

                var result = new PaginatedResult<PublicationDto>
                {
                    Items = dtos,
                    TotalItems = paginatedPublications.TotalItems,
                    PageNumber = paginatedPublications.PageNumber,
                    PageSize = paginatedPublications.PageSize,
                };

                return ServiceResult<PaginatedResult<PublicationDto>>.Success(result);
            }

            // Master data methods
            public async Task<ServiceResult<List<CategoryDto>>> GetCategoriesAsync()
            {
                var categories = await _publicationRepository.GetCategoriesAsync();
                var dtos = categories.Select(c => new CategoryDto { Id = c.Id, CategoryName = c.CategoryName }).ToList();
                return ServiceResult<List<CategoryDto>>.Success(dtos);
            }

            public async Task<ServiceResult<List<SourceDto>>> GetSourcesAsync()
            {
                var sources = await _publicationRepository.GetSourcesAsync();
                var dtos = sources.Select(s => new SourceDto { Id = s.Id, SourceName = s.SourceName }).ToList();
                return ServiceResult<List<SourceDto>>.Success(dtos);
            }

            public async Task<ServiceResult<List<ModeDto>>> GetModesAsync()
            {
                var modes = await _publicationRepository.GetModesAsync();
                var dtos = modes.Select(m => new ModeDto { Id = m.Id, ModeName = m.ModeName }).ToList();
                return ServiceResult<List<ModeDto>>.Success(dtos);
            }

            public async Task<ServiceResult<List<RegionDto>>> GetRegionsAsync()
            {
                var regions = await _publicationRepository.GetRegionsAsync();
                var dtos = regions.Select(r => new RegionDto { Id = r.Id, RegionName = r.RegionName }).ToList();
                return ServiceResult<List<RegionDto>>.Success(dtos);
            }

            public async Task<ServiceResult<List<ExtensionLiteratureDto>>> GetExtensionLiteraturesAsync()
            {
                var extensionLiteratures = await _publicationRepository.GetExtensionLiteraturesAsync();
                var dtos = extensionLiteratures.Select(el => new ExtensionLiteratureDto
                {
                    Id = el.Id,
                    ExtensionDate = el.ExtensionDate,
                    PublicationName = el.PublicationName,
                    NumberSold = el.NumberSold,
                    TotalFarmers = el.TotalFarmers
                }).ToList();
                return ServiceResult<List<ExtensionLiteratureDto>>.Success(dtos);
            }

            // Helper methods
            private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
            {
                var assignments = await _trainerAssignmentRepository.GetByTrainerIdAsync(_currentUserService.UserId);
                return assignments.Any(a => a.UnitLocationId == unitLocationId);
            }

            private Publication MapCreateDtoToEntity(PublicationCreateDto createDto)
            {
                return new Publication
                {
                    Title = createDto.Title,
                    PublicationDate = createDto.PublicationDate,
                    StartDate = createDto.StartDate,
                    EndDate = createDto.EndDate,
                    Attachements = createDto.Attachments, // Note: keeping the existing typo for consistency
                    UnitLocationId = createDto.UnitLocationId,
                    CategoryId = createDto.CategoryId,
                    ModeId = createDto.ModeId,
                    RegionId = createDto.RegionId,
                    SourceId = createDto.SourceId,
                    ExtensionLiteratureId = createDto.ExtensionLiteratureId,
                    MJASFormat = createDto.MJASFormat,
                    PublicationTitle = createDto.PublicationTitle,
                    PublicationJournalTitle = createDto.PublicationJournalTitle,
                    PublicationYear = createDto.PublicationYear,
                    PublicationVolume = createDto.PublicationVolume,
                    PublicationIssue = createDto.PublicationIssue,
                    PublicationPages = createDto.PublicationPages,
                    PublicationISBN = createDto.PublicationISBN,
                    PublicationUniNumber = createDto.PublicationUniNumber,
                    PublicationNAAS = createDto.PublicationNAAS,
                    PublicationImpact = createDto.PublicationImpact,
                    PublicationWebLink = createDto.PublicationWebLink,
                    PublicationCover = createDto.PublicationCover,
                    PublicationWhole = createDto.PublicationWhole,
                    Funds = createDto.Funds,
                    SponsorDetails = createDto.SponsorDetails,
                    PermissionLetterDate = createDto.PermissionLetterDate,
                    PermissionLetterUrl = createDto.PermissionLetterUrl,
                    PublisherBrochure = createDto.PublisherBrochure,
                    PublisherName = createDto.PublisherName,
                    PublisherInstitutionName = createDto.PublisherInstitutionName,
                    PublisherAddress = createDto.PublisherAddress
                };
            }

            private void MapUpdateDtoToEntity(PublicationUpdateDto updateDto, Publication entity)
            {
                if (updateDto.Title != null) entity.Title = updateDto.Title;
                if (updateDto.PublicationDate.HasValue) entity.PublicationDate = updateDto.PublicationDate;
                if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
                if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
                if (updateDto.Attachments != null) entity.Attachements = updateDto.Attachments;
                if (updateDto.CategoryId.HasValue) entity.CategoryId = updateDto.CategoryId;
                if (updateDto.ModeId.HasValue) entity.ModeId = updateDto.ModeId;
                if (updateDto.RegionId.HasValue) entity.RegionId = updateDto.RegionId;
                if (updateDto.SourceId.HasValue) entity.SourceId = updateDto.SourceId;
                if (updateDto.ExtensionLiteratureId.HasValue) entity.ExtensionLiteratureId = updateDto.ExtensionLiteratureId;
                if (updateDto.MJASFormat != null) entity.MJASFormat = updateDto.MJASFormat;
                if (updateDto.PublicationTitle != null) entity.PublicationTitle = updateDto.PublicationTitle;
                // Add all other fields as needed...
            }

            private PublicationDto MapEntityToDto(Publication entity)
            {
                return new PublicationDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    PublicationDate = entity.PublicationDate,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    Attachments = entity.Attachements,
                    UnitLocationId = entity.UnitLocationId,
                    UnitLocationName = $"{entity.UnitLocation?.Unit?.Name} - {entity.UnitLocation?.District?.Name}",
                    CategoryId = entity.CategoryId,
                    CategoryName = entity.Category?.CategoryName,
                    ModeId = entity.ModeId,
                    ModeName = entity.Mode?.ModeName,
                    RegionId = entity.RegionId,
                    RegionName = entity.Region?.RegionName,
                    SourceId = entity.SourceId,
                    SourceName = entity.Source?.SourceName,
                    ExtensionLiteratureId = entity.ExtensionLiteratureId,
                    ExtensionLiteratureName = entity.ExtensionLiterature?.PublicationName,
                    MJASFormat = entity.MJASFormat,
                    PublicationTitle = entity.PublicationTitle,
                    PublicationJournalTitle = entity.PublicationJournalTitle,
                    PublicationYear = entity.PublicationYear,
                    PublicationVolume = entity.PublicationVolume,
                    PublicationIssue = entity.PublicationIssue,
                    PublicationPages = entity.PublicationPages,
                    PublicationISBN = entity.PublicationISBN,
                    PublicationUniNumber = entity.PublicationUniNumber,
                    PublicationNAAS = entity.PublicationNAAS,
                    PublicationImpact = entity.PublicationImpact,
                    PublicationWebLink = entity.PublicationWebLink,
                    PublicationCover = entity.PublicationCover,
                    PublicationWhole = entity.PublicationWhole,
                    Funds = entity.Funds,
                    SponsorDetails = entity.SponsorDetails,
                    PermissionLetterDate = entity.PermissionLetterDate,
                    PermissionLetterUrl = entity.PermissionLetterUrl,
                    PublisherBrochure = entity.PublisherBrochure,
                    PublisherName = entity.PublisherName,
                    PublisherInstitutionName = entity.PublisherInstitutionName,
                    PublisherAddress = entity.PublisherAddress
                };
            }
        }
    }
}
