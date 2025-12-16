// Infrastructure/Services/DataTables/IBTVA/IbtvaProgramService.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Services.DataTables.IBTVA;
using Application.Mapper.DataTable.IBTVA;
using Application.Models.DataTables.IBTVA;
using Application.Services.Common;
using Infrastructure.Repository;

namespace Infrastructure.Services.DataTables.IBTVA
{
    public class IbtvaProgramService : IIbtvaProgramService
    {
        private readonly IIbtvaProgramDetailsRepository _programRepository;
        private readonly IIbtvaParticipantDemographicsRepository _demographicsRepository;
        private readonly IIbtvaProgramContentRepository _contentRepository;
        private readonly IIbtvaResourcePersonRepository _resourcePersonRepository;
        private readonly IIbtvaTopicsCoveredRepository _topicsRepository;
        private readonly IIbtvaTeachingAidsRepository _teachingAidsRepository;
        private readonly IIbtvaAdvisoryServicesRepository _advisoryRepository;
        private readonly IIbtvaReportRepository _reportRepository;
        private readonly IIbtvaRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly IbtvaProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly GenericTrainerHistoryService<IbtvaProgramDetails> _historyService;


        private const int IBTVA_UNIT_ID = 9;

        public IbtvaProgramService(
            IIbtvaProgramDetailsRepository programRepository,
            IIbtvaParticipantDemographicsRepository demographicsRepository,
            IIbtvaProgramContentRepository contentRepository,
            IIbtvaResourcePersonRepository resourcePersonRepository,
            IIbtvaTopicsCoveredRepository topicsRepository,
            IIbtvaTeachingAidsRepository teachingAidsRepository,
            IIbtvaAdvisoryServicesRepository advisoryRepository,
            IIbtvaReportRepository reportRepository,
            IIbtvaRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IUserRepository userRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IbtvaProgramMapper mapper)
        {
            _programRepository = programRepository;
            _demographicsRepository = demographicsRepository;
            _contentRepository = contentRepository;
            _resourcePersonRepository = resourcePersonRepository;
            _topicsRepository = topicsRepository;
            _teachingAidsRepository = teachingAidsRepository;
            _advisoryRepository = advisoryRepository;
            _reportRepository = reportRepository;
            _recommendationRepository = recommendationRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;

            //  generic history service
            _historyService = new GenericTrainerHistoryService<IbtvaProgramDetails>(currentUserService, trainerAssignmentRepository, organizationUnitRepository, unitHeadAssignmentRepository, userRepository);
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> CreateProgramAsync(IbtvaProgramCreateDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.FormStatus = "Draft";
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _programRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<IbtvaProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(program);
            return ServiceResult<IbtvaProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<IbtvaProgramCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<IbtvaProgramCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<IbtvaProgramCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> UpdateProgramAsync(int id, IbtvaProgramUpdateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaProgramDetailsDto>.Failure(
            //         "Cannot modify programs that are not in Draft or Rejected status",
            //         ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _programRepository.UpdateAsync(program);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<IbtvaProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteProgramAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied. You can only delete your own forms.", ServiceErrorStatus.FORBIDDEN);

            await _programRepository.DeleteAsync(id);
            return ServiceResult.Success("Program deleted successfully");
        }

        // ============================
        // SECTION B: DEMOGRAPHICS
        // ============================

        public async Task<ServiceResult<IbtvaParticipantDemographicsDto>> AddDemographicsAsync(
            int programId,
            IbtvaParticipantDemographicsCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
            //         "Cannot add demographics to approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _demographicsRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<IbtvaParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaParticipantDemographicsDto>> UpdateDemographicsAsync(
            int demographicsId,
            IbtvaParticipantDemographicsUpdateDto dto)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Demographics not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
            //         "Cannot modify demographics for approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _demographicsRepository.UpdateAsync(demographics);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<IbtvaParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteDemographicsAsync(int demographicsId)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult.Failure("Demographics not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft")
            //     return ServiceResult.Failure(
            //         "Cannot delete demographics from approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            await _demographicsRepository.DeleteAsync(demographicsId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<IbtvaParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<IbtvaParticipantDemographicsDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var demographics = await _demographicsRepository.GetByProgramIdAsync(programId);
            var dtos = demographics.Select(d => _mapper.MapToDto(d)).ToList();

            return ServiceResult<List<IbtvaParticipantDemographicsDto>>.Success(dtos);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create IbtvaProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// This solves the problem of needing parent ID before creating children
        /// </summary>
        public async Task<ServiceResult<IbtvaProgramContentDto>> AddProgramContentWithChildrenAsync(
            int programId,
            IbtvaProgramContentWithChildrenCreateDto dto)
        {
            // Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Validate form status
            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaProgramContentDto>.Failure(
            //         "Cannot add content to approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity
                var parentEntity = _mapper.MapToEntity(new IbtvaProgramContentCreateDto
                {
                    Title = dto.Title,
                    Description = dto.Description
                });
                parentEntity.IbtvaProgramDetailsId = programId;
                parentEntity.UnitLocationId = program.UnitLocationId;
                parentEntity.OrganizationId = program.OrganizationId;
                parentEntity.CreatedById = _currentUserService.UserId;
                parentEntity.CreatedAt = DateTimeOffset.UtcNow;

                // Prepare child entities
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = _mapper.MapToEntity(rp);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var topicsCovered = dto.TopicsCovered?.Select(tc =>
                {
                    var entity = _mapper.MapToEntity(tc);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var teachingAids = dto.TeachingAids?.Select(ta =>
                {
                    var entity = _mapper.MapToEntity(ta);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Repository handles transaction internally
                var createdContent = await _contentRepository.CreateWithChildrenAsync(
                    parentEntity,
                    resourcePersons,
                    topicsCovered,
                    teachingAids);

                var resultDto = _mapper.MapToDto(createdContent);
                return ServiceResult<IbtvaProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    $"Failed to create program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        /// <summary>
        /// Update IbtvaProgramContentAndResources with all child entities using Hybrid Pattern
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// </summary>
        public async Task<ServiceResult<IbtvaProgramContentDto>> UpdateProgramContentWithChildrenAsync(
            int contentId,
            IbtvaProgramContentWithChildrenUpdateDto dto)
        {
            // Validate content exists
            var content = await _contentRepository.GetWithDetailsAsync(contentId);
            if (content == null)
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            // Validate program and permissions
            var program = await _programRepository.GetByIdAsync(content.IbtvaProgramDetailsId ?? 0);
            if (program == null)
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Cannot update content in approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new IbtvaProgramContentAndResources
                {
                    Id = contentId,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities (hybrid: mix of new and existing)
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = new IbtvaResourcePerson
                    {
                        Id = rp.Id ?? 0, // 0 means new
                        Name = rp.Name,
                        Designation = rp.Designation,
                        ResourceType = rp.ResourceType,
                        Responsibility = rp.Responsibility,
                        InstitutionOrDepartment = rp.InstitutionOrDepartment,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                var topicsCovered = dto.TopicsCovered?.Select(tc =>
                {
                    var entity = new IbtvaTopicsCoveredInClass
                    {
                        Id = tc.Id ?? 0,
                        Date = tc.Date,
                        Title = tc.Title,
                        PhotoUpload = tc.PhotoUpload,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                var teachingAids = dto.TeachingAids?.Select(ta =>
                {
                    var entity = new IbtvaTeachingAidsDeveloped
                    {
                        Id = ta.Id ?? 0,
                        TypeOfAidId = ta.TypeOfAidId,
                        OtherTypeOfAid = ta.OtherTypeOfAid,
                        Purpose = ta.Purpose,
                        Number = ta.Number,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Repository handles transaction internally
                var updatedContent = await _contentRepository.UpdateWithChildrenAsync(
                    parentEntity,
                    resourcePersons,
                    topicsCovered,
                    teachingAids);

                var resultDto = _mapper.MapToDto(updatedContent);
                return ServiceResult<IbtvaProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    $"Failed to update program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        public async Task<ServiceResult<IbtvaProgramContentDto>> GetProgramContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetWithDetailsAsync(contentId);

            if (content == null)
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(content);
            return ServiceResult<IbtvaProgramContentDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft")
            //     return ServiceResult.Failure(
            //         "Cannot delete content from approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            await _contentRepository.DeleteAsync(contentId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<IbtvaProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<IbtvaProgramContentDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var contents = await _contentRepository.GetByProgramIdAsync(programId);
            var dtos = contents.Select(c => _mapper.MapToDto(c)).ToList();

            return ServiceResult<List<IbtvaProgramContentDto>>.Success(dtos);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<IbtvaAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(
            int programId,
            IbtvaAdvisoryServicesCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
            //         "Cannot modify advisory services for approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.IbtvaProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _advisoryRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<IbtvaAdvisoryServicesDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new IbtvaAdvisoryServicesUpdateDto
                {
                    Id = existing.Id,
                    NoOfFacebookSMS = dto.NoOfFacebookSMS,
                    NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers,
                    NoOfWhatsappGroups = dto.NoOfWhatsappGroups,
                    NoOfWhatsappSMS = dto.NoOfWhatsappSMS,
                    NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries,
                    NoOfPhoneCalls = dto.NoOfPhoneCalls,
                    NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions,
                    NoOfGroupDiscussions = dto.NoOfGroupDiscussions,
                    NoOfEmailsSent = dto.NoOfEmailsSent,
                    NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage,
                    NoOfBeneficiaries = dto.NoOfBeneficiaries
                };

                _mapper.MapUpdateDtoToEntity(updateDto, existing);
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _advisoryRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<IbtvaAdvisoryServicesDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<IbtvaAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var advisory = await _advisoryRepository.GetByProgramIdAsync(programId);

            if (advisory == null)
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(advisory);
            return ServiceResult<IbtvaAdvisoryServicesDto>.Success(dto);
        }




        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================

        public async Task<ServiceResult<IbtvaReportDto>> AddOrUpdateReportAsync(
            int programId,
            IbtvaReportCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);



            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaReportDto>.Failure(
            //         "Cannot modify reports for approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _reportRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.IbtvaProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _reportRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<IbtvaReportDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new IbtvaReportUpdateDto
                {
                    Id = existing.Id,                  
                    ReportDate = dto.ReportDate,
                    ProgressReport = dto.ProgressReport,
                    GeoTaggedPhoto = dto.GeoTaggedPhoto,
                    ReportingVideo = dto.ReportingVideo,
                    Outcome = dto.Outcome,
                    TestingCompletionDate = dto.TestingCompletionDate,
                    TestingCompletionLetter = dto.TestingCompletionLetter,
                    ProjectCompletionDate = dto.ProjectCompletionDate,
                    ProjectCompletionLetter = dto.ProjectCompletionLetter,
                    TypeOfReport = dto.TypeOfReport,
                    SpclReport = dto.SpclReport
                };




                _mapper.MapUpdateDtoToEntity(updateDto, existing);
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _reportRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<IbtvaReportDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<IbtvaReportDto>> GetReportByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var report = await _reportRepository.GetByProgramIdAsync(programId);

            if (report == null)
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(report);
            return ServiceResult<IbtvaReportDto>.Success(dto);
        }

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<IbtvaRecommendationDto>> AddOrUpdateRecommendationAsync(
            int programId,
            IbtvaRecommendationCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //     return ServiceResult<IbtvaRecommendationDto>.Failure(
            //         "Cannot modify recommendations for approved programs",
            //         ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.IbtvaProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _recommendationRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                // // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
                // if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
                // {
                program.FormStatus = "Pending";
                program.UpdatedById = _currentUserService.UserId;
                program.UpdatedAt = DateTimeOffset.UtcNow;
                await _programRepository.UpdateAsync(program);
                // }

                return ServiceResult<IbtvaRecommendationDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new IbtvaRecommendationUpdateDto
                {
                    Id = existing.Id,
                    ProblemsIdentified = dto.ProblemsIdentified,
                    Recommendation = dto.Recommendation,
                    ActionTaken = dto.ActionTaken,
                    SignificantAchievement = dto.SignificantAchievement,
                    SuccessStories = dto.SuccessStories,
                    ImpactOutcome = dto.ImpactOutcome
                };

                _mapper.MapUpdateDtoToEntity(updateDto, existing);
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _recommendationRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
                // if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
                // {
                program.FormStatus = "Pending";
                program.UpdatedById = _currentUserService.UserId;
                program.UpdatedAt = DateTimeOffset.UtcNow;
                await _programRepository.UpdateAsync(program);
                //}

                return ServiceResult<IbtvaRecommendationDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<IbtvaRecommendationDto>> GetRecommendationByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var recommendation = await _recommendationRepository.GetByProgramIdAsync(programId);

            if (recommendation == null)
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(recommendation);
            return ServiceResult<IbtvaRecommendationDto>.Success(dto);
        }

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================

        public async Task<ServiceResult> SubmitForApprovalAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only draft or rejected programs can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            program.FormStatus = "Pending";
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ApproveAsync(int programId, string? remarks = null)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can approve
            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can approve programs",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending programs can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

            program.FormStatus = "Approved";
            program.FormStatusRemarks = remarks;
            program.ApprovedById = _currentUserService.UserId;
            program.ApprovedAt = DateTimeOffset.UtcNow;
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectAsync(int programId, string remarks)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can reject
            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can reject programs",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending programs can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.BADREQUEST);

            program.FormStatus = "Rejected";
            program.FormStatusRemarks = remarks;
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        // ============================
        // LISTING & FILTERING
        // ============================

        public async Task<PaginatedResult<IbtvaProgramListItemDto>> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            DateOnly? startDate,
            DateOnly? endDate,
            int? categoryId,
            string? searchTerm,
            string? formStatus,
            int? createdById,
            int? unitLocationId)
        {
            var query = _programRepository.GetQueryable()
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .AsQueryable();

            // Apply filters
            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(formStatus))
                query = query.Where(x => x.FormStatus == formStatus);

            if (createdById.HasValue)
                query = query.Where(x => x.CreatedById == createdById.Value);

            if (unitLocationId.HasValue)
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(x =>
                    (x.Title != null && x.Title.ToLower().Contains(lowerSearchTerm)) ||
                    (x.Location != null && x.Location.ToLower().Contains(lowerSearchTerm)));
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new IbtvaProgramListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CategoryName = x.Category != null ? x.Category.Name : null,
                    TypeName = x.Type != null ? x.Type.Name : null,
                    Location = x.Location,
                    FormStatus = x.FormStatus,
                    CreatedByName = x.CreatedBy != null ? x.CreatedBy.FirstName : null,
                    CreatedAt = x.CreatedAt,
                    UnitName = x.UnitLocation != null ? x.UnitLocation.Unit.Name : null
                })
                .ToListAsync();

            return new PaginatedResult<IbtvaProgramListItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<IbtvaProgramListItemDto>> GetByStatusAsync(
            string status,
            int pageNumber,
            int pageSize)
        {
            var query = _programRepository.GetQueryable()
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .Where(x => x.FormStatus == status);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new IbtvaProgramListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CategoryName = x.Category != null ? x.Category.Name : null,
                    TypeName = x.Type != null ? x.Type.Name : null,
                    Location = x.Location,
                    FormStatus = x.FormStatus,
                    CreatedByName = x.CreatedBy != null ? x.CreatedBy.FirstName : null,
                    CreatedAt = x.CreatedAt,
                    UnitName = x.UnitLocation != null ? x.UnitLocation.Unit.Name : null
                })
                .ToListAsync();

            return new PaginatedResult<IbtvaProgramListItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var summary = await _programRepository.GetQueryable()
                .GroupBy(x => x.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return summary.ToDictionary(x => x.Status, x => x.Count);
        }

        // -------------------------------------------------------
        // HISTORY: Using Generic Service
        // -------------------------------------------------------

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        public async Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Get base query with necessary includes
            var query = _programRepository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.ProgramType?.Name,
                getFormStatus: x => x.FormStatus,
                getRemarks: x => x.FormStatusRemarks ?? "-",
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get pending approvals for Unit Head with pagination
        /// </summary>
        public async Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _programRepository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.ProgramType?.Name,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize);
        }


    }
}