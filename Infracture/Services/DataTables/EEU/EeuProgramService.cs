// Infrastructure/Services/DataTables/EEU/EeuProgramService.cs
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Services.DataTables.EEU;
using Application.Mapper.DataTable.EEU;
using Application.Models.DataTables.EEU;
using Application.Services.Common;
using Infrastructure.Repository;

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
        private readonly IEeuResultRepository _resultRepository;
        private readonly IEeuFldResultRepository _fldResultRepository;
        private readonly IEeuOftResultRepository _oftResultRepository;
        private readonly IEeuReportRepository _reportRepository;
        private readonly IEeuRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly EeuProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly GenericTrainerHistoryService<EeuProgramDetails> _historyService;

        private const int FLD_CATEGORY_ID = 18;
        private const int OFT_CATEGORY_ID = 24;
        private const int EEU_UNIT_ID = 9;

        public EeuProgramService(
            IEeuProgramDetailsRepository programRepository,
            IEeuParticipantDemographicsRepository demographicsRepository,
            IEeuProgramContentRepository contentRepository,
            IEeuResourcePersonRepository resourcePersonRepository,
            IEeuTopicsCoveredRepository topicsRepository,
            IEeuTeachingAidsRepository teachingAidsRepository,
            IEeuAdvisoryServicesRepository advisoryRepository,
            IEeuResultRepository resultRepository,
            IEeuFldResultRepository fldResultRepository,
            IEeuOftResultRepository oftResultRepository,
            IEeuReportRepository reportRepository,
            IEeuRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IUserRepository userRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            EeuProgramMapper mapper)
        {
            _programRepository = programRepository;
            _demographicsRepository = demographicsRepository;
            _contentRepository = contentRepository;
            _resourcePersonRepository = resourcePersonRepository;
            _topicsRepository = topicsRepository;
            _teachingAidsRepository = teachingAidsRepository;
            _advisoryRepository = advisoryRepository;
            _resultRepository = resultRepository;
            _fldResultRepository = fldResultRepository;
            _oftResultRepository = oftResultRepository;
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
            _historyService = new GenericTrainerHistoryService<EeuProgramDetails>(currentUserService, trainerAssignmentRepository, organizationUnitRepository, unitHeadAssignmentRepository, userRepository);
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<EeuProgramDetailsDto>> CreateProgramAsync(EeuProgramCreateDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.FormStatus = "Draft";
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _programRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<EeuProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<EeuProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(program);
            return ServiceResult<EeuProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<EeuProgramCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<EeuProgramCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<EeuProgramCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<EeuProgramDetailsDto>> UpdateProgramAsync(int id, EeuProgramUpdateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuProgramDetailsDto>.Failure(
            //        "Cannot modify programs that are not in Draft or Rejected status",
            //        ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, program);

            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;
            program.FormStatus = "Draft"; // Reset to Draft on update
            var updated = await _programRepository.UpdateAsync(program);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<EeuProgramDetailsDto>.Success(resultDto);
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

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuParticipantDemographicsDto>.Failure(
            //        "Cannot add demographics to approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.EeuProgramDetailsId = programId;
            entity.UnitLocationId = program.UnitLocationId;
            entity.OrganizationId = program.OrganizationId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _demographicsRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

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

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuParticipantDemographicsDto>.Failure(
            //        "Cannot modify demographics for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UnitLocationId = program.UnitLocationId;
            demographics.OrganizationId = program.OrganizationId;
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _demographicsRepository.UpdateAsync(demographics);
            var resultDto = _mapper.MapToDto(updated);

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

            //if (program.FormStatus != "Draft")
            //    return ServiceResult.Failure(
            //        "Cannot delete demographics from approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

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
            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuProgramContentDto>.Failure(
            //        "Cannot add content to approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

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

                var fieldDays = dto.FieldDays?.Select(fd =>
                {
                    var entity = _mapper.MapToEntity(fd);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var fieldVisits = dto.FieldVisits?.Select(fv =>
                {
                    var entity = _mapper.MapToEntity(fv);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var farmerScientistInteractions = dto.FarmerScientistInteractions?.Select(fsi =>
                {
                    var entity = _mapper.MapToEntity(fsi);
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
                    teachingAids,
                    fieldVisits,
                    fieldDays,
                    farmerScientistInteractions);

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

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuProgramContentDto>.Failure(
            //        "Cannot update content in approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

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
                        ResourceTypeId = rp.ResourceTypeId,
                        ResponsibilityId = rp.ResponsibilityId,
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

                var fieldVisits = dto.FieldVisits?.Select(fv =>
                {
                    var entity = new EeuFieldVisit
                    {
                        Id = fv.Id ?? 0,
                        Date = fv.Date,
                        ScientistOfficerVisitedName = fv.ScientistOfficerVisitedName,
                        Purpose = fv.Purpose,
                        NoOfFieldsCovered = fv.NoOfFieldsCovered,
                        NoOfFarmerCovered = fv.NoOfFarmerCovered,
                        PhotoUpload = fv.PhotoUpload
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

                var fieldDays = dto.FieldDays?.Select(fd =>
                {
                    var entity = new EeuFieldDay
                    {
                        Id = fd.Id ?? 0
                        // Add other EeuFieldDay fields here based on your DTO
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

                var farmerScientistInteractions = dto.FarmerScientistInteractions?.Select(fsi =>
                {
                    var entity = new EeuFarmerScientistInteraction
                    {
                        Id = fsi.Id ?? 0,
                        Date = fsi.Date,
                        ScientistOfficerName = fsi.ScientistOfficerName,
                        TopicDiscussed = fsi.TopicDiscussed,
                        NoOfFarmersParticipated = fsi.NoOfFarmersParticipated,
                        PhotoUpload = fsi.PhotoUpload
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
                    teachingAids,
                    fieldVisits,
                    fieldDays,
                    farmerScientistInteractions);

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

        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.EeuProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            //if (program.FormStatus != "Draft")
            //    return ServiceResult.Failure(
            //        "Cannot delete content from approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

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
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<EeuAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(
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

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuAdvisoryServicesDto>.Failure(
            //        "Cannot modify advisory services for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.EeuProgramDetailsId = programId;
                entity.UnitLocationId = program.UnitLocationId;
                entity.OrganizationId = program.OrganizationId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _advisoryRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new EeuAdvisoryServicesUpdateDto
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
                existing.UnitLocationId = program.UnitLocationId;
                existing.OrganizationId = program.OrganizationId;
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _advisoryRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
            }
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
        public async Task<ServiceResult<EeuAdvisoryServicesDto>> AddAdvisoryServicesWithChildrenAsync(
           int programId,
           EeuAdvisoryServicesHybridCreateDto dto)
        {
            // Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            try
            {
                // Prepare parent entity
                var parentEntity = new EeuAdvisoryServices
                {
                    EeuProgramDetailsId = programId,
                    NoOfFacebookSMS = dto.NoOfFacebookSMS ?? 0,
                    NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers ?? 0,
                    NoOfWhatsappGroups = dto.NoOfWhatsappGroups ?? 0,
                    NoOfWhatsappSMS = dto.NoOfWhatsappSMS ?? 0,
                    NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries ?? 0,
                    NoOfPhoneCalls = dto.NoOfPhoneCalls ?? 0,
                    NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions ?? 0,
                    NoOfGroupDiscussions = dto.NoOfGroupDiscussions ?? 0,
                    NoOfEmailsSent = dto.NoOfEmailsSent ?? 0,
                    NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage ?? 0,
                    NoOfBeneficiaries = dto.NoOfBeneficiaries ?? 0,
                    UnitLocationId = program.UnitLocationId,
                    OrganizationId = program.OrganizationId,
                    CreatedById = _currentUserService.UserId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities
                var criticalInputs = dto.CriticalInputsDistributed?.Select(ci =>
                {
                    var entity = _mapper.MapToEntity(ci);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Repository handles transaction internally
                var createdAdvisory = await _advisoryRepository.CreateWithChildrenAsync(
                    parentEntity,
                    criticalInputs);

                var resultDto = _mapper.MapToDto(createdAdvisory);
                return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    $"Error creating advisory services: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        public async Task<ServiceResult<EeuAdvisoryServicesDto>> UpdateAdvisoryServicesWithChildrenAsync(
            int advisoryServicesId,
            EeuAdvisoryServicesHybridUpdateDto dto)
        {
            // Validate advisory services exists
            var advisory = await _advisoryRepository.GetWithDetailsAsync(advisoryServicesId);
            if (advisory == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            // Validate program and permissions
            var program = await _programRepository.GetByIdAsync(advisory.EeuProgramDetailsId ?? 0);
            if (program == null)
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new EeuAdvisoryServices
                {
                    Id = advisoryServicesId,
                    NoOfFacebookSMS = dto.NoOfFacebookSMS ?? 0,
                    NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers ?? 0,
                    NoOfWhatsappGroups = dto.NoOfWhatsappGroups ?? 0,
                    NoOfWhatsappSMS = dto.NoOfWhatsappSMS ?? 0,
                    NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries ?? 0,
                    NoOfPhoneCalls = dto.NoOfPhoneCalls ?? 0,
                    NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions ?? 0,
                    NoOfGroupDiscussions = dto.NoOfGroupDiscussions ?? 0,
                    NoOfEmailsSent = dto.NoOfEmailsSent ?? 0,
                    NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage ?? 0,
                    NoOfBeneficiaries = dto.NoOfBeneficiaries ?? 0,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities (hybrid: mix of new and existing)
                var criticalInputs = dto.CriticalInputsDistributed?.Select(ci =>
                {
                    var entity = new EeuCriticalInputsDistributed
                    {
                        Id = ci.Id ?? 0, // 0 means new
                        InputName = ci.InputName,
                        QuantityDistributed = ci.QuantityDistributed,
                        NoOfRecipients = ci.NoOfRecipients,
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

                // Repository handles transaction and hybrid pattern internally
                var updatedAdvisory = await _advisoryRepository.UpdateWithChildrenAsync(
                    parentEntity,
                    criticalInputs);

                var resultDto = _mapper.MapToDto(updatedAdvisory);
                return ServiceResult<EeuAdvisoryServicesDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuAdvisoryServicesDto>.Failure(
                    $"Error updating advisory services: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24 ONLY)
        // ============================

        public async Task<ServiceResult<EeuResultDto>> GetResultByIdAsync(int resultId)
        {
            var result = await _resultRepository.GetWithDetailsAsync(resultId);

            if (result == null)
                return ServiceResult<EeuResultDto>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.EeuProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanViewForm(program))
                return ServiceResult<EeuResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(result);
            return ServiceResult<EeuResultDto>.Success(dto);
        }

        public async Task<ServiceResult> UpdateResultExcelAsync(int resultId, string excelUrl)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult.Failure("Result not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.EeuProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult.Failure(
            //        "Cannot modify results for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            result.UploadExcelUrl = excelUrl;
            result.UpdatedById = _currentUserService.UserId;
            result.UpdatedAt = DateTimeOffset.UtcNow;

            await _resultRepository.UpdateAsync(result);
            return ServiceResult.Success();
        }

        // ============================
        // E3: COMPOSITE CREATE/UPDATE FOR RESULTS WITH CHILDREN
        // ============================

        public async Task<ServiceResult<EeuResultDto>> CreateResultWithChildrenAsync(
            int programId,
            EeuResultWithChildrenCreateDto dto)
        {
            // Step 1: Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<EeuResultDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuResultDto>.Failure(
            //        "Cannot add results to approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            // Step 4: Check if result already exists for this program
            var existingResult = await _resultRepository.GetByProgramIdAsync(programId);
            if (existingResult != null)
                return ServiceResult<EeuResultDto>.Failure(
                    "Result record already exists for this program. Use update instead.",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 5: Prepare parent entity
                var parentEntity = new EeuResult
                {
                    EeuProgramDetailsId = programId,
                    UploadExcelUrl = dto.UploadExcelUrl,
                    UnitLocationId = program.UnitLocationId,
                    OrganizationId = program.OrganizationId,
                    CreatedById = _currentUserService.UserId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                // Step 6: Prepare FldResults child entities
                var fldResultEntities = dto.FldResults?.Select(fld =>
                {
                    var entity = _mapper.MapToEntity(fld);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Step 7: Prepare OftResults child entities
                var oftResultEntities = dto.OftResults?.Select(oft =>
                {
                    var entity = _mapper.MapToEntity(oft);
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Step 8: Repository handles transaction - creates parent, gets ID, creates children
                var createdResult = await _resultRepository.CreateWithChildrenAsync(
                    parentEntity,
                    fldResultEntities,
                    oftResultEntities);

                // Step 9: Map to DTO and return
                var resultDto = _mapper.MapToDto(createdResult);
                return ServiceResult<EeuResultDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuResultDto>.Failure(
                    $"Failed to create result with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        public async Task<ServiceResult<EeuResultDto>> UpdateResultWithChildrenAsync(
            int resultId,
            EeuResultWithChildrenUpdateDto dto)
        {
            // Step 1: Get existing result
            var existingResult = await _resultRepository.GetByIdAsync(resultId);
            if (existingResult == null)
                return ServiceResult<EeuResultDto>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Get program and check permissions
            var program = await _programRepository.GetByIdAsync(existingResult.EeuProgramDetailsId);
            if (program == null)
                return ServiceResult<EeuResultDto>.Failure(
                    "Associated program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<EeuResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuResultDto>.Failure(
            //        "Cannot modify results for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 4: Prepare parent entity for update
                var parentEntity = new EeuResult
                {
                    Id = resultId,
                    EeuProgramDetailsId = existingResult.EeuProgramDetailsId,
                    UploadExcelUrl = dto.UploadExcelUrl,
                    UnitLocationId = existingResult.UnitLocationId,
                    OrganizationId = existingResult.OrganizationId,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Step 5: Prepare FldResults - Hybrid Pattern (items with Id will be updated, without will be created)
                var fldResultEntities = dto.FldResults?.Select(fld =>
                {
                    var entity = new EeuFldResult
                    {
                        Id = fld.Id ?? 0,  // 0 means create new, > 0 means update existing
                        DetailsOfDemoId = fld.DetailsOfDemoId,
                        FldNumber = fld.FldNumber,
                        Parameter1 = fld.Parameter1,
                        Observation1 = fld.Observation1,
                        Parameter2 = fld.Parameter2,
                        Observation2 = fld.Observation2,
                        Parameter3 = fld.Parameter3,
                        Observation3 = fld.Observation3,
                        Parameter4 = fld.Parameter4,
                        Observation4 = fld.Observation4,
                        Parameter5 = fld.Parameter5,
                        Observation5 = fld.Observation5,
                        Yield = fld.Yield,
                        GrossCost = fld.GrossCost,
                        GrossReturns = fld.GrossReturns,
                        NetReturns = fld.NetReturns,
                        BC = fld.BC,
                        UnitLocationId = existingResult.UnitLocationId,
                        OrganizationId = existingResult.OrganizationId
                    };

                    if (entity.Id > 0)
                    {
                        // Update: set update audit fields
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        // Create: set create audit fields
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Step 6: Prepare OftResults - Hybrid Pattern
                var oftResultEntities = dto.OftResults?.Select(oft =>
                {
                    var entity = new EeuOftResult
                    {
                        Id = oft.Id ?? 0,
                        DetailsOfDemoId = oft.DetailsOfDemoId,
                        Parameter1 = oft.Parameter1,
                        Observation1 = oft.Observation1,
                        Parameter2 = oft.Parameter2,
                        Observation2 = oft.Observation2,
                        Parameter3 = oft.Parameter3,
                        Observation3 = oft.Observation3,
                        Parameter4 = oft.Parameter4,
                        Observation4 = oft.Observation4,
                        Parameter5 = oft.Parameter5,
                        Observation5 = oft.Observation5,
                        Yield = oft.Yield,
                        GrossCost = oft.GrossCost,
                        GrossReturns = oft.GrossReturns,
                        NetReturns = oft.NetReturns,
                        BC = oft.BC
                    };

                    if (entity.Id > 0)
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Step 7: Repository handles transaction - updates parent, creates/updates/deletes children
                var updatedResult = await _resultRepository.UpdateWithChildrenAsync(
                    parentEntity,
                    fldResultEntities,
                    oftResultEntities);

                // Step 8: Map to DTO and return
                var resultDto = _mapper.MapToDto(updatedResult);
                return ServiceResult<EeuResultDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<EeuResultDto>.Failure(
                    $"Failed to update result with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================

        public async Task<ServiceResult<EeuReportDto>> AddOrUpdateReportAsync(
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

            // Check if category allows Reports (not FLD/OFT)
            if (program.CategoryId == FLD_CATEGORY_ID || program.CategoryId == OFT_CATEGORY_ID)
                return ServiceResult<EeuReportDto>.Failure(
                    $"Reports are not available for FLD/OFT categories (CategoryId {FLD_CATEGORY_ID} or {OFT_CATEGORY_ID}). Use Results endpoint instead.",
                    ServiceErrorStatus.INVALIDOPERATION);

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuReportDto>.Failure(
            //        "Cannot modify reports for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _reportRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.EeuProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _reportRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<EeuReportDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new EeuReportUpdateDto
                {
                    Id = existing.Id,
                    //ReportingYear = dto.ReportingYear,
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

                return ServiceResult<EeuReportDto>.Success(resultDto);
            }
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
        // SECTION G: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<EeuRecommendationDto>> AddOrUpdateRecommendationAsync(
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

            //if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
            //    return ServiceResult<EeuRecommendationDto>.Failure(
            //        "Cannot modify recommendations for approved programs",
            //        ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.EeuProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _recommendationRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
                if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
                {
                    program.FormStatus = "Pending";
                    program.UpdatedById = _currentUserService.UserId;
                    program.UpdatedAt = DateTimeOffset.UtcNow;
                    await _programRepository.UpdateAsync(program);
                }

                return ServiceResult<EeuRecommendationDto>.Success(resultDto);
            }
            else
            {
                // Update existing using mapper
                var updateDto = new EeuRecommendationUpdateDto
                {
                    Id = existing.Id,
                    ProblemsIdentified = dto.ProblemsIdentified,
                    Recommendation = dto.Recommendation,
                    ActionTaken = dto.ActionTaken,
                    SignificantAchievement = dto.SignificantAchievement,
                    SuccessStories = dto.SuccessStories,
                    ImpactOutcome = dto.ImpactOutcome,
                    UploadVideoUrl = dto.UploadVideoUrl
                };

                _mapper.MapUpdateDtoToEntity(updateDto, existing);
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _recommendationRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                // AUTO-SUBMIT: Since Recommendation is the last section, automatically change status to Pending
                //if (program.FormStatus == "Draft" || program.FormStatus == "Rejected")
                //{
                program.FormStatus = "Pending";
                program.UpdatedById = _currentUserService.UserId;
                program.UpdatedAt = DateTimeOffset.UtcNow;
                await _programRepository.UpdateAsync(program);
                //}

                return ServiceResult<EeuRecommendationDto>.Success(resultDto);
            }
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

        public async Task<PaginatedResult<EeuProgramListItemDto>> GetPaginatedAsync(
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
                .Select(x => new EeuProgramListItemDto
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

            return new PaginatedResult<EeuProgramListItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<EeuProgramListItemDto>> GetByStatusAsync(
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
                .Select(x => new EeuProgramListItemDto
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

            return new PaginatedResult<EeuProgramListItemDto>
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