//using Application.Interface;
//using Application.Interface.Repository;
//using Domain.Entities.Enum;
//using Domain.Entities.IBTVA;
//using Infrastructure.DbContext;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApi.Controllers.DataEntry;

//namespace WebApi.Controllers.DataEntry
//{
//    /// <summary>
//    /// Controller for IBTVA (Institute of Baking Technology and Value Addition)
//    /// 
//    /// PHASE WORKFLOW:
//    /// - Phase 1 (Program Details): REQUIRED - Must be completed first
//    /// - Phases 2-5: OPTIONAL - Use "Save & Next" to proceed or skip
//    /// - Phase 6 (Recommendations): FINAL - Only phase with "Submit" button
//    /// 
//    /// BUTTON BEHAVIOR:
//    /// - Phases 1-5: "Save & Next" button only → Auto-navigates to next phase
//    /// - Phase 6: "Save Draft" OR "Submit for Approval"
//    /// 
//    /// STATUS FLOW (FormStatus):
//    /// - Saved: Draft state, can edit all phases
//    /// - Pending: Submitted for Unit Head approval, cannot edit
//    /// - Approved: Unit Head approved, cannot edit
//    /// - Rejected: Unit Head rejected, can edit and resubmit
//    /// </summary>
//    [ApiController]
//    [Route("api/units/ibtva")]
//    [Authorize(Roles = RoleString.Trainer)]
//    public class IBTVAController : ControllerBase
//    {
//        private readonly TdmsDbContext _context;
//        private readonly ICurrentUserService _currentUser;
//        private readonly ITrainerAssignmentRepository _trainerAssignment;
//        private const int IBTVA_UNIT_ID = 4;

//        public IBTVAController(
//            TdmsDbContext context,
//            ICurrentUserService currentUser,
//            ITrainerAssignmentRepository trainerAssignment)
//        {
//            _context = context;
//            _currentUser = currentUser;
//            _trainerAssignment = trainerAssignment;
//        }

//        #region Helper Methods

//        private async Task<bool> ValidateTrainerAccess(int unitLocationId)
//        {
//            var assignments = await _trainerAssignment.GetByTrainerIdAsync(_currentUser.UserId);
//            return assignments.Any(a => a.UnitLocationId == unitLocationId);
//        }

//        private async Task<bool> ValidateIBTVAUnit(int unitLocationId)
//        {
//            var unitLocation = await _context.OrganizationUnitLocations
//                .FirstOrDefaultAsync(ul => ul.Id == unitLocationId && ul.UnitId == IBTVA_UNIT_ID);
//            return unitLocation != null;
//        }

//        #endregion

//        #region Program History & Status

//        /// <summary>
//        /// Get program content for editing (Phase 3)
//        /// </summary>
//        [HttpGet("programs/{programId}/phase3")]
//        public async Task<IActionResult> GetContentPhase3(int programId)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            var content = await _context.IbtvaProgramContentAndResources
//                .Where(c => c.IbtvaProgramDetailsId == programId)
//                .Select(c => new { c.Title, c.Description })
//                .FirstOrDefaultAsync();

//            var resourcePersons = await _context.IbtvaResourcePersons
//                .Where(r => r.IbtvaProgramContentAndResourcesId == programId)
//                .Select(r => new { r.Id, r.Name, r.Designation, r.Affiliation })
//                .ToListAsync();

//            var topics = await _context.IbtvaTopicsCoveredInClass
//                .Where(t => t.IbtvaProgramContentAndResourcesId == programId)
//                .Select(t => new { t.Id, t.TopicName, t.Duration })
//                .ToListAsync();

//            var teachingAids = await _context.IbtvaTeachingAidsDeveloped
//                .Where(a => a.IbtvaProgramDetailsId == programId)
//                .Select(a => new { a.Id, a.AidName, a.Description })
//                .ToListAsync();

//            return Ok(new
//            {
//                content,
//                resourcePersons,
//                topics,
//                teachingAids
//            });
//        }

//        #endregion

//        #region Phase 4: Advisory Services

//        /// <summary>
//        /// Save advisory services (Phase 4)
//        /// </summary>
//        [HttpPost("programs/{programId}/phase4")]
//        public async Task<IActionResult> SaveAdvisoryServicesPhase4(int programId, [FromBody] AdvisoryServicesDto dto)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            if (program.FormStatus == "Approved")
//                return BadRequest("Cannot edit approved programs");

//            // Remove existing advisory services
//            var existing = await _context.IbtvaAdvisoryServices
//                .Where(a => a.IbtvaProgramDetailsId == programId)
//                .ToListAsync();
//            _context.IbtvaAdvisoryServices.RemoveRange(existing);

//            // Add new advisory service
//            if (dto != null)
//            {
//                var advisory = new IbtvaAdvisoryServices
//                {
//                    IbtvaProgramDetailsId = programId,
//                    NoOfBeneficiaries = dto.NoOfBeneficiaries,
//                    NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions,
//                    NoOfGroupDiscussions = dto.NoOfGroupDiscussions,
//                    NoOfPhoneCalls = dto.NoOfPhoneCalls,
//                    NoOfWhatsappSMS = dto.NoOfWhatsappSMS,
//                    CreatedById = _currentUser.UserId,
//                    CreatedAt = DateTimeOffset.UtcNow
//                };
//                _context.IbtvaAdvisoryServices.Add(advisory);
//            }

//            // Update phase progress
//            if (program.CurrentPhase < 4)
//                program.CurrentPhase = 4;

//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                message = "Phase 4 saved successfully.",
//                nextPhase = 5,
//                currentPhase = program.CurrentPhase
//            });
//        }

//        /// <summary>
//        /// Get advisory services for editing (Phase 4)
//        /// </summary>
//        [HttpGet("programs/{programId}/phase4")]
//        public async Task<IActionResult> GetAdvisoryServicesPhase4(int programId)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            var advisory = await _context.IbtvaAdvisoryServices
//                .Where(a => a.IbtvaProgramDetailsId == programId)
//                .Select(a => new
//                {
//                    a.NoOfBeneficiaries,
//                    a.NoOfFaceToFaceDiscussions,
//                    a.NoOfGroupDiscussions,
//                    a.NoOfPhoneCalls,
//                    a.NoOfWhatsappSMS
//                })
//                .FirstOrDefaultAsync();

//            return Ok(advisory ?? new { });
//        }

//        #endregion

//        #region Phase 5: Reports

//        /// <summary>
//        /// Save reports (Phase 5)
//        /// </summary>
//        [HttpPost("programs/{programId}/phase5")]
//        public async Task<IActionResult> SaveReportsPhase5(int programId, [FromBody] List<ReportDto> reports)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            if (program.FormStatus == "Approved")
//                return BadRequest("Cannot edit approved programs");

//            // Remove existing reports
//            var existing = await _context.IbtvaReports
//                .Where(r => r.IbtvaProgramDetailsId == programId)
//                .ToListAsync();
//            _context.IbtvaReports.RemoveRange(existing);

//            // Add new reports
//            if (reports != null && reports.Any())
//            {
//                foreach (var reportDto in reports)
//                {
//                    var report = new IbtvaReport
//                    {
//                        IbtvaProgramDetailsId = programId,
//                        ProgressReportReportingYear = reportDto.ReportTitle,
//                        Date = reportDto.ReportDate,
//                        UploadPhoto = reportDto.ReportFileUrl,
//                        PhotosGeotaggedPhotoOrUploadPhoto = reportDto.ReportFileUrl,
//                        UploadVideo = reportDto.ReportFileUrl,
//                        SignificantOutcome = reportDto.ReportTitle,
//                        CreatedById = _currentUser.UserId,
//                        CreatedAt = DateTimeOffset.UtcNow
//                    };
//                    _context.IbtvaReports.Add(report);
//                }
//            }

//            // Update phase progress
//            if (program.CurrentPhase < 5)
//                program.CurrentPhase = 5;

//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                message = "Phase 5 saved successfully.",
//                nextPhase = 6,
//                currentPhase = program.CurrentPhase
//            });
//        }

//        /// <summary>
//        /// Get reports for editing (Phase 5)
//        /// </summary>
//        [HttpGet("programs/{programId}/phase5")]
//        public async Task<IActionResult> GetReportsPhase5(int programId)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            var reports = await _context.IbtvaReports
//                .Where(r => r.IbtvaProgramDetailsId == programId)
//                .Select(r => new
//                {
//                    r.Id,
//                    ReportTitle = r.ProgressReportReportingYear,
//                    ReportDate = r.Date,
//                    ReportFileUrl = r.UploadPhoto
//                })
//                .ToListAsync();

//            return Ok(reports);
//        }

//        #endregion

//        #region Phase 6: Recommendations & Final Submit

//        /// <summary>
//        /// Save recommendations (Phase 6) and optionally submit for approval
//        /// This is the ONLY phase where submission is available
//        /// </summary>
//        [HttpPost("programs/{programId}/phase6")]
//        public async Task<IActionResult> SaveRecommendationsPhase6(int programId, [FromBody] RecommendationsDto dto)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            if (program.FormStatus == "Approved")
//                return BadRequest("Cannot edit approved programs");

//            // Remove existing recommendations
//            var existing = await _context.IbtvaRecommendations
//                .Where(r => r.IbtvaProgramDetailsId == programId)
//                .ToListAsync();
//            _context.IbtvaRecommendations.RemoveRange(existing);

//            // Add new recommendations
//            if (dto.Recommendations != null && dto.Recommendations.Any())
//            {
//                foreach (var recDto in dto.Recommendations)
//                {
//                    var recommendation = new IbtvaRecommendation
//                    {
//                        IbtvaProgramDetailsId = programId,
//                        Recommendation = recDto.RecommendationText,
//                        CreatedById = _currentUser.UserId,
//                        CreatedAt = DateTimeOffset.UtcNow
//                    };
//                    _context.IbtvaRecommendations.Add(recommendation);
//                }
//            }

//            // Update phase progress
//            if (program.CurrentPhase < 6)
//                program.CurrentPhase = 6;

//            // If submitForApproval is true, change FormStatus to Pending
//            if (dto.SubmitForApproval)
//            {
//                program.FormStatus = "Pending";
//            }
//            else
//            {
//                program.FormStatus = "Saved";
//            }

//            program.UpdatedById = _currentUser.UserId;
//            program.UpdatedAt = DateTimeOffset.UtcNow;

//            await _context.SaveChangesAsync();

//            var message = dto.SubmitForApproval
//                ? "Program submitted for approval successfully."
//                : "Phase 6 saved successfully.";

//            return Ok(new
//            {
//                message,
//                formStatus = program.FormStatus,
//                currentPhase = program.CurrentPhase,
//                showSubmit = true,
//                isLastPhase = true
//            });
//        }

//        /// <summary>
//        /// Get recommendations for editing (Phase 6)
//        /// </summary>
//        [HttpGet("programs/{programId}/phase6")]
//        public async Task<IActionResult> GetRecommendationsPhase6(int programId)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            var recommendations = await _context.IbtvaRecommendations
//                .Where(r => r.IbtvaProgramDetailsId == programId)
//                .Select(r => new
//                {
//                    r.Id,
//                    r.RecommendationText
//                })
//                .ToListAsync();

//            return Ok(recommendations);
//        }

//        #endregion

//        #region Phase Navigation & Tracking

//        /// <summary>
//        /// Get available phases and their completion status
//        /// Allows trainer to see which phases they can access
//        /// </summary>
//        [HttpGet("programs/{id}/phases")]
//        public async Task<IActionResult> GetPhaseStatus(int id)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(id);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid("You can only access your own entries");

//            // Check which phases have data
//            var hasPhase1 = true; // Always true since program exists
//            var hasPhase2 = await _context.IbtvaParticipantDemographics.AnyAsync(d => d.IbtvaProgramDetailsId == id);
//            var hasPhase3Content = await _context.IbtvaProgramContentAndResources.AnyAsync(c => c.IbtvaProgramDetailsId == id);
//            var hasPhase3ResourcePersons = await _context.IbtvaResourcePersons.AnyAsync(r => r.IbtvaProgramDetailsId == id);
//            var hasPhase3Topics = await _context.IbtvaTopicsCoveredInClass.AnyAsync(t => t.IbtvaProgramDetailsId == id);
//            var hasPhase3Aids = await _context.IbtvaTeachingAidsDeveloped.AnyAsync(a => a.IbtvaProgramDetailsId == id);
//            var hasPhase3 = hasPhase3Content || hasPhase3ResourcePersons || hasPhase3Topics || hasPhase3Aids;
//            var hasPhase4 = await _context.IbtvaAdvisoryServices.AnyAsync(a => a.IbtvaProgramDetailsId == id);
//            var hasPhase5 = await _context.IbtvaReports.AnyAsync(r => r.IbtvaProgramDetailsId == id);
//            var hasPhase6 = await _context.IbtvaRecommendations.AnyAsync(r => r.IbtvaProgramDetailsId == id);

//            var canEdit = program.FormStatus == "Saved" || program.FormStatus == "Rejected";

//            return Ok(new
//            {
//                programId = program.Id,
//                programTitle = program.Title,
//                formStatus = program.FormStatus,
//                canEdit,
//                canSubmit = hasPhase1, // Can submit anytime after Phase 1
//                phases = new[]
//                {
//                    new { phaseNumber = 1, name = "Program Details", completed = hasPhase1, required = true, accessible = true },
//                    new { phaseNumber = 2, name = "Participant Demographics", completed = hasPhase2, required = false, accessible = true },
//                    new { phaseNumber = 3, name = "Program Content & Resources", completed = hasPhase3, required = false, accessible = true },
//                    new { phaseNumber = 4, name = "Advisory Services", completed = hasPhase4, required = false, accessible = true },
//                    new { phaseNumber = 5, name = "Reports", completed = hasPhase5, required = false, accessible = true },
//                    new { phaseNumber = 6, name = "Recommendations", completed = hasPhase6, required = false, accessible = true }
//                },
//                message = "All phases are accessible. Only Phase 1 is required. Submit in Phase 6."
//            });
//        }

//        /// <summary>
//        /// Quick navigation - get phase endpoint for frontend routing
//        /// </summary>
//        [HttpGet("programs/{id}/navigate/{phaseNumber}")]
//        public async Task<IActionResult> NavigateToPhase(int id, int phaseNumber)
//        {
//            if (phaseNumber < 1 || phaseNumber > 6)
//                return BadRequest("Invalid phase number. Must be between 1 and 6.");

//            var program = await _context.IbtvaProgramDetails.FindAsync(id);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid("You can only access your own entries");

//            if (program.FormStatus == "Approved")
//                return BadRequest("Cannot edit approved programs");

//            var phaseInfo = new
//            {
//                programId = id,
//                phaseNumber,
//                endpoint = $"/api/units/ibtva/programs/{id}/phase{phaseNumber}",
//                method = "GET/POST/PUT",
//                canEdit = program.FormStatus == "Saved" || program.FormStatus == "Rejected",
//                showSubmit = phaseNumber == 6,
//                message = phaseNumber == 6
//                    ? "Phase 6 - You can submit for approval here"
//                    : $"Phase {phaseNumber} - Click Save & Next to continue"
//            };

//            return Ok(phaseInfo);
//        }

//        #endregion

//        #region Submit & Delete

//        /// <summary>
//        /// Submit program for approval (changes FormStatus from Saved to Pending)
//        /// Can submit after Phase 1 is completed - all other phases are optional
//        /// Alternative to using submitForApproval flag in Phase 6
//        /// </summary>
//        [HttpPost("programs/{id}/submit")]
//        public async Task<IActionResult> SubmitProgram(int id)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(id);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid();

//            if (program.FormStatus == "Pending" || program.FormStatus == "Approved")
//                return BadRequest("Program is already submitted or approved");

//            // Only Phase 1 (Program Details) is mandatory - all others are optional
//            // No need to check CurrentPhase since Phase 1 is already created

//            program.FormStatus = "Pending";
//            program.UpdatedById = _currentUser.UserId;
//            program.UpdatedAt = DateTimeOffset.UtcNow;

//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                message = "Program submitted for approval. Phases 2-6 are optional and can be completed later if needed.",
//                formStatus = program.FormStatus,
//                completedPhases = program.CurrentPhase
//            });
//        }

//        /// <summary>
//        /// Delete a program entry (only if FormStatus is Saved or Rejected)
//        /// </summary>
//        [HttpDelete("programs/{id}")]
//        public async Task<IActionResult> DeleteProgram(int id)
//        {
//            var program = await _context.IbtvaProgramDetails.FindAsync(id);
//            if (program == null)
//                return NotFound("Program not found");

//            if (program.CreatedById != _currentUser.UserId)
//                return Forbid("You can only delete your own entries");

//            if (program.FormStatus == "Pending" || program.FormStatus == "Approved")
//                return BadRequest("Cannot delete program with FormStatus: " + program.FormStatus);

//            _context.IbtvaProgramDetails.Remove(program);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Program deleted successfully" });
//        }

//        #endregion
//    }

//    #region DTOs

//    public class ProgramDetailsDto
//    {
//        public int UnitLocationId { get; set; }
//        public int? ProgramTypeId { get; set; }
//        public int? CategoryId { get; set; }
//        public int? TypeId { get; set; }
//        public int? ThemeId { get; set; }
//        public int? ThematicAreaId { get; set; }
//        public string Title { get; set; }
//        public DateTime? StartDate { get; set; }
//        public DateTime? EndDate { get; set; }
//        public string? Duration { get; set; }
//        public string? Location { get; set; }
//        public int? Mode { get; set; }
//        public decimal? TotalOutlayRs { get; set; }
//        public int? Funds { get; set; }
//    }

//    public class ParticipantDemographicsDto
//    {
//        public int? ParticipantId { get; set; }
//        public int? Male_SC { get; set; }
//        public int? Male_ST { get; set; }
//        public int? Male_OBC { get; set; }
//        public int? Male_GEN { get; set; }
//        public int? Female_SC { get; set; }
//        public int? Female_ST { get; set; }
//        public int? Female_OBC { get; set; }
//        public int? Female_GEN { get; set; }
//        public int? Total { get; set; }
//    }

//    public class ProgramContentDto
//    {
//        public ContentItemDto? Content { get; set; }
//        public List<ResourcePersonDto>? ResourcePersons { get; set; }
//        public List<TopicDto>? Topics { get; set; }
//        public List<TeachingAidDto>? TeachingAids { get; set; }
//    }

//    public class ContentItemDto
//    {
//        public string Title { get; set; }
//        public string Description { get; set; }
//    }

//    public class ResourcePersonDto
//    {
//        public string Name { get; set; }
//        public string? Designation { get; set; }
//        public string? Affiliation { get; set; }
//    }

//    public class TopicDto
//    {
//        public string TopicName { get; set; }
//        public string? Duration { get; set; }
//    }

//    public class TeachingAidDto
//    {
//        public string AidName { get; set; }
//        public string? Description { get; set; }
//    }

//    public class AdvisoryServicesDto
//    {
//        public int? NoOfBeneficiaries { get; set; }
//        public int? NoOfFaceToFaceDiscussions { get; set; }
//        public int? NoOfGroupDiscussions { get; set; }
//        public int? NoOfPhoneCalls { get; set; }
//        public int? NoOfWhatsappSMS { get; set; }
//    }

//    public class ReportDto
//    {
//        public string ReportTitle { get; set; }
//        public DateTime? ReportDate { get; set; }
//        public string? ReportFileUrl { get; set; }
//    }

//    public class RecommendationsDto
//    {
//        public List<RecommendationItemDto> Recommendations { get; set; }
//        public bool SubmitForApproval { get; set; }
//    }

//    public class RecommendationItemDto
//    {
//        public string RecommendationText { get; set; }
//    }

//    #endregion
//}


///// Get all programs with their current status and completion phase
///// Shows history of all entries created by the trainer
///// </summary>
//[HttpGet("programs/history")]
//public async Task<IActionResult> GetProgramHistory([FromQuery] int unitLocationId, [FromQuery] string? status = null)
//{
//    if (!await ValidateIBTVAUnit(unitLocationId))
//        return BadRequest("Invalid IBTVA unit location");

//    if (!await ValidateTrainerAccess(unitLocationId))
//        return Forbid("You don't have access to this unit location");

//    var query = _context.IbtvaProgramDetails
//        .Where(p => p.UnitLocationId == unitLocationId && p.CreatedById == _currentUser.UserId);

//    // Filter by formStatus if provided
//    if (!string.IsNullOrEmpty(status))
//    {
//        query = query.Where(p => p.FormStatus == status);
//    }

//    var programs = await query
//        .Select(p => new
//        {
//            p.Id,
//            p.Title,
//            p.StartDate,
//            p.EndDate,
//            p.Location,
//            FormStatus = p.FormStatus, // Saved, Pending, Approved, Rejected
//            CurrentPhase = p.CurrentPhase, // Which phase is completed (1-6)
//            p.CreatedAt,
//            p.UpdatedAt,
//            CanEdit = p.FormStatus == "Saved" || p.FormStatus == "Rejected"
//        })
//        .OrderByDescending(p => p.CreatedAt)
//        .ToListAsync();

//    return Ok(programs);
//}

///// <summary>
///// Get program summary with completion status of all phases
///// All phases are accessible and can be completed in any order
///// </summary>
//[HttpGet("programs/{id}/summary")]
//public async Task<IActionResult> GetProgramSummary(int id)
//{
//    var program = await _context.IbtvaProgramDetails.FindAsync(id);
//    if (program == null)
//        return NotFound("Program not found");

//    if (program.CreatedById != _currentUser.UserId)
//        return Forbid("You can only access your own entries");

//    // Check completion status of each phase
//    var hasDemographics = await _context.IbtvaParticipantDemographics
//        .AnyAsync(d => d.IbtvaProgramDetailsId == id);

//    var hasContent = await _context.IbtvaProgramContentAndResources
//        .AnyAsync(c => c.IbtvaProgramDetailsId == id);

//    var hasAdvisory = await _context.IbtvaAdvisoryServices
//        .AnyAsync(a => a.IbtvaProgramDetailsId == id);

//    var hasReports = await _context.IbtvaReports
//        .AnyAsync(r => r.IbtvaProgramDetailsId == id);

//    var hasRecommendations = await _context.IbtvaRecommendations
//        .AnyAsync(r => r.IbtvaProgramDetailsId == id);

//    var summary = new
//    {
//        programId = program.Id,
//        title = program.Title,
//        formStatus = program.FormStatus,
//        currentPhase = program.CurrentPhase,
//        canEdit = program.FormStatus == "Saved" || program.FormStatus == "Rejected",
//        canSubmit = true, // Can always submit after Phase 1 is created
//        phases = new
//        {
//            phase1_ProgramDetails = new
//            {
//                completed = true,
//                accessible = true,
//                required = true,
//                description = "Program Details (Required)"
//            },
//            phase2_Demographics = new
//            {
//                completed = hasDemographics,
//                accessible = true,
//                required = false,
//                description = "Participant Demographics (Optional - can skip)"
//            },
//            phase3_ContentAndResources = new
//            {
//                completed = hasContent,
//                accessible = true,
//                required = false,
//                description = "Program Content & Resources (Optional - can skip)"
//            },
//            phase4_AdvisoryServices = new
//            {
//                completed = hasAdvisory,
//                accessible = true,
//                required = false,
//                description = "Advisory Services (Optional - can skip)"
//            },
//            phase5_Reports = new
//            {
//                completed = hasReports,
//                accessible = true,
//                required = false,
//                description = "Reports (Optional - can skip)"
//            },
//            phase6_Recommendations = new
//            {
//                completed = hasRecommendations,
//                accessible = true,
//                required = false,
//                description = "Recommendations (Optional) - Submit here"
//            }
//        },
//        instructions = new
//        {
//            message = "You can navigate to any phase directly without completing previous phases.",
//            submission = "Navigate to Phase 6 to submit the program. All other phases are optional.",
//            editing = "Phases 2-6 can be filled, skipped, or edited later before final submission."
//        }
//    };

//    return Ok(summary);
//}

//#endregion

//#region Phase 1: Program Details (Main Entry)

///// <summary>
///// Create new program entry (Phase 1)
///// FormStatus: "Saved" (can continue to next phases)
///// </summary>
//[HttpPost("programs/phase1")]
//public async Task<IActionResult> CreateProgramPhase1([FromBody] ProgramDetailsDto dto)
//{
//    if (!ModelState.IsValid)
//        return BadRequest(ModelState);

//    if (!await ValidateIBTVAUnit(dto.UnitLocationId))
//        return BadRequest("Invalid IBTVA unit location");

//    if (!await ValidateTrainerAccess(dto.UnitLocationId))
//        return Forbid("You don't have access to this unit location");

//    var program = new IbtvaProgramDetails
//    {
//        UnitLocationId = dto.UnitLocationId,
//        OrganizationId = _currentUser.OrganizationId,
//        ProgramTypeId = dto.ProgramTypeId,
//        CategoryId = dto.CategoryId,
//        TypeId = dto.TypeId,
//        ThemeId = dto.ThemeId,
//        ThematicAreaId = dto.ThematicAreaId,
//        Title = dto.Title,
//        StartDate = dto.StartDate,
//        EndDate = dto.EndDate,
//        Duration = dto.Duration,
//        Location = dto.Location,
//        Mode = dto.Mode,
//        TotalOutlayRs = dto.TotalOutlayRs,
//        Funds = dto.Funds,
//        FormStatus = "Saved", // Initial FormStatus
//        CurrentPhase = 1, // Phase 1 completed
//        CreatedById = _currentUser.UserId,
//        CreatedAt = DateTimeOffset.UtcNow
//    };

//    _context.IbtvaProgramDetails.Add(program);
//    await _context.SaveChangesAsync();

//    return Ok(new
//    {
//        programId = program.Id,
//        message = "Phase 1 saved successfully.",
//        nextPhase = 2,
//        formStatus = program.FormStatus,
//        currentPhase = program.CurrentPhase
//    });
//}

///// <summary>
///// Update program details (Phase 1)
///// </summary>
//[HttpPut("programs/{id}/phase1")]
//public async Task<IActionResult> UpdateProgramPhase1(int id, [FromBody] ProgramDetailsDto dto)
//{
//    var program = await _context.IbtvaProgramDetails.FindAsync(id);
//    if (program == null)
//        return NotFound("Program not found");

//    if (program.CreatedById != _currentUser.UserId)
//        return Forbid("You can only update your own entries");

//    if (program.FormStatus == "Approved")
//        return BadRequest("Cannot edit approved programs");

//    // Update fields
//    program.Title = dto.Title;
//    program.CategoryId = dto.CategoryId;
//    program.TypeId = dto.TypeId;
//    program.ThemeId = dto.ThemeId;
//    program.StartDate = dto.StartDate;
//    program.EndDate = dto.EndDate;
//    program.Duration = dto.Duration;
//    program.Location = dto.Location;
//    program.UpdatedById = _currentUser.UserId;
//    program.UpdatedAt = DateTimeOffset.UtcNow;

//    await _context.SaveChangesAsync();

//    return Ok(new
//    {
//        message = "Phase 1 updated successfully",
//        nextPhase = 2,
//        currentPhase = program.CurrentPhase
//    });
//}

///// <summary>
///// Get program details for editing (Phase 1)
///// </summary>
//[HttpGet("programs/{id}/phase1")]
//public async Task<IActionResult> GetProgramPhase1(int id)
//{
//    var program = await _context.IbtvaProgramDetails
//        .Where(p => p.Id == id && p.CreatedById == _currentUser.UserId)
//        .Select(p => new
//        {
//            p.Id,
//            p.UnitLocationId,
//            p.ProgramTypeId,
//            p.CategoryId,
//            p.TypeId,
//            p.ThemeId,
//            p.ThematicAreaId,
//            p.Title,
//            p.StartDate,
//            p.EndDate,
//            p.Duration,
//            p.Location,
//            p.Mode,
//            p.TotalOutlayRs,
//            p.Funds,
//            p.FormStatus,
//            p.CurrentPhase
//        })
//        .FirstOrDefaultAsync();

//    if (program == null)
//        return NotFound("Program not found");

//    return Ok(program);
//}

//#endregion

//#region Phase 2: Participant Demographics

///// <summary>
///// Save participant demographics (Phase 2)
///// Can have multiple demographic entries per program
///// </summary>
//[HttpPost("programs/{programId}/phase2")]
//public async Task<IActionResult> SaveDemographicsPhase2(int programId, [FromBody] List<ParticipantDemographicsDto> demographics)
//{
//    var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//    if (program == null)
//        return NotFound("Program not found");

//    if (program.CreatedById != _currentUser.UserId)
//        return Forbid();

//    if (program.FormStatus == "Approved")
//        return BadRequest("Cannot edit approved programs");

//    // Remove existing demographics and add new ones
//    var existing = await _context.IbtvaParticipantDemographics
//        .Where(d => d.IbtvaProgramDetailsId == programId)
//        .ToListAsync();
//    _context.IbtvaParticipantDemographics.RemoveRange(existing);

//    // Add new demographics
//    if (demographics != null && demographics.Any())
//    {
//        foreach (var dto in demographics)
//        {
//            var demographic = new IbtvaParticipantDemographics
//            {
//                IbtvaProgramDetailsId = programId,
//                ParticipantId = dto.ParticipantId,
//                Male_SC = dto.Male_SC,
//                Male_ST = dto.Male_ST,
//                Male_OBC = dto.Male_OBC,
//                Male_GEN = dto.Male_GEN,
//                Female_SC = dto.Female_SC,
//                Female_ST = dto.Female_ST,
//                Female_OBC = dto.Female_OBC,
//                Female_GEN = dto.Female_GEN,
//                Total = dto.Total,
//                CreatedById = _currentUser.UserId,
//                CreatedAt = DateTimeOffset.UtcNow
//            };
//            _context.IbtvaParticipantDemographics.Add(demographic);
//        }
//    }

//    // Update phase progress
//    if (program.CurrentPhase < 2)
//        program.CurrentPhase = 2;

//    await _context.SaveChangesAsync();

//    return Ok(new
//    {
//        message = "Phase 2 saved successfully.",
//        nextPhase = 3,
//        currentPhase = program.CurrentPhase
//    });
//}

///// <summary>
///// Get demographics for editing (Phase 2)
///// </summary>
//[HttpGet("programs/{programId}/phase2")]
//public async Task<IActionResult> GetDemographicsPhase2(int programId)
//{
//    var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//    if (program == null)
//        return NotFound("Program not found");

//    if (program.CreatedById != _currentUser.UserId)
//        return Forbid();

//    var demographics = await _context.IbtvaParticipantDemographics
//        .Where(d => d.IbtvaProgramDetailsId == programId)
//        .Select(d => new
//        {
//            d.Id,
//            d.ParticipantId,
//            d.Male_SC,
//            d.Male_ST,
//            d.Male_OBC,
//            d.Male_GEN,
//            d.Female_SC,
//            d.Female_ST,
//            d.Female_OBC,
//            d.Female_GEN,
//            d.Total
//        })
//        .ToListAsync();

//    return Ok(demographics);
//}

//#endregion

//#region Phase 3: Program Content and Resources

///// <summary>
///// Save program content with resource persons, topics, and teaching aids (Phase 3)
///// This is a composite phase with multiple sub-sections
///// </summary>
//[HttpPost("programs/{programId}/phase3")]
//public async Task<IActionResult> SaveContentPhase3(int programId, [FromBody] ProgramContentDto dto)
//{
//    var program = await _context.IbtvaProgramDetails.FindAsync(programId);
//    if (program == null)
//        return NotFound("Program not found");

//    if (program.CreatedById != _currentUser.UserId)
//        return Forbid();

//    if (program.FormStatus == "Approved")
//        return BadRequest("Cannot edit approved programs");

//    // Remove existing content and related data
//    var existingContent = await _context.IbtvaProgramContentAndResources
//        .Where(c => c.IbtvaProgramDetailsId == programId)
//        .ToListAsync();
//    _context.IbtvaProgramContentAndResources.RemoveRange(existingContent);

//    var existingResourcePersons = await _context.IbtvaResourcePersons
//        .Where(r => r.IbtvaProgramDetailsId == programId)
//        .ToListAsync();
//    _context.IbtvaResourcePersons.RemoveRange(existingResourcePersons);

//    var existingTopics = await _context.IbtvaTopicsCoveredInClass
//        .Where(t => t.IbtvaProgramDetailsId == programId)
//        .ToListAsync();
//    _context.IbtvaTopicsCoveredInClass.RemoveRange(existingTopics);

//    var existingAids = await _context.IbtvaTeachingAidsDeveloped
//        .Where(a => a.IbtvaProgramDetailsId == programId)
//        .ToListAsync();
//    _context.IbtvaTeachingAidsDeveloped.RemoveRange(existingAids);

//    // Add new content
//    if (dto.Content != null)
//    {
//        var content = new IbtvaProgramContentAndResources
//        {
//            IbtvaProgramDetailsId = programId,
//            Title = dto.Content.Title,
//            Description = dto.Content.Description,
//            CreatedById = _currentUser.UserId,
//            CreatedAt = DateTimeOffset.UtcNow
//        };
//        _context.IbtvaProgramContentAndResources.Add(content);
//    }

//    // Add resource persons
//    if (dto.ResourcePersons != null && dto.ResourcePersons.Any())
//    {
//        foreach (var rp in dto.ResourcePersons)
//        {
//            var resourcePerson = new IbtvaResourcePerson
//            {
//                IbtvaProgramDetailsId = programId,
//                Name = rp.Name,
//                Designation = rp.Designation,
//                Affiliation = rp.Affiliation,
//                CreatedById = _currentUser.UserId,
//                CreatedAt = DateTimeOffset.UtcNow
//            };
//            _context.IbtvaResourcePersons.Add(resourcePerson);
//        }
//    }

//    // Add topics
//    if (dto.Topics != null && dto.Topics.Any())
//    {
//        foreach (var t in dto.Topics)
//        {
//            var topic = new IbtvaTopicsCoveredInClass
//            {
//                IbtvaProgramDetailsId = programId,
//                TopicName = t.TopicName,
//                Duration = t.Duration,
//                CreatedById = _currentUser.UserId,
//                CreatedAt = DateTimeOffset.UtcNow
//            };
//            _context.IbtvaTopicsCoveredInClass.Add(topic);
//        }
//    }

//    // Add teaching aids
//    if (dto.TeachingAids != null && dto.TeachingAids.Any())
//    {
//        foreach (var aid in dto.TeachingAids)
//        {
//            var teachingAid = new IbtvaTeachingAidsDeveloped
//            {
//                IbtvaProgramDetailsId = programId,
//                AidName = aid.AidName,
//                Description = aid.Description,
//                CreatedById = _currentUser.UserId,
//                CreatedAt = DateTimeOffset.UtcNow
//            };
//            _context.IbtvaTeachingAidsDeveloped.Add(teachingAid);
//        }
//    }

//    // Update phase progress
//    if (program.CurrentPhase < 3)
//        program.CurrentPhase = 3;

//    await _context.SaveChangesAsync();

//    return Ok(new
//    {
//        message = "Phase 3 saved successfully.",
//        nextPhase = 4,
//        currentPhase = program.CurrentPhase
//    });
//}

