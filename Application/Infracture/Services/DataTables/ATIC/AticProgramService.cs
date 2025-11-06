

namespace Infrastructure.Services.DataTables.ATIC
{
    public class AticProgramService : IAticProgramService
    {
        private readonly IAticProgramDetailsRepository _programRepository;
        private readonly IAticParticipantDemographicsRepository _demographicsRepository;
        private readonly IAticProgramContentRepository _contentRepository;
        private readonly IAticResourcePersonRepository _resourcePersonRepository;
        private readonly IAticTopicsCoveredRepository _topicsRepository;
        private readonly IAticTeachingAidsRepository _teachingAidsRepository;
        private readonly IAticAdvisoryServicesRepository _advisoryRepository;
        private readonly IAticReportRepository _reportRepository;
        private readonly IAticRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly AticProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public AticProgramService(
            IAticProgramDetailsRepository programRepository,
            IAticParticipantDemographicsRepository demographicsRepository,
            IAticProgramContentRepository contentRepository,
            IAticResourcePersonRepository resourcePersonRepository,
            IAticTopicsCoveredRepository topicsRepository,
            IAticTeachingAidsRepository teachingAidsRepository,
            IAticAdvisoryServicesRepository advisoryRepository,
            IAticReportRepository reportRepository,
            IAticRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            AticProgramMapper mapper,
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
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<AticProgramDetailsDto>> CreateProgramAsync(AticProgramCreateDto dto)
        {
            // Verify access to unit location
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<AticProgramDetailsDto>.Failure(
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

            return ServiceResult<AticProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<AticProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<AticProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(program);
            return ServiceResult<AticProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<AticProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<AticProgramDetailsCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<AticProgramDetailsCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<AticProgramDetailsCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<AticProgramDetailsDto>> UpdateProgramAsync(int id, AticProgramUpdateDto dto)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<AticProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticProgramDetailsDto>.Failure(
                    "Cannot edit programs that have been submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            AticProgramMapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);

            var updatedProgram = await _programRepository.GetWithDetailsAsync(id);
            var resultDto = _mapper.MapToDtoWithDetails(updatedProgram!);

            return ServiceResult<AticProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteProgramAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only draft programs can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _programRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================

        public async Task<ServiceResult<AticParticipantDemographicsDto>> AddDemographicsAsync(
            int programId,
            AticParticipantDemographicsCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var demographics = _mapper.MapToEntity(dto);
            demographics.AticProgramDetailsId = programId;
            demographics.OrganizationId = _currentUserService.OrganizationId;
            demographics.UnitLocationId = program.UnitLocationId;
            demographics.CreatedById = _currentUserService.UserId;
            demographics.CreatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.CreateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<AticParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticParticipantDemographicsDto>> UpdateDemographicsAsync(
            int demographicsId,
            AticParticipantDemographicsUpdateDto dto)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Demographics not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticParticipantDemographicsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            AticProgramMapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.UpdateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
            return ServiceResult<AticParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteDemographicsAsync(int demographicsId)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult.Failure("Demographics not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _demographicsRepository.DeleteAsync(demographicsId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<AticParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<AticParticipantDemographicsDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var demographics = await _demographicsRepository.GetByProgramIdAsync(programId);
            var dtos = demographics.Select(d => _mapper.MapToDto(d)).ToList();

            return ServiceResult<List<AticParticipantDemographicsDto>>.Success(dtos);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        public async Task<ServiceResult<AticProgramContentDto>> AddProgramContentAsync(
            int programId,
            AticProgramContentCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticProgramContentDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var content = _mapper.MapToEntity(dto);
            content.AticProgramDetailsId = programId;
            content.OrganizationId = _currentUserService.OrganizationId;
            content.UnitLocationId = program.UnitLocationId;
            content.CreatedById = _currentUserService.UserId;
            content.CreatedAt = DateTimeOffset.UtcNow;

            await _contentRepository.CreateAsync(content);

            var result = await _contentRepository.GetWithDetailsAsync(content.Id);
            var resultDto = _mapper.MapToDto(result!);

            return ServiceResult<AticProgramContentDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticProgramContentDto>> GetProgramContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetWithDetailsAsync(contentId);

            if (content == null)
                return ServiceResult<AticProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(content);
            return ServiceResult<AticProgramContentDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _contentRepository.DeleteAsync(contentId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<AticProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<AticProgramContentDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var contents = await _contentRepository.GetByProgramIdAsync(programId);
            var dtos = contents.Select(c => _mapper.MapToDto(c)).ToList();

            return ServiceResult<List<AticProgramContentDto>>.Success(dtos);
        }

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================

        public async Task<ServiceResult<AticResourcePersonDto>> AddResourcePersonAsync(
            int contentId,
            AticResourcePersonCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var person = _mapper.MapToEntity(dto);
            person.AticProgramContentAndResourcesId = contentId;
            person.OrganizationId = _currentUserService.OrganizationId;
            person.UnitLocationId = program.UnitLocationId;
            person.CreatedById = _currentUserService.UserId;
            person.CreatedAt = DateTimeOffset.UtcNow;

            await _resourcePersonRepository.CreateAsync(person);

            var resultDto = _mapper.MapToDto(person);
            return ServiceResult<AticResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticResourcePersonDto>> UpdateResourcePersonAsync(
            int personId,
            AticResourcePersonUpdateDto dto)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Resource person not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticResourcePersonDto>.Failure(
                    "Cannot modify submitted programs",
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
            return ServiceResult<AticResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteResourcePersonAsync(int personId)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult.Failure("Resource person not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _resourcePersonRepository.DeleteAsync(personId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<AticResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<AticResourcePersonDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var persons = await _resourcePersonRepository.GetByContentIdAsync(contentId);
            var dtos = persons.Select(p => _mapper.MapToDto(p)).ToList();

            return ServiceResult<List<AticResourcePersonDto>>.Success(dtos);
        }

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================

        public async Task<ServiceResult<AticTopicsCoveredDto>> AddTopicAsync(
            int contentId,
            AticTopicsCoveredCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var topic = _mapper.MapToEntity(dto);
            topic.AticProgramContentAndResourcesId = contentId;
            topic.OrganizationId = _currentUserService.OrganizationId;
            topic.UnitLocationId = program.UnitLocationId;
            topic.CreatedById = _currentUserService.UserId;
            topic.CreatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.CreateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<AticTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticTopicsCoveredDto>> UpdateTopicAsync(
            int topicId,
            AticTopicsCoveredUpdateDto dto)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Topic not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticTopicsCoveredDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.Date.HasValue) topic.Date = dto.Date;
            if (dto.Title != null) topic.Title = dto.Title;
            if (dto.PhotoUpload != null) topic.PhotoUpload = dto.PhotoUpload;

            topic.UpdatedById = _currentUserService.UserId;
            topic.UpdatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.UpdateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<AticTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTopicAsync(int topicId)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult.Failure("Topic not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _topicsRepository.DeleteAsync(topicId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<AticTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<AticTopicsCoveredDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var topics = await _topicsRepository.GetByContentIdAsync(contentId);
            var dtos = topics.Select(t => _mapper.MapToDto(t)).ToList();

            return ServiceResult<List<AticTopicsCoveredDto>>.Success(dtos);
        }

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================

        public async Task<ServiceResult<AticTeachingAidsDto>> AddTeachingAidAsync(
            int contentId,
            AticTeachingAidsCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var aid = _mapper.MapToEntity(dto);
            aid.AticProgramContentAndResourcesId = contentId;
            aid.OrganizationId = _currentUserService.OrganizationId;
            aid.UnitLocationId = program.UnitLocationId;
            aid.CreatedById = _currentUserService.UserId;
            aid.CreatedAt = DateTimeOffset.UtcNow;

            await _teachingAidsRepository.CreateAsync(aid);

            var resultDto = _mapper.MapToDto(aid);
            return ServiceResult<AticTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticTeachingAidsDto>> UpdateTeachingAidAsync(
            int aidId,
            AticTeachingAidsUpdateDto dto)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Teaching aid not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticTeachingAidsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.TypeOfAidId.HasValue) aid.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) aid.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) aid.Purpose = dto.Purpose;
            if (dto.Number.HasValue) aid.Number = dto.Number.Value;

            aid.UpdatedById = _currentUserService.UserId;
            aid.UpdatedAt = DateTimeOffset.UtcNow;

            await _teachingAidsRepository.UpdateAsync(aid);

            var resultDto = _mapper.MapToDto(aid);
            return ServiceResult<AticTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTeachingAidAsync(int aidId)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult.Failure("Teaching aid not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.AticProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.AticProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _teachingAidsRepository.DeleteAsync(aidId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<AticTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<AticTeachingAidsDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var aids = await _teachingAidsRepository.GetByContentIdAsync(contentId);
            var dtos = aids.Select(a => _mapper.MapToDto(a)).ToList();

            return ServiceResult<List<AticTeachingAidsDto>>.Success(dtos);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<AticAdvisoryServicesDto>> AddAdvisoryServicesAsync(
            int programId,
            AticAdvisoryServicesCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if advisory services already exist
            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Advisory services already exist for this program",
                    ServiceErrorStatus.INVALIDOPERATION);

            var advisory = _mapper.MapToEntity(dto);
            advisory.AticProgramDetailsId = programId;
            advisory.OrganizationId = _currentUserService.OrganizationId;
            advisory.UnitLocationId = program.UnitLocationId;
            advisory.CreatedById = _currentUserService.UserId;
            advisory.CreatedAt = DateTimeOffset.UtcNow;

            await _advisoryRepository.CreateAsync(advisory);

            var resultDto = _mapper.MapToDto(advisory);
            return ServiceResult<AticAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(
            int advisoryId,
            AticAdvisoryServicesUpdateDto dto)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Cannot modify submitted programs",
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
            return ServiceResult<AticAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult.Failure("Advisory services not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _advisoryRepository.DeleteAsync(advisoryId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AticAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var advisory = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (advisory == null)
                return ServiceResult<AticAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(advisory);
            return ServiceResult<AticAdvisoryServicesDto>.Success(dto);
        }

        // ============================
        // SECTION E: REPORTS
        // ============================

        public async Task<ServiceResult<AticReportDto>> AddReportAsync(
            int programId,
            AticReportCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticReportDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if report already exists
            var existing = await _reportRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<AticReportDto>.Failure(
                    "Report already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var report = _mapper.MapToEntity(dto);
            report.AticProgramDetailsId = programId;
            report.OrganizationId = _currentUserService.OrganizationId;
            report.UnitLocationId = program.UnitLocationId;
            report.CreatedById = _currentUserService.UserId;
            report.CreatedAt = DateTimeOffset.UtcNow;

            await _reportRepository.CreateAsync(report);

            var resultDto = _mapper.MapToDto(report);
            return ServiceResult<AticReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticReportDto>> UpdateReportAsync(
            int reportId,
            AticReportUpdateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult<AticReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticReportDto>.Failure(
                    "Cannot modify submitted programs",
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
            return ServiceResult<AticReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteReportAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult.Failure("Report not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.CONFLICT);

            await _reportRepository.DeleteAsync(reportId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AticReportDto>> GetReportByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var report = await _reportRepository.GetByProgramIdAsync(programId);
            if (report == null)
                return ServiceResult<AticReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(report);
            return ServiceResult<AticReportDto>.Success(dto);
        }

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<AticRecommendationDto>> AddRecommendationAsync(
            int programId,
            AticRecommendationCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if recommendation already exists
            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Recommendation already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var recommendation = _mapper.MapToEntity(dto);
            recommendation.AticProgramDetailsId = programId;
            recommendation.OrganizationId = _currentUserService.OrganizationId;
            recommendation.UnitLocationId = program.UnitLocationId;
            recommendation.CreatedById = _currentUserService.UserId;
            recommendation.CreatedAt = DateTimeOffset.UtcNow;

            await _recommendationRepository.CreateAsync(recommendation);

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<AticRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult<AticRecommendationDto>> UpdateRecommendationAsync(
            int recommendationId,
            AticRecommendationUpdateDto dto)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Cannot modify submitted programs",
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
            return ServiceResult<AticRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteRecommendationAsync(int recommendationId)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult.Failure("Recommendation not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.AticProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _recommendationRepository.DeleteAsync(recommendationId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AticRecommendationDto>> GetRecommendationByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var recommendation = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (recommendation == null)
                return ServiceResult<AticRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(recommendation);
            return ServiceResult<AticRecommendationDto>.Success(dto);
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

            if (program.FormStatus != "Draft")
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

        public async Task<PaginatedResult<AticProgramDetailsDto>> GetPaginatedAsync(
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

            return new PaginatedResult<AticProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<AticProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _programRepository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);
            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<AticProgramDetailsDto>(
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