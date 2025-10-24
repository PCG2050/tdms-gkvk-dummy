using Application.Interface.Services.DataTables.IBTVA;
using Application.Mapper.DataTable.IBTVA;

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
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

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
            IbtvaProgramMapper mapper,
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

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> CreateProgramAsync(IbtvaProgramCreateDto dto)
        {
            // Verify access to unit location
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
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

            return ServiceResult<IbtvaProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(program);
            return ServiceResult<IbtvaProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<IbtvaProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramDetailsCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<IbtvaProgramDetailsCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<IbtvaProgramDetailsCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<IbtvaProgramDetailsDto>> UpdateProgramAsync(int id, IbtvaProgramUpdateDto dto)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaProgramDetailsDto>.Failure(
                    "Cannot edit programs that have been submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            IbtvaProgramMapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await _programRepository.UpdateAsync(program);

            var updatedProgram = await _programRepository.GetWithDetailsAsync(id);
            var resultDto = _mapper.MapToDtoWithDetails(updatedProgram!);

            return ServiceResult<IbtvaProgramDetailsDto>.Success(resultDto);
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

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var demographics = _mapper.MapToEntity(dto);
            demographics.IbtvaProgramDetailsId = programId;
            demographics.OrganizationId = _currentUserService.OrganizationId;
            demographics.UnitLocationId = program.UnitLocationId;
            demographics.CreatedById = _currentUserService.UserId;
            demographics.CreatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.CreateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
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

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaParticipantDemographicsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            IbtvaProgramMapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            await _demographicsRepository.UpdateAsync(demographics);

            var resultDto = _mapper.MapToDto(demographics);
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

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

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

        public async Task<ServiceResult<IbtvaProgramContentDto>> AddProgramContentAsync(
            int programId,
            IbtvaProgramContentCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaProgramContentDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var content = _mapper.MapToEntity(dto);
            content.IbtvaProgramDetailsId = programId;
            content.OrganizationId = _currentUserService.OrganizationId;
            content.UnitLocationId = program.UnitLocationId;
            content.CreatedById = _currentUserService.UserId;
            content.CreatedAt = DateTimeOffset.UtcNow;

            await _contentRepository.CreateAsync(content);

            var result = await _contentRepository.GetWithDetailsAsync(content.Id);
            var resultDto = _mapper.MapToDto(result!);

            return ServiceResult<IbtvaProgramContentDto>.Success(resultDto);
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

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

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
        // SECTION C1: RESOURCE PERSONS
        // ============================

        public async Task<ServiceResult<IbtvaResourcePersonDto>> AddResourcePersonAsync(
            int contentId,
            IbtvaResourcePersonCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var person = _mapper.MapToEntity(dto);
            person.IbtvaProgramContentAndResourcesId = contentId;
            person.OrganizationId = _currentUserService.OrganizationId;
            person.UnitLocationId = program.UnitLocationId;
            person.CreatedById = _currentUserService.UserId;
            person.CreatedAt = DateTimeOffset.UtcNow;

            await _resourcePersonRepository.CreateAsync(person);

            var resultDto = _mapper.MapToDto(person);
            return ServiceResult<IbtvaResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaResourcePersonDto>> UpdateResourcePersonAsync(
            int personId,
            IbtvaResourcePersonUpdateDto dto)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
                    "Resource person not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaResourcePersonDto>.Failure(
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
            return ServiceResult<IbtvaResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteResourcePersonAsync(int personId)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult.Failure("Resource person not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _resourcePersonRepository.DeleteAsync(personId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<IbtvaResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<IbtvaResourcePersonDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var persons = await _resourcePersonRepository.GetByContentIdAsync(contentId);
            var dtos = persons.Select(p => _mapper.MapToDto(p)).ToList();

            return ServiceResult<List<IbtvaResourcePersonDto>>.Success(dtos);
        }

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================

        public async Task<ServiceResult<IbtvaTopicsCoveredDto>> AddTopicAsync(
            int contentId,
            IbtvaTopicsCoveredCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var topic = _mapper.MapToEntity(dto);
            topic.IbtvaProgramContentAndResourcesId = contentId;
            topic.OrganizationId = _currentUserService.OrganizationId;
            topic.UnitLocationId = program.UnitLocationId;
            topic.CreatedById = _currentUserService.UserId;
            topic.CreatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.CreateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<IbtvaTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaTopicsCoveredDto>> UpdateTopicAsync(
            int topicId,
            IbtvaTopicsCoveredUpdateDto dto)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Topic not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaTopicsCoveredDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (dto.Date.HasValue) topic.Date = dto.Date;
            if (dto.Title != null) topic.Title = dto.Title;
            if (dto.PhotoUpload != null) topic.PhotoUpload = dto.PhotoUpload;

            topic.UpdatedById = _currentUserService.UserId;
            topic.UpdatedAt = DateTimeOffset.UtcNow;

            await _topicsRepository.UpdateAsync(topic);

            var resultDto = _mapper.MapToDto(topic);
            return ServiceResult<IbtvaTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTopicAsync(int topicId)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult.Failure("Topic not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _topicsRepository.DeleteAsync(topicId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<IbtvaTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<IbtvaTopicsCoveredDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var topics = await _topicsRepository.GetByContentIdAsync(contentId);
            var dtos = topics.Select(t => _mapper.MapToDto(t)).ToList();

            return ServiceResult<List<IbtvaTopicsCoveredDto>>.Success(dtos);
        }

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================

        public async Task<ServiceResult<IbtvaTeachingAidsDto>> AddTeachingAidAsync(
            int contentId,
            IbtvaTeachingAidsCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var aid = _mapper.MapToEntity(dto);
            aid.IbtvaProgramContentAndResourcesId = contentId;
            aid.OrganizationId = _currentUserService.OrganizationId;
            aid.UnitLocationId = program.UnitLocationId;
            aid.CreatedById = _currentUserService.UserId;
            aid.CreatedAt = DateTimeOffset.UtcNow;

            await _teachingAidsRepository.CreateAsync(aid);

            var resultDto = _mapper.MapToDto(aid);
            return ServiceResult<IbtvaTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaTeachingAidsDto>> UpdateTeachingAidAsync(
            int aidId,
            IbtvaTeachingAidsUpdateDto dto)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
                    "Teaching aid not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaTeachingAidsDto>.Failure(
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
            return ServiceResult<IbtvaTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTeachingAidAsync(int aidId)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult.Failure("Teaching aid not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.IbtvaProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.IbtvaProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _teachingAidsRepository.DeleteAsync(aidId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<IbtvaTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<IbtvaTeachingAidsDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var aids = await _teachingAidsRepository.GetByContentIdAsync(contentId);
            var dtos = aids.Select(a => _mapper.MapToDto(a)).ToList();

            return ServiceResult<List<IbtvaTeachingAidsDto>>.Success(dtos);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<IbtvaAdvisoryServicesDto>> AddAdvisoryServicesAsync(
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

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if advisory services already exist
            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Advisory services already exist for this program",
                    ServiceErrorStatus.INVALIDOPERATION);

            var advisory = _mapper.MapToEntity(dto);
            advisory.IbtvaProgramDetailsId = programId;
            advisory.OrganizationId = _currentUserService.OrganizationId;
            advisory.UnitLocationId = program.UnitLocationId;
            advisory.CreatedById = _currentUserService.UserId;
            advisory.CreatedAt = DateTimeOffset.UtcNow;

            await _advisoryRepository.CreateAsync(advisory);

            var resultDto = _mapper.MapToDto(advisory);
            return ServiceResult<IbtvaAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(
            int advisoryId,
            IbtvaAdvisoryServicesUpdateDto dto)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaAdvisoryServicesDto>.Failure(
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
            return ServiceResult<IbtvaAdvisoryServicesDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId)
        {
            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
                return ServiceResult.Failure("Advisory services not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(advisory.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _advisoryRepository.DeleteAsync(advisoryId);
            return ServiceResult.Success();
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
        // SECTION E: REPORTS
        // ============================

        public async Task<ServiceResult<IbtvaReportDto>> AddReportAsync(
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

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if report already exists
            var existing = await _reportRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Report already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var report = _mapper.MapToEntity(dto);
            report.IbtvaProgramDetailsId = programId;
            report.OrganizationId = _currentUserService.OrganizationId;
            report.UnitLocationId = program.UnitLocationId;
            report.CreatedById = _currentUserService.UserId;
            report.CreatedAt = DateTimeOffset.UtcNow;

            await _reportRepository.CreateAsync(report);

            var resultDto = _mapper.MapToDto(report);
            return ServiceResult<IbtvaReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaReportDto>> UpdateReportAsync(
            int reportId,
            IbtvaReportUpdateDto dto)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaReportDto>.Failure(
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
            return ServiceResult<IbtvaReportDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteReportAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);

            if (report == null)
                return ServiceResult.Failure("Report not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(report.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.CONFLICT);

            await _reportRepository.DeleteAsync(reportId);
            return ServiceResult.Success();
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
        // SECTION F: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<IbtvaRecommendationDto>> AddRecommendationAsync(
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

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Check if recommendation already exists
            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);
            if (existing != null)
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Recommendation already exists for this program",
                    ServiceErrorStatus.CONFLICT);

            var recommendation = _mapper.MapToEntity(dto);
            recommendation.IbtvaProgramDetailsId = programId;
            recommendation.OrganizationId = _currentUserService.OrganizationId;
            recommendation.UnitLocationId = program.UnitLocationId;
            recommendation.CreatedById = _currentUserService.UserId;
            recommendation.CreatedAt = DateTimeOffset.UtcNow;

            await _recommendationRepository.CreateAsync(recommendation);

            var resultDto = _mapper.MapToDto(recommendation);
            return ServiceResult<IbtvaRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult<IbtvaRecommendationDto>> UpdateRecommendationAsync(
            int recommendationId,
            IbtvaRecommendationUpdateDto dto)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<IbtvaRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult<IbtvaRecommendationDto>.Failure(
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
            return ServiceResult<IbtvaRecommendationDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteRecommendationAsync(int recommendationId)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);

            if (recommendation == null)
                return ServiceResult.Failure("Recommendation not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(recommendation.IbtvaProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot modify submitted programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _recommendationRepository.DeleteAsync(recommendationId);
            return ServiceResult.Success();
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

        public async Task<PaginatedResult<IbtvaProgramDetailsDto>> GetPaginatedAsync(
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

            return new PaginatedResult<IbtvaProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<IbtvaProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _programRepository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);
            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<IbtvaProgramDetailsDto>(
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