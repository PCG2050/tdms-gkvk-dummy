//using Domain.Entities.Enum;
//using Domain.Entities.MasterData;
//using Infrastructure.DbContext;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace WebApi.Controllers.DataTables.FTI
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class FTIReportController : ControllerBase
//    {
//        private readonly TdmsDbContext _context;

//        public FTIReportController(TdmsDbContext context)
//        {
//            _context = context;
//        }

//        // =======================================================
//        // COMBINED ENDPOINT: FTI Monthly Report with Hierarchy Support
//        // =======================================================
//        [HttpGet("MonthlyReport")]
//        public async Task<IActionResult> GetMonthlyReport(int unitId, int year, int month, int? unitLocationId = null)
//        {
//            // Get current user from JWT token
//            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

//            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
//                return Unauthorized("User information not found");

//            int currentUserId = int.Parse(userIdClaim);
//            var currentUserRole = Enum.Parse<Role>(userRoleClaim);

//            // Get list of trainer IDs based on role and hierarchy
//            var trainerIds = await GetTrainerIdsForUser(currentUserId, currentUserRole, unitLocationId);

//            if (!trainerIds.Any())
//                return Ok(new
//                {
//                    UnitId = unitId,
//                    UnitName = "Farm Training Institute",
//                    Message = "No trainers found under your supervision",
//                    FrontLineDemonstrations = new { Number = 0, Area = 0.0, Farmers = 0 },
//                    FarmTrials = new { Number = 0, Area = 0.0, Farmers = 0 },
//                    ExtensionActivities = GetEmptyExtensionActivities(),
//                    Summary = new { TotalDemonstrations = 0, TotalFarmers = 0 }
//                });

//            // ---------- (A) FRONT LINE DEMONSTRATIONS ----------
//            var startDateOnly = new DateOnly(year, month, 1);
//            var endDateOnly = startDateOnly.AddMonths(1);

//            var frontLineDemonstrations = await _context.FtiProgramDetails
//                .Include(p => p.Type)
//                .Include(p => p.Category)
//                .Where(p => trainerIds.Contains(p.CreatedById ?? 0) &&
//                            p.StartDate >= startDateOnly &&
//                            p.StartDate < endDateOnly &&
//                            p.Category != null && p.Category.Name == "Front Line Demonstration")
//                .ToListAsync();

//            var fldData = new
//            {
//                Number = frontLineDemonstrations.Count,
//                Area = frontLineDemonstrations.Sum(p => p.AreaInHectares ?? 0),
//                Farmers = await _context.FtiParticipantDemographics
//                    .Where(d => frontLineDemonstrations.Select(f => f.Id).Contains(d.FtiProgramDetailsId ?? 0))
//                    .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0) +
//                                   (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0))
//            };

//            // ---------- (B) FARM TRIALS ----------
//            var farmTrials = await _context.FtiProgramDetails
//                .Include(p => p.Category)
//                .Where(p => trainerIds.Contains(p.CreatedById ?? 0) &&
//                            p.StartDate >= startDateOnly &&
//                            p.StartDate < endDateOnly &&
//                            p.Category != null && p.Category.Name == "Farm trials")
//                .ToListAsync();

//            var ftData = new
//            {
//                Number = farmTrials.Count,
//                Area = farmTrials.Sum(p => p.AreaInHectares ?? 0),
//                Farmers = await _context.FtiParticipantDemographics
//                    .Where(d => farmTrials.Select(f => f.Id).Contains(d.FtiProgramDetailsId ?? 0))
//                    .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0) +
//                                   (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0))
//            };

//            // ---------- (C) EXTENSION ACTIVITIES ----------
//            var extensionActivities = await GetExtensionActivitiesData(trainerIds, year, month);

//            // ---------- (D) FINAL RESULT ----------
//            return Ok(new
//            {
//                UnitId = unitId,
//                UnitName = "Farm Training Institute",
//                UnitLocationId = unitLocationId,
//                ReportScope = GetReportScope(currentUserRole, trainerIds.Count),
//                FrontLineDemonstrations = fldData,
//                FarmTrials = ftData,
//                ExtensionActivities = extensionActivities,
//                Summary = new
//                {
//                    TotalDemonstrations = fldData.Number + ftData.Number,
//                    TotalFarmers = fldData.Farmers + ftData.Farmers,
//                    TotalTrainers = trainerIds.Count
//                }
//            });
//        }

//        // =======================================================
//        // HELPER: Get Trainer IDs based on User Role and Hierarchy
//        // =======================================================
//        private async Task<List<int>> GetTrainerIdsForUser(int userId, Role role, int? unitLocationId)
//        {
//            switch (role)
//            {
//                case Role.TRAINER:
//                    // Trainers only see their own data
//                    return new List<int> { userId };

//                case Role.UNITHEAD:
//                    // UnitHeads see all trainers they created
//                    var trainersUnderUnitHead = await _context.Users
//                        .Where(u => u.CreatedById == userId && u.Role == Role.TRAINER && !u.IsDeactivated)
//                        .Select(u => u.Id)
//                        .ToListAsync();

//                    // If unitLocationId specified, filter trainers assigned to that location
//                    if (unitLocationId.HasValue && trainersUnderUnitHead.Any())
//                    {
//                        trainersUnderUnitHead = await _context.UnitTrainers
//                            .Where(ta => trainersUnderUnitHead.Contains(ta.TrainerId) &&
//                                        ta.UnitLocationId == unitLocationId.Value)
//                            .Select(ta => ta.TrainerId)
//                            .Distinct()
//                            .ToListAsync();
//                    }

//                    return trainersUnderUnitHead;

//                case Role.ADMIN:
//                    // Admins see all trainers created by UnitHeads they created

//                    // Step 1: Get all UnitHeads created by this Admin
//                    var unitHeadIds = await _context.Users
//                        .Where(u => u.CreatedById == userId && u.Role == Role.UNITHEAD && !u.IsDeactivated)
//                        .Select(u => u.Id)
//                        .ToListAsync();

//                    if (!unitHeadIds.Any())
//                        return new List<int>();

//                    // Step 2: Get all Trainers created by those UnitHeads
//                    var trainersUnderAdmin = await _context.Users
//                        .Where(u => unitHeadIds.Contains(u.CreatedById ?? 0) &&
//                                   u.Role == Role.TRAINER &&
//                                   !u.IsDeactivated)
//                        .Select(u => u.Id)
//                        .ToListAsync();

//                    // If unitLocationId specified, filter trainers assigned to that location
//                    if (unitLocationId.HasValue && trainersUnderAdmin.Any())
//                    {
//                        trainersUnderAdmin = await _context.UnitTrainers
//                            .Where(ta => trainersUnderAdmin.Contains(ta.TrainerId) &&
//                                        ta.UnitLocationId == unitLocationId.Value)
//                            .Select(ta => ta.TrainerId)
//                            .Distinct()
//                            .ToListAsync();
//                    }

//                    return trainersUnderAdmin;

//                case Role.SUPERADMIN:
//                    // SuperAdmin sees all trainers (optionally filtered by unitLocationId)
//                    var query = _context.Users.Where(u => u.Role == Role.TRAINER && !u.IsDeactivated);

//                    if (unitLocationId.HasValue)
//                    {
//                        var trainerIdsForLocation = await _context.UnitTrainers
//                            .Where(ta => ta.UnitLocationId == unitLocationId.Value)
//                            .Select(ta => ta.TrainerId)
//                            .Distinct()
//                            .ToListAsync();

//                        return trainerIdsForLocation;
//                    }

//                    return await query.Select(u => u.Id).ToListAsync();

//                default:
//                    return new List<int>();
//            }
//        }

//        // =======================================================
//        // HELPER: Get Report Scope Description
//        // =======================================================
//        private string GetReportScope(Role role, int trainerCount)
//        {
//            return role switch
//            {
//                Role.TRAINER => "Individual Trainer Report",
//                Role.UNITHEAD => $"Unit Head Report (Aggregated from {trainerCount} trainer(s))",
//                Role.ADMIN => $"Admin Report (Aggregated from {trainerCount} trainer(s) across all Unit Heads)",
//                Role.SUPERADMIN => $"Super Admin Report (Aggregated from {trainerCount} trainer(s))",
//                _ => "Unknown Scope"
//            };
//        }

//        // =======================================================
//        // HELPER: Extension Activities Data
//        // =======================================================
//        private async Task<object> GetExtensionActivitiesData(List<int> trainerIds, int year, int month)
//        {
//            var startDateOnly = new DateOnly(year, month, 1);
//            var endDateOnly = startDateOnly.AddMonths(1);

//            // Get all programs for the month for all trainers
//            var allPrograms = await _context.FtiProgramDetails
//                .Include(p => p.Category)
//                .Include(p => p.Type)
//                .Include(p => p.ProgramType)
//                .Where(p => trainerIds.Contains(p.CreatedById ?? 0) &&
//                            p.StartDate >= startDateOnly &&
//                            p.StartDate < endDateOnly)
//                .ToListAsync();

//            // Helper method to get participant count for programs
//            async Task<int> GetParticipantCount(List<int> programIds)
//            {
//                if (!programIds.Any()) return 0;

//                return await _context.FtiParticipantDemographics
//                    .Where(d => programIds.Contains(d.FtiProgramDetailsId ?? 0))
//                    .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0) +
//                                   (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));
//            }

//            // 1. On-campus Programs
//            var onCampusPrograms = allPrograms.Where(p =>
//                p.Type != null && p.Type.Name == "On-campus").ToList();
//            var onCampusPart = await GetParticipantCount(onCampusPrograms.Select(p => p.Id).ToList());

//            // 2. Collaborative Programs
//            var collaborativePrograms = allPrograms.Where(p =>
//                p.Type != null && p.Type.Name == "Collaborative").ToList();
//            var collaborativePart = await GetParticipantCount(collaborativePrograms.Select(p => p.Id).ToList());

//            // 3. Resource Person / Guest Lectures
//            var guestLecturePrograms = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Guest Lecture").ToList();
//            var guestLecturePart = await GetParticipantCount(guestLecturePrograms.Select(p => p.Id).ToList());

//            // 4. Field Visits by Scientists
//            var fieldVisitsByScientists = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Field visit by scientist").ToList();
//            var fieldVisitsByScientistsPart = await GetParticipantCount(fieldVisitsByScientists.Select(p => p.Id).ToList());

//            // 5. Consultancy - Telephone
//            var telephoneConsultancy = await _context.FtiAdvisoryServices
//                .Include(a => a.ProgramDetails)
//                .Where(a => trainerIds.Contains(a.ProgramDetails.CreatedById ?? 0) &&
//                            a.ProgramDetails.StartDate >= startDateOnly &&
//                            a.ProgramDetails.StartDate < endDateOnly)
//                .SumAsync(a => a.NoOfPhoneCalls);

//            // 6. Consultancy - Face to Face
//            var faceToFaceConsultancy = await _context.FtiAdvisoryServices
//                .Include(a => a.ProgramDetails)
//                .Where(a => trainerIds.Contains(a.ProgramDetails.CreatedById ?? 0) &&
//                            a.ProgramDetails.StartDate >= startDateOnly &&
//                            a.ProgramDetails.StartDate < endDateOnly)
//                .SumAsync(a => a.NoOfFaceToFaceDiscussions);

//            // 7. Group Discussion Meetings
//            var groupDiscussions = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Group discussion").ToList();
//            var groupDiscussionsPart = await GetParticipantCount(groupDiscussions.Select(p => p.Id).ToList());

//            // 8. Diagnostic Field Visits
//            var diagnosticVisits = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Diagnostic field visit").ToList();
//            var diagnosticVisitsPart = await GetParticipantCount(diagnosticVisits.Select(p => p.Id).ToList());

//            // 9. Newspaper Coverage
//            var newspaperCoverage = allPrograms.Count(p =>
//                p.Category != null && p.Category.Name == "Newspaper coverage");

//            // 10. Radio Programme
//            var radioPrograms = allPrograms.Count(p =>
//                p.Category != null && p.Category.Name == "Radio programme");

//            // 11. Exposure Visits Organized
//            var exposureVisits = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Exposure visit" &&
//                p.ProgramType != null && p.ProgramType.Name == "Organized").ToList();
//            var exposureVisitsPart = await GetParticipantCount(exposureVisits.Select(p => p.Id).ToList());

//            // 12. Educational Tours
//            var educationalTours = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Educational tour").ToList();
//            var educationalToursPart = await GetParticipantCount(educationalTours.Select(p => p.Id).ToList());

//            // 13. Field Days Organized
//            var fieldDays = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Field day").ToList();
//            var fieldDaysPart = await GetParticipantCount(fieldDays.Select(p => p.Id).ToList());

//            // 14. Important Events
//            var importantEvents = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Important event").ToList();
//            var importantEventsPart = await GetParticipantCount(importantEvents.Select(p => p.Id).ToList());

//            // 15. Special Day Celebration
//            var specialDays = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Special day celebration").ToList();
//            var specialDaysPart = await GetParticipantCount(specialDays.Select(p => p.Id).ToList());

//            // 16. Method Demonstrations
//            var methodDemos = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Method demonstration").ToList();
//            var methodDemosPart = await GetParticipantCount(methodDemos.Select(p => p.Id).ToList());

//            // 17. Bi-Monthly Technical Workshop
//            var workshops = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Workshop").ToList();
//            var workshopsPart = await GetParticipantCount(workshops.Select(p => p.Id).ToList());

//            // 18. SMS to Registered Farmers
//            var smsCount = await _context.FtiAdvisoryServices
//                .Include(a => a.ProgramDetails)
//                .Where(a => trainerIds.Contains(a.ProgramDetails.CreatedById ?? 0) &&
//                            a.ProgramDetails.StartDate >= startDateOnly &&
//                            a.ProgramDetails.StartDate < endDateOnly)
//                .SumAsync(a => a.NoOfFacebookSMS);

//            // 19. WhatsApp SMS
//            var whatsappCount = await _context.FtiAdvisoryServices
//                .Include(a => a.ProgramDetails)
//                .Where(a => trainerIds.Contains(a.ProgramDetails.CreatedById ?? 0) &&
//                            a.ProgramDetails.StartDate >= startDateOnly &&
//                            a.ProgramDetails.StartDate < endDateOnly)
//                .SumAsync(a => a.NoOfWhatsappSMS);

//            // 20. Meetings
//            var meetings = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "Meeting").ToList();
//            var meetingsPart = await GetParticipantCount(meetings.Select(p => p.Id).ToList());

//            // 21. DAESI Classes
//            var daesiClasses = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "DAESI class").ToList();
//            var daesiClassesPart = await GetParticipantCount(daesiClasses.Select(p => p.Id).ToList());

//            // 22. RSKs Visit
//            var rskVisits = allPrograms.Where(p =>
//                p.Category != null && p.Category.Name == "RSK visit").ToList();
//            var rskVisitsPart = await GetParticipantCount(rskVisits.Select(p => p.Id).ToList());

//            // Build response
//            return new
//            {
//                OnCampus = new { Programmes = onCampusPrograms.Count, Participants = onCampusPart },
//                CollaborativePrograms = new { Programmes = collaborativePrograms.Count, Participants = collaborativePart },
//                ResourcePerson = new { Programmes = guestLecturePrograms.Count, Participants = guestLecturePart },
//                FieldVisitsByScientists = new { Programmes = fieldVisitsByScientists.Count, Participants = fieldVisitsByScientistsPart },
//                ConsultancyTelephone = new { Programmes = telephoneConsultancy, Participants = telephoneConsultancy },
//                ConsultancyFaceToFace = new { Programmes = faceToFaceConsultancy, Participants = faceToFaceConsultancy },
//                GroupDiscussions = new { Programmes = groupDiscussions.Count, Participants = groupDiscussionsPart },
//                DiagnosticFieldVisits = new { Programmes = diagnosticVisits.Count, Participants = diagnosticVisitsPart },
//                NewspaperCoverage = new { Programmes = newspaperCoverage, Participants = 0 },
//                RadioProgramme = new { Programmes = radioPrograms, Participants = 0 },
//                ExposureVisitsOrganized = new { Programmes = exposureVisits.Count, Participants = exposureVisitsPart },
//                EducationalToursOrganized = new { Programmes = educationalTours.Count, Participants = educationalToursPart },
//                FieldDaysOrganized = new { Programmes = fieldDays.Count, Participants = fieldDaysPart },
//                ImportantEventsOrganized = new { Programmes = importantEvents.Count, Participants = importantEventsPart },
//                SpecialDayCelebration = new { Programmes = specialDays.Count, Participants = specialDaysPart },
//                MethodDemonstrations = new { Programmes = methodDemos.Count, Participants = methodDemosPart },
//                BiMonthlyWorkshop = new { Programmes = workshops.Count, Participants = workshopsPart },
//                SMSToFarmers = new { Programmes = smsCount, Participants = smsCount },
//                WhatsAppSMS = new { Programmes = whatsappCount, Participants = whatsappCount },
//                Meetings = new { Programmes = meetings.Count, Participants = meetingsPart },
//                DAESIClasses = new { Programmes = daesiClasses.Count, Participants = daesiClassesPart },
//                RSKsVisit = new { Programmes = rskVisits.Count, Participants = rskVisitsPart }
//            };
//        }

//        // =======================================================
//        // HELPER: Empty Extension Activities (for no data scenario)
//        // =======================================================
//        private object GetEmptyExtensionActivities()
//        {
//            return new
//            {
//                OnCampus = new { Programmes = 0, Participants = 0 },
//                CollaborativePrograms = new { Programmes = 0, Participants = 0 },
//                ResourcePerson = new { Programmes = 0, Participants = 0 },
//                FieldVisitsByScientists = new { Programmes = 0, Participants = 0 },
//                ConsultancyTelephone = new { Programmes = 0, Participants = 0 },
//                ConsultancyFaceToFace = new { Programmes = 0, Participants = 0 },
//                GroupDiscussions = new { Programmes = 0, Participants = 0 },
//                DiagnosticFieldVisits = new { Programmes = 0, Participants = 0 },
//                NewspaperCoverage = new { Programmes = 0, Participants = 0 },
//                RadioProgramme = new { Programmes = 0, Participants = 0 },
//                ExposureVisitsOrganized = new { Programmes = 0, Participants = 0 },
//                EducationalToursOrganized = new { Programmes = 0, Participants = 0 },
//                FieldDaysOrganized = new { Programmes = 0, Participants = 0 },
//                ImportantEventsOrganized = new { Programmes = 0, Participants = 0 },
//                SpecialDayCelebration = new { Programmes = 0, Participants = 0 },
//                MethodDemonstrations = new { Programmes = 0, Participants = 0 },
//                BiMonthlyWorkshop = new { Programmes = 0, Participants = 0 },
//                SMSToFarmers = new { Programmes = 0, Participants = 0 },
//                WhatsAppSMS = new { Programmes = 0, Participants = 0 },
//                Meetings = new { Programmes = 0, Participants = 0 },
//                DAESIClasses = new { Programmes = 0, Participants = 0 },
//                RSKsVisit = new { Programmes = 0, Participants = 0 }
//            };
//        }
//    }
//}
