

using Application.Services.Common;

namespace Infrastructure.Services.DataTables.NAEP
{
    public class NaepProgramService : INaepProgramService
    {
        private readonly INaepProgramDetailsRepository _programRepository;
        private readonly INaepParticipantDemographicsRepository _demographicsRepository;
        private readonly INaepProgramContentRepository _contentRepository;
        private readonly INaepResourcePersonRepository _resourcePersonRepository;
        private readonly INaepTopicsCoveredRepository _topicsRepository;
        private readonly INaepTeachingAidsRepository _teachingAidsRepository;
        private readonly INaepAdvisoryServicesRepository _advisoryRepository;
        private readonly INaepReportRepository _reportRepository;
        private readonly INaepRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly NaepProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly GenericTrainerHistoryService<NaepProgramDetails> _historyService;


        public NaepProgramService(
            INaepProgramDetailsRepository programRepository,
            INaepParticipantDemographicsRepository demographicsRepository,
            INaepProgramContentRepository contentRepository,
            INaepResourcePersonRepository resourcePersonRepository,
            INaepTopicsCoveredRepository topicsRepository,
            INaepTeachingAidsRepository teachingAidsRepository,
            INaepAdvisoryServicesRepository advisoryRepository,
            INaepReportRepository reportRepository,
            INaepRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            NaepProgramMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
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
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<NaepProgramDetails>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<NaepProgramDetailsDto>> CreateProgramAsync(NaepProgramCreateDto dto)
        {
            // Verify access to unit location
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Access denied to this unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var program = _mapper.MapToEntity(dto);
            program.OrganizationId = _currentUserService.OrganizationId;
            program.CreatedById = _currentUserService.UserId;
            program.CreatedAt = DateTimeOffset.UtcNow;

            // Auto-approve forms created by Unit Heads
            if (_currentUserService.Role == Role.UNITHEAD)
            {
                program.FormStatus = "Approved";
                program.ApprovedById = _currentUserService.UserId;
                program.ApprovedAt = DateTimeOffset.UtcNow;
                program.FormStatusRemarks = "Auto-approved (Unit Head)";
            }
            else
            {
                program.FormStatus = "Draft";
            }

            await _programRepository.CreateAsync(program);

            var result = await _programRepository.GetWithDetailsAsync(program.Id);
            var resultDto = _mapper.MapToDtoWithDetails(result!);

            return ServiceResult<NaepProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<NaepProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(program);
            return ServiceResult<NaepProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<NaepProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<NaepProgramDetailsCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<NaepProgramDetailsCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<NaepProgramDetailsCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<NaepProgramDetailsDto>> UpdateProgramAsync(int id, NaepProgramUpdateDto dto)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Allow edit for Draft, Rejected, or Approved (if unit head is the creator)
            if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
            {
                // Trainers can edit Draft and Rejected forms
            }
            else if (program.FormStatus == "Approved" &&
                     _currentUserService.Role == Role.UNITHEAD &&
                     program.CreatedById == _currentUserService.UserId)
            {
                // Unit heads can edit their own approved forms
            }
            else
            {
                return ServiceResult<NaepProgramDetailsDto>.Failure(
                    "Cannot edit programs in current status",
                    ServiceErrorStatus.INVALIDOPERATION);
            }

            NaepProgramMapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);

            var updatedProgram = await _programRepository.GetWithDetailsAsync(id);
            var resultDto = _mapper.MapToDtoWithDetails(updatedProgram!);

            return ServiceResult<NaepProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteProgramAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only draft programs can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _programRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================

        public async Task<ServiceResult<NaepParticipantDemographicsDto>> AddDemographicsAsync(
            int programId,
            NaepParticipantDemographicsCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var demographics = _mapper.MapToEntity(dto);
            demographics.NaepProgramDetailsId = programId;
            demographics.OrganizationId = _currentUserService.OrganizationId;
            demographics.UnitLocationId = program.UnitLocationId;
            demographics.CreatedById = _currentUserService.UserId;
            demographics.CreatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.CreateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<NaepParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<NaepParticipantDemographicsDto>> UpdateDemographicsAsync(
            int demographicsId,
            NaepParticipantDemographicsUpdateDto dto)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Demographics not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepParticipantDemographicsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            NaepProgramMapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.UpdateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<NaepParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteDemographicsAsync(int demographicsId)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult.Failure("Demographics not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _demographicsRepository.DeleteAsync(demographicsId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<NaepParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<NaepParticipantDemographicsDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var demographics = await _demographicsRepository.GetByProgramIdAsync(programId);
            var dtos = demographics.Select(d => _mapper.MapToDto(d)).ToList();

            return ServiceResult<List<NaepParticipantDemographicsDto>>.Success(dtos);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        public async Task<ServiceResult<NaepProgramContentDto>> AddProgramContentAsync(
            int programId,
            NaepProgramContentCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var content = _mapper.MapToEntity(dto);
            content.NaepProgramDetailsId = programId;
            content.OrganizationId = _currentUserService.OrganizationId;
            content.UnitLocationId = program.UnitLocationId;
            content.CreatedById = _currentUserService.UserId;
            content.CreatedAt = DateTimeOffset.UtcNow;

            await _contentRepository.CreateAsync(content);

            var result = await _contentRepository.GetWithDetailsAsync(content.Id);
            var resultDto = _mapper.MapToDto(result!);

            return ServiceResult<NaepProgramContentDto>.Success(resultDto);
        }

        /// <summary>
        /// Create NaepProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// This solves the problem of needing parent ID before creating children
        /// </summary>
        public async Task<ServiceResult<NaepProgramContentDto>> AddProgramContentWithChildrenAsync(
            int programId,
            NaepProgramContentWithChildrenCreateDto dto)
        {
            // Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Validate form status
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending" && program.FormStatus != "Rejected")
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Cannot add content to approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity
                var parentEntity = _mapper.MapToEntity(new NaepProgramContentCreateDto
                {
                    Title = dto.Title,
                    Description = dto.Description
                });
                parentEntity.NaepProgramDetailsId = programId;
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
                return ServiceResult<NaepProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<NaepProgramContentDto>.Failure(
                    $"Failed to create program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        /// <summary>
        /// Update NaepProgramContentAndResources with all child entities using Hybrid Pattern
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// </summary>
        public async Task<ServiceResult<NaepProgramContentDto>> UpdateProgramContentWithChildrenAsync(
            int contentId,
            NaepProgramContentWithChildrenUpdateDto dto)
        {
            // Validate content exists
            var content = await _contentRepository.GetWithDetailsAsync(contentId);
            if (content == null)
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            // Validate program and permissions
            var program = await _programRepository.GetByIdAsync(content.NaepProgramDetailsId ?? 0);
            if (program == null)
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending" && program.FormStatus != "Rejected")
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Cannot update content in approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new NaepProgramContentAndResources
                {
                    Id = contentId,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities (hybrid: mix of new and existing)
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = new NaepResourcePerson
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
                    var entity = new NaepTopicsCoveredInClass
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
                    var entity = new NaepTeachingAidsDeveloped
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
                return ServiceResult<NaepProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<NaepProgramContentDto>.Failure(
                    $"Failed to update program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }


        public async Task<ServiceResult<NaepProgramContentDto>> GetProgramContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetWithDetailsAsync(contentId);

            if (content == null)
                return ServiceResult<NaepProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(content);
            return ServiceResult<NaepProgramContentDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _contentRepository.DeleteAsync(contentId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<NaepProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<NaepProgramContentDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var contents = await _contentRepository.GetByProgramIdAsync(programId);
            var dtos = contents.Select(c => _mapper.MapToDto(c)).ToList();

            return ServiceResult<List<NaepProgramContentDto>>.Success(dtos);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<NaepAdvisoryServicesDto>> AddAdvisoryServicesAsync(
            int programId,
            NaepAdvisoryServicesCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if advisory services already exist
            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Advisory services already exist for this program",
                    ServiceErrorStatus.INVALIDOPERATION);

            var advisory = _mapper.MapToEntity(dto);
            advisory.NaepProgramDetailsId = programId;
            advisory.OrganizationId = _currentUserService.OrganizationId;
            advisory.UnitLocationId = program.UnitLocationId;
            advisory.CreatedById = _currentUserService.UserId;
            advisory.CreatedAt = DateTimeOffset.UtcNow;

            await _advisoryRepository.CreateAsync(advisory);

            var resultDto = _mapper.MapToDto(advisory);
            return ServiceResult<NaepAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult<NaepAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(
            int advisoryId,
            NaepAdvisoryServicesUpdateDto dto)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.NoOfFacebookSMS.HasValue) advisory.NoOfFacebookSMS = dto.NoOfFacebookSMS.Value;
            if (dto.NoOfSMSSentToRegisteredFarmers.HasValue) advisory.NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers.Value;
            if (dto.NoOfWhatsappGroups.HasValue) advisory.NoOfWhatsappGroups = dto.NoOfWhatsappGroups.Value;
            if (dto.NoOfWhatsappSMS.HasValue) advisory.NoOfWhatsappSMS = dto.NoOfWhatsappSMS.Value;
            if (dto.NoOfAnsweredWhatsappQueries.HasValue) advisory.NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries.Value;
            if (dto.NoOfPhoneCalls.HasValue) advisory.NoOfPhoneCalls = dto.NoOfPhoneCalls.Value;
            if (dto.NoOfFaceToFaceDiscussions.HasValue) advisory.NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions.Value;
            if (dto.NoOfGroupDiscussions.HasValue) advisory.NoOfGroupDiscussions = dto.NoOfGroupDiscussions.Value;
            if (dto.NoOfEmailsSent.HasValue) advisory.NoOfEmailsSent = dto.NoOfEmailsSent.Value;
            if (dto.NoOfNewspaperCoverage.HasValue) advisory.NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage.Value;
            if (dto.NoOfBeneficiaries.HasValue) advisory.NoOfBeneficiaries = dto.NoOfBeneficiaries.Value;

            advisory.UpdatedById = _currentUserService.UserId;
            advisory.UpdatedAt = DateTimeOffset.UtcNow;

            await _advisoryRepository.UpdateAsync(advisory);

            var resultDto = _mapper.MapToDto(advisory);
            return ServiceResult<NaepAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult.Failure("Advisory services not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _advisoryRepository.DeleteAsync(advisoryId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<NaepAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var advisory = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (advisory == null)
                return ServiceResult<NaepAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(advisory);
            return ServiceResult<NaepAdvisoryServicesDto>.Success(dto);
        }

        // ============================
        // SECTION E: REPORTS
        // ============================

        public async Task<ServiceResult<NaepReportDto>> AddReportAsync(
            int programId,
            NaepReportCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepReportDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if report already exists
            var existing = await _reportRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<NaepReportDto>.Failure(
                    "Report already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var report = _mapper.MapToEntity(dto);
            report.NaepProgramDetailsId = programId;
            report.OrganizationId = _currentUserService.OrganizationId;
            report.UnitLocationId = program.UnitLocationId;
            report.CreatedById = _currentUserService.UserId;
            report.CreatedAt = DateTimeOffset.UtcNow;

            await _reportRepository.CreateAsync(report);

            var resultDto = _mapper.MapToDto(report);
            return ServiceResult<NaepReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult<NaepReportDto>> UpdateReportAsync(
            int reportId,
            NaepReportUpdateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult<NaepReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepReportDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.ProgressReportReportingYear != null) report.ProgressReportReportingYear = dto.ProgressReportReportingYear;
            if (dto.Date.HasValue) report.Date = dto.Date;
            if (dto.UploadPhoto != null) report.UploadPhoto = dto.UploadPhoto;
            if (dto.PhotosGeotaggedPhotoOrUploadPhoto != null) report.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
            if (dto.UploadVideo != null) report.UploadVideo = dto.UploadVideo;
            if (dto.SignificantOutcome != null) report.SignificantOutcome = dto.SignificantOutcome;

            report.UpdatedById = _currentUserService.UserId;
            report.UpdatedAt = DateTimeOffset.UtcNow;

            await _reportRepository.UpdateAsync(report);

            var resultDto = _mapper.MapToDto(report);
            return ServiceResult<NaepReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteReportAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult.Failure("Report not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.CONFLICT);

            await _reportRepository.DeleteAsync(reportId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<NaepReportDto>> GetReportByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var report = await _reportRepository.GetByProgramIdAsync(programId);
            if (report == null)
                return ServiceResult<NaepReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(report);
            return ServiceResult<NaepReportDto>.Success(dto);
        }

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<NaepRecommendationDto>> AddRecommendationAsync(
            int programId,
            NaepRecommendationCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if recommendation already exists
            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Recommendation already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var recommendation = _mapper.MapToEntity(dto);
            recommendation.NaepProgramDetailsId = programId;
            recommendation.OrganizationId = _currentUserService.OrganizationId;
            recommendation.UnitLocationId = program.UnitLocationId;
            recommendation.CreatedById = _currentUserService.UserId;
            recommendation.CreatedAt = DateTimeOffset.UtcNow;

            await _recommendationRepository.CreateAsync(recommendation);

            // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
            if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
            {
                program.FormStatus = "Pending";
                program.UpdatedById = _currentUserService.UserId;
                program.UpdatedAt = DateTimeOffset.UtcNow;
                await _programRepository.UpdateAsync(program);
            }

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<NaepRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult<NaepRecommendationDto>> UpdateRecommendationAsync(
            int recommendationId,
            NaepRecommendationUpdateDto dto)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.CONFLICT);

            if (dto.ProblemsIdentified != null) recommendation.ProblemsIdentified = dto.ProblemsIdentified;
            if (dto.Recommendation != null) recommendation.Recommendation = dto.Recommendation;
            if (dto.ActionTaken != null) recommendation.ActionTaken = dto.ActionTaken;
            if (dto.SignificantAchievement != null) recommendation.SignificantAchievement = dto.SignificantAchievement;
            if (dto.SuccessStories != null) recommendation.SuccessStories = dto.SuccessStories;
            if (dto.ImpactOutcome != null) recommendation.ImpactOutcome = dto.ImpactOutcome;

            recommendation.UpdatedById = _currentUserService.UserId;
            recommendation.UpdatedAt = DateTimeOffset.UtcNow;

            await _recommendationRepository.UpdateAsync(recommendation);

            // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
            if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
            {
                program.FormStatus = "Pending";
                program.UpdatedById = _currentUserService.UserId;
                program.UpdatedAt = DateTimeOffset.UtcNow;
                await _programRepository.UpdateAsync(program);
            }

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<NaepRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteRecommendationAsync(int recommendationId)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult.Failure("Recommendation not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.NaepProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _recommendationRepository.DeleteAsync(recommendationId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<NaepRecommendationDto>> GetRecommendationByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var recommendation = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (recommendation == null)
                return ServiceResult<NaepRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(recommendation);
            return ServiceResult<NaepRecommendationDto>.Success(dto);
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
                    "Only draft programs can be submitted",
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
                    "Remarks are required when rejecting",
                    ServiceErrorStatus.INVALIDOPERATION);

            program.FormStatus = "Rejected";
            program.FormStatusRemarks = remarks;
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        // ============================
        // PAGINATION & FILTERING
        // ============================

        public async Task<PaginatedResult<NaepProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            if (unitLocationId.HasValue && unitLocationIds.Contains(unitLocationId.Value))
            {
                unitLocationIds = new List<int> { unitLocationId.Value };
            }

            var result = await _programRepository.GetPaginatedAsync(
                unitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                programTypeId,
                searchTerm);

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NaepProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<NaepProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _programRepository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);
            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NaepProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _programRepository.GetStatusSummaryAsync(unitLocationIds);
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

        /// <summary>
        /// Get programs by trainer ID with optional unit location filter
        /// </summary>
        public async Task<PaginatedResult<NaepProgramDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Get accessible unit locations for the current user
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _programRepository.GetQueryable()
                .Include(x => x.ProgramType)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.Theme)
                .Include(x => x.ThematicArea)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District)
                .Where(x => x.CreatedById == trainerId)
                .Where(x => unitLocationIds.Contains(x.UnitLocationId));

            // Apply unit location filter if provided
            if (unitLocationId.HasValue)
            {
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);
            }

            var totalCount = await query.CountAsync();
            var programs = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = programs.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NaepProgramDetailsDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        public async Task<PaginatedResult<NaepProgramDetailsDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // If trainerId is provided, get that trainer's forms (for unit heads viewing trainer's history)
            if (trainerId.HasValue)
            {
                return await GetByTrainerAsync(trainerId.Value, unitLocationId, pageNumber, pageSize);
            }

            // Otherwise, get own history (works for both trainers and unit heads)
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _programRepository.GetQueryable()
                .Include(x => x.ProgramType)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.Theme)
                .Include(x => x.ThematicArea)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District)
                .Where(x => x.CreatedById == _currentUserService.UserId)
                .Where(x => unitLocationIds.Contains(x.UnitLocationId));

            // Apply unit location filter if provided
            if (unitLocationId.HasValue)
            {
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);
            }

            var totalCount = await query.CountAsync();
            var programs = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = programs.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NaepProgramDetailsDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        // ============================
        // HELPER METHODS
        // ============================

        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return accessibleUnitLocationIds.Contains(unitLocationId);
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            if (_currentUserService.Role == Role.TRAINER)
            {
                return await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);
            }
            else if (_currentUserService.Role == Role.UNITHEAD)
            {
                return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);
            }
            else if (_currentUserService.Role == Role.ADMIN)
            {
                return await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);
            }

            return new List<int>();
        }
    }
}