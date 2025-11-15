

using Application.Services.Common;

namespace Infrastructure.Services.DataTables.EEU
{
    public class EeuProgramService : IEeuProgramService
    {
        private readonly IEeuProgramDetailsRepository _programRepository;
        private readonly IEeuParticipantDemographicsRepository _demographicsRepository;
        private readonly IEeuProgramContentRepository _contentRepository;
        private readonly IEeuResourcePersonRepository _resourcePersonRepository;
        private readonly IEeuTopicsCoveredRepository _topicsRepository;
        private readonly IEeuTeachingAidsRepository _teachingAidsRepository;
        private readonly IEeuAdvisoryServicesRepository _advisoryRepository;
        private readonly IEeuReportRepository _reportRepository;
        private readonly IEeuRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly EeuProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly GenericTrainerHistoryService<EeuProgramDetails> _historyService;

        public EeuProgramService(
            IEeuProgramDetailsRepository programRepository,
            IEeuParticipantDemographicsRepository demographicsRepository,
            IEeuProgramContentRepository contentRepository,
            IEeuResourcePersonRepository resourcePersonRepository,
            IEeuTopicsCoveredRepository topicsRepository,
            IEeuTeachingAidsRepository teachingAidsRepository,
            IEeuAdvisoryServicesRepository advisoryRepository,
            IEeuReportRepository reportRepository,
            IEeuRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            EeuProgramMapper mapper,
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
            _historyService = new GenericTrainerHistoryService<EeuProgramDetails>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<EeuProgramDetailsDto>> CreateProgramAsync(EeuProgramCreateDto dto)
        {
            // Verify access to unit location
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Access denied to this unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var program = _mapper.MapToEntity(dto);
            program.OrganizationId = _currentUserService.OrganizationId;
            program.CreatedById = _currentUserService.UserId;
            program.CreatedAt = DateTimeOffset.UtcNow;
            program.FormStatus = "Draft";

            await _programRepository.CreateAsync(program);

            var result = await _programRepository.GetWithDetailsAsync(program.Id);
            var resultDto = _mapper.MapToDtoWithDetails(result!);

            return ServiceResult<EeuProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(program);
            return ServiceResult<EeuProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<EeuProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramDetailsCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<EeuProgramDetailsCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<EeuProgramDetailsCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<EeuProgramDetailsDto>> UpdateProgramAsync(int id, EeuProgramUpdateDto dto)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Cannot edit programs that have been submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            EeuProgramMapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);

            var updatedProgram = await _programRepository.GetWithDetailsAsync(id);
            var resultDto = _mapper.MapToDtoWithDetails(updatedProgram!);

            return ServiceResult<EeuProgramDetailsDto>.Success(resultDto);
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

        public async Task<ServiceResult<EeuParticipantDemographicsDto>> AddDemographicsAsync(
            int programId,
            EeuParticipantDemographicsCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var demographics = _mapper.MapToEntity(dto);
            demographics.EeuProgramDetailsId = programId;
            demographics.OrganizationId = _currentUserService.OrganizationId;
            demographics.UnitLocationId = program.UnitLocationId;
            demographics.CreatedById = _currentUserService.UserId;
            demographics.CreatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.CreateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<EeuParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuParticipantDemographicsDto>> UpdateDemographicsAsync(
            int demographicsId,
            EeuParticipantDemographicsUpdateDto dto)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Demographics not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuParticipantDemographicsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            EeuProgramMapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.UpdateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<EeuParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteDemographicsAsync(int demographicsId)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult.Failure("Demographics not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _demographicsRepository.DeleteAsync(demographicsId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<EeuParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<EeuParticipantDemographicsDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var demographics = await _demographicsRepository.GetByProgramIdAsync(programId);
            var dtos = demographics.Select(d => _mapper.MapToDto(d)).ToList();

            return ServiceResult<List<EeuParticipantDemographicsDto>>.Success(dtos);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        public async Task<ServiceResult<EeuProgramContentDto>> AddProgramContentAsync(
            int programId,
            EeuProgramContentCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var content = _mapper.MapToEntity(dto);
            content.EeuProgramDetailsId = programId;
            content.OrganizationId = _currentUserService.OrganizationId;
            content.UnitLocationId = program.UnitLocationId;
            content.CreatedById = _currentUserService.UserId;
            content.CreatedAt = DateTimeOffset.UtcNow;

            await _contentRepository.CreateAsync(content);

            var result = await _contentRepository.GetWithDetailsAsync(content.Id);
            var resultDto = _mapper.MapToDto(result!);

            return ServiceResult<EeuProgramContentDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuProgramContentDto>> GetProgramContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetWithDetailsAsync(contentId);

            if (content == null)
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(content);
            return ServiceResult<EeuProgramContentDto>.Success(dto);
        }

        /// <summary>
        /// Create EeuProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// This solves the problem of needing parent ID before creating children
        /// </summary>
        public async Task<ServiceResult<EeuProgramContentDto>> AddProgramContentWithChildrenAsync(
            int programId,
            EeuProgramContentWithChildrenCreateDto dto)
        {
            // Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Validate form status
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending" && program.FormStatus != "Rejected")
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Cannot add content to approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity
                var parentEntity = _mapper.MapToEntity(new EeuProgramContentCreateDto
                {
                    Title = dto.Title,
                    Description = dto.Description
                });
                parentEntity.EeuProgramDetailsId = programId;
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
                return ServiceResult<EeuProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuProgramContentDto>.Failure(
                    $"Failed to create program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        /// <summary>
        /// Update EeuProgramContentAndResources with all child entities using Hybrid Pattern
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// </summary>
        public async Task<ServiceResult<EeuProgramContentDto>> UpdateProgramContentWithChildrenAsync(
            int contentId,
            EeuProgramContentWithChildrenUpdateDto dto)
        {
            // Validate content exists
            var content = await _contentRepository.GetWithDetailsAsync(contentId);
            if (content == null)
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            // Validate program and permissions
            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null)
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending" && program.FormStatus != "Rejected")
                return ServiceResult<EeuProgramContentDto>.Failure(
                    "Cannot update content in approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new EeuProgramContentAndResources
                {
                    Id = contentId,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities (hybrid: mix of new and existing)
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = new EeuResourcePerson
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
                    var entity = new EeuTopicsCoveredInClass
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
                    var entity = new EeuTeachingAidsDeveloped
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
                return ServiceResult<EeuProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuProgramContentDto>.Failure(
                    $"Failed to update program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }


        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _contentRepository.DeleteAsync(contentId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<EeuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<EeuProgramContentDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var contents = await _contentRepository.GetByProgramIdAsync(programId);
            var dtos = contents.Select(c => _mapper.MapToDto(c)).ToList();

            return ServiceResult<List<EeuProgramContentDto>>.Success(dtos);
        }

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================

        public async Task<ServiceResult<EeuResourcePersonDto>> AddResourcePersonAsync(
            int contentId,
            EeuResourcePersonCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var person = _mapper.MapToEntity(dto);
            person.EeuProgramContentAndResourcesId = contentId;
            person.OrganizationId = _currentUserService.OrganizationId;
            person.UnitLocationId = program.UnitLocationId;
            person.CreatedById = _currentUserService.UserId;
            person.CreatedAt = DateTimeOffset.UtcNow;

            await _resourcePersonRepository.CreateAsync(person);

            var resultDto = _mapper.MapToDto(person);
            return ServiceResult<EeuResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuResourcePersonDto>> UpdateResourcePersonAsync(
            int personId,
            EeuResourcePersonUpdateDto dto)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Resource person not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuResourcePersonDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Apply updates from DTO
            if (dto.Name != null) person.Name = dto.Name;
            if (dto.Designation != null) person.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) person.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) person.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) person.InstitutionOrDepartment = dto.InstitutionOrDepartment;

            person.UpdatedById = _currentUserService.UserId;
            person.UpdatedAt = DateTimeOffset.UtcNow;

            await _resourcePersonRepository.UpdateAsync(person);

            var resultDto = _mapper.MapToDto(person);
            return ServiceResult<EeuResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteResourcePersonAsync(int personId)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult.Failure("Resource person not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _resourcePersonRepository.DeleteAsync(personId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<EeuResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<EeuResourcePersonDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var persons = await _resourcePersonRepository.GetByContentIdAsync(contentId);
            var dtos = persons.Select(p => _mapper.MapToDto(p)).ToList();

            return ServiceResult<List<EeuResourcePersonDto>>.Success(dtos);
        }

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================

        public async Task<ServiceResult<EeuTopicsCoveredDto>> AddTopicAsync(
            int contentId,
            EeuTopicsCoveredCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var topic = _mapper.MapToEntity(dto);
            topic.EeuProgramContentAndResourcesId = contentId;
            topic.OrganizationId = _currentUserService.OrganizationId;
            topic.UnitLocationId = program.UnitLocationId;
            topic.CreatedById = _currentUserService.UserId;
            topic.CreatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.CreateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<EeuTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuTopicsCoveredDto>> UpdateTopicAsync(
            int topicId,
            EeuTopicsCoveredUpdateDto dto)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Topic not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuTopicsCoveredDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.Date.HasValue) topic.Date = dto.Date;
            if (dto.Title != null) topic.Title = dto.Title;
            if (dto.PhotoUpload != null) topic.PhotoUpload = dto.PhotoUpload;

            topic.UpdatedById = _currentUserService.UserId;
            topic.UpdatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.UpdateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<EeuTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTopicAsync(int topicId)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult.Failure("Topic not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _topicsRepository.DeleteAsync(topicId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<EeuTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<EeuTopicsCoveredDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var topics = await _topicsRepository.GetByContentIdAsync(contentId);
            var dtos = topics.Select(t => _mapper.MapToDto(t)).ToList();

            return ServiceResult<List<EeuTopicsCoveredDto>>.Success(dtos);
        }

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================

        public async Task<ServiceResult<EeuTeachingAidsDto>> AddTeachingAidAsync(
            int contentId,
            EeuTeachingAidsCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var aid = _mapper.MapToEntity(dto);
            aid.EeuProgramContentAndResourcesId = contentId;
            aid.OrganizationId = _currentUserService.OrganizationId;
            aid.UnitLocationId = program.UnitLocationId;
            aid.CreatedById = _currentUserService.UserId;
            aid.CreatedAt = DateTimeOffset.UtcNow;

            await _teachingAidsRepository.CreateAsync(aid);

            var resultDto = _mapper.MapToDto(aid);
            return ServiceResult<EeuTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuTeachingAidsDto>> UpdateTeachingAidAsync(
            int aidId,
            EeuTeachingAidsUpdateDto dto)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Teaching aid not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuTeachingAidsDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.TypeOfAidId.HasValue) aid.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) aid.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) aid.Purpose = dto.Purpose;
            if (dto.Number.HasValue) aid.Number = dto.Number.Value;

            aid.UpdatedById = _currentUserService.UserId;
            aid.UpdatedAt = DateTimeOffset.UtcNow;

            await _teachingAidsRepository.UpdateAsync(aid);

            var resultDto = _mapper.MapToDto(aid);
            return ServiceResult<EeuTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTeachingAidAsync(int aidId)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult.Failure("Teaching aid not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.EeuProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.EeuProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _teachingAidsRepository.DeleteAsync(aidId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<EeuTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<EeuTeachingAidsDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var aids = await _teachingAidsRepository.GetByContentIdAsync(contentId);
            var dtos = aids.Select(a => _mapper.MapToDto(a)).ToList();

            return ServiceResult<List<EeuTeachingAidsDto>>.Success(dtos);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<EeuAdvisoryServicesDto>> AddAdvisoryServicesAsync(
            int programId,
            EeuAdvisoryServicesCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if advisory services already exist
            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Advisory services already exist for this program",
                    ServiceErrorStatus.INVALIDOPERATION);

            var advisory = _mapper.MapToEntity(dto);
            advisory.EeuProgramDetailsId = programId;
            advisory.OrganizationId = _currentUserService.OrganizationId;
            advisory.UnitLocationId = program.UnitLocationId;
            advisory.CreatedById = _currentUserService.UserId;
            advisory.CreatedAt = DateTimeOffset.UtcNow;

            await _advisoryRepository.CreateAsync(advisory);

            var resultDto = _mapper.MapToDto(advisory);
            return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(
            int advisoryId,
            EeuAdvisoryServicesUpdateDto dto)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
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
            return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult.Failure("Advisory services not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _advisoryRepository.DeleteAsync(advisoryId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<EeuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var advisory = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (advisory == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(advisory);
            return ServiceResult<EeuAdvisoryServicesDto>.Success(dto);
        }

        // ============================
        // SECTION E: REPORTS
        // ============================

        public async Task<ServiceResult<EeuReportDto>> AddReportAsync(
            int programId,
            EeuReportCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuReportDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if report already exists
            var existing = await _reportRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<EeuReportDto>.Failure(
                    "Report already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var report = _mapper.MapToEntity(dto);
            report.EeuProgramDetailsId = programId;
            report.OrganizationId = _currentUserService.OrganizationId;
            report.UnitLocationId = program.UnitLocationId;
            report.CreatedById = _currentUserService.UserId;
            report.CreatedAt = DateTimeOffset.UtcNow;

            await _reportRepository.CreateAsync(report);

            var resultDto = _mapper.MapToDto(report);
            return ServiceResult<EeuReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuReportDto>> UpdateReportAsync(
            int reportId,
            EeuReportUpdateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult<EeuReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuReportDto>.Failure(
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
            return ServiceResult<EeuReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteReportAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult.Failure("Report not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.CONFLICT);

            await _reportRepository.DeleteAsync(reportId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<EeuReportDto>> GetReportByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var report = await _reportRepository.GetByProgramIdAsync(programId);
            if (report == null)
                return ServiceResult<EeuReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(report);
            return ServiceResult<EeuReportDto>.Success(dto);
        }

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<EeuRecommendationDto>> AddRecommendationAsync(
            int programId,
            EeuRecommendationCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if recommendation already exists
            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Recommendation already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var recommendation = _mapper.MapToEntity(dto);
            recommendation.EeuProgramDetailsId = programId;
            recommendation.OrganizationId = _currentUserService.OrganizationId;
            recommendation.UnitLocationId = program.UnitLocationId;
            recommendation.CreatedById = _currentUserService.UserId;
            recommendation.CreatedAt = DateTimeOffset.UtcNow;

            await _recommendationRepository.CreateAsync(recommendation);

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<EeuRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuRecommendationDto>> UpdateRecommendationAsync(
            int recommendationId,
            EeuRecommendationUpdateDto dto)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult<EeuRecommendationDto>.Failure(
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

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<EeuRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteRecommendationAsync(int recommendationId)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult.Failure("Recommendation not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Cannot modify approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _recommendationRepository.DeleteAsync(recommendationId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<EeuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var recommendation = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (recommendation == null)
                return ServiceResult<EeuRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(recommendation);
            return ServiceResult<EeuRecommendationDto>.Success(dto);
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

        public async Task<PaginatedResult<EeuProgramDetailsDto>> GetPaginatedAsync(
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

            return new PaginatedResult<EeuProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<EeuProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _programRepository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);
            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<EeuProgramDetailsDto>(
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
            var query = _programRepository  .GetQueryable()
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