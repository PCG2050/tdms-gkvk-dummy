using Domain.Entities.MasterData;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.DataTables.IBTVA
{
    [Route("api/[controller]")]
    [ApiController]
    public class IBTVAReportController : ControllerBase
    {
        private readonly TdmsDbContext _context;

        public IBTVAReportController(TdmsDbContext context)
        {
            _context = context;
        }

        // =======================================================
        // SINGLE ENDPOINT: Combined Monthly Report
        // =======================================================
        [HttpGet("MonthlyReport")]
        public async Task<IActionResult> GetMonthlyReport(int unitId, int year, int month)
        {
            // ---------- (A) TRAINING PROGRAMMES ----------
            var trainingPrograms = await _context.IbtvaProgramDetails
                .Where(p => p.CreatedById == 81 &&
                            p.StartDate.Year == year &&
                            p.StartDate.Month == month)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    Date = (p.StartDate != DateOnly.MinValue && p.EndDate != DateOnly.MinValue)
                        ? $"{p.StartDate:dd-MM-yyyy} to {p.EndDate:dd-MM-yyyy}"
                        : (p.StartDate != DateOnly.MinValue ? p.StartDate.ToString("dd-MM-yyyy") : ""),
                    p.Duration,
                    // 🟢 Fetch participants from related IbtvaParticipantDemographics table
                    NoOfParticipants = _context.IbtvaParticipantDemographics
                             .Where(d => d.IbtvaProgramDetailsId == p.Id)
                             .Sum(d => d.Total ?? 0),


                    Category = !string.IsNullOrEmpty(p.CategoryOther)
                        ? p.CategoryOther
                        : (p.Category != null ? p.Category.Name : "Training Programmes")
                })
                .OrderBy(p => p.Id)
                .ToListAsync();

            // ---------- (B) SERVICES (Bakery & Value-Added Products) ----------
            // 1. Define the start and end as DateTimeOffset (as you did)
            var startDateTime = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
            var endDateTime = startDateTime.AddMonths(1);

            // 2. CONVERT these to DateOnly for the comparison (This is the critical step)
            // Use the ToDateOnly() method on the DateTimeOffset.Date property to get a DateOnly object.
            var startDateOnly = DateOnly.FromDateTime(startDateTime.Date);
            var endDateOnly = DateOnly.FromDateTime(endDateTime.Date);

            var serviceData = await _context.Services
              .Include(s => s.Theme)
              .Where(s => s.CreatedById == 81 &&
                          s.Component == "IBT&VA" &&
                          s.Date >= startDateOnly &&
                          s.Date < endDateOnly)
              .Select(s => new
              {
                  Theme = s.OtherTheme ?? s.Theme.Name,
                  Product = s.CropPlantProductName,
                  Quantity = s.Number,
                  Amount = s.AmountGenerated
              })
              .ToListAsync();



            var production = serviceData
                .GroupBy(s => s.Theme)
                .Select(g => new
                {
                    Theme = g.Key,
                    Items = g.Select(x => new
                    {
                        x.Product,
                        x.Quantity,
                        x.Amount
                    }).ToList(),
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .OrderBy(g => g.Theme)
                .ToList();

            // ---------- (C) EXTENSION ACTIVITIES ----------
            var extensionActivities = await GetExtensionActivitiesData(81, year, month);

            // ---------- (D) SUMMARY ----------
            var totalParticipants = trainingPrograms.Sum(p => p.NoOfParticipants);
            var totalProductionValue = production.Sum(p => p.TotalAmount);

            // ---------- (E) FINAL COMBINED RESULT ----------
            return Ok(new
            {
                UnitId = unitId,
                UnitName = "Institute of Baking Technology and Value Addition (IBT & VA)",
                TrainingPrograms = trainingPrograms,
                Production = production,
                ExtensionActivities = extensionActivities,
                Summary = new
                {
                    TotalParticipants = totalParticipants,
                    TotalProductionValue = totalProductionValue
                }
            });
        }

        // -------------------------------------------
        // 2️⃣ Theme Summary (optional)
        // -------------------------------------------
        [HttpGet("MonthlyThemeReports")]
        public async Task<IActionResult> GetMonthlyThemeReports(int unitId, int year, int month)
        {
            var programs = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Where(p => p.CreatedById == 81 &&
                            p.StartDate.Year == year &&
                            p.StartDate.Month == month)
                .ToListAsync();

            var grouped = programs
                .GroupBy(p => p.Category?.Name ?? p.CategoryOther ?? "Other")
                .Select(g => new
                {
                    Category = g.Key,
                    CompletedPrograms = g.Count(p => p.EndDate != DateOnly.MinValue && p.EndDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now),
                    OngoingPrograms = g.Count(p => p.StartDate != DateOnly.MinValue && p.StartDate.ToDateTime(TimeOnly.MinValue) <= DateTime.Now &&
                                                   (p.EndDate == DateOnly.MinValue || p.EndDate.ToDateTime(TimeOnly.MinValue) == default || p.EndDate.ToDateTime(TimeOnly.MinValue) >= DateTime.Now)),
                    StartedPrograms = g.Count(p => p.StartDate != DateOnly.MinValue && p.StartDate.Month == month),
                    //TotalParticipants = g.Sum(p => p.Reports !=null ? p.Reports.Count : 0),
                    UnitId = unitId
                })
                .OrderBy(x => x.Category)
                .ToList();

            return Ok(grouped);
        }

        // =======================================================
        // 3️⃣ EXTENSION ACTIVITIES ENDPOINT
        // =======================================================
        [HttpGet("ExtensionActivities")]
        public async Task<IActionResult> GetExtensionActivities(int trainerId, int year, int month)
        {
            var extensionActivities = await GetExtensionActivitiesData(trainerId, year, month);

            return Ok(new
            {
                TrainerId = trainerId,
                Year = year,
                Month = month,
                ExtensionActivities = extensionActivities
            });
        }

        // =======================================================
        // HELPER METHOD: Get Extension Activities Data
        // =======================================================
        private async Task<object> GetExtensionActivitiesData(int trainerId, int year, int month)
        {
            var startDateOnly = new DateOnly(year, month, 1); 
            var endDateOnly = startDateOnly.AddMonths(1);

            // 1. Method/Skill Demonstrations (Type = "Skill Development", Theme = "Method Demonstration")
            var skillDemos = await _context.IbtvaProgramDetails
                .Include(p => p.Type)
                .Include(p => p.Theme)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Type != null && p.Type.Name == "Skill Development" &&
                            p.Theme != null && (p.Theme.Name == "Method demonstration" || p.Theme.Name == "Skill demonstration") &&
                            p.ProgramType != null && p.ProgramType.Name == "Organized")
                .ToListAsync();

            var skillDemoMale = await _context.IbtvaParticipantDemographics
                .Where(d => skillDemos.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0));

            var skillDemoFemale = await _context.IbtvaParticipantDemographics
                .Where(d => skillDemos.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));

            // 2. Advisory Services - Face to Face and Telephone
            var advisoryServices = await _context.IbtvaAdvisoryServices
                .Include(p => p.ProgramDetails)
                .Where(a => a.ProgramDetails.CreatedById == trainerId &&
                            a.ProgramDetails.StartDate >= startDateOnly &&
                            a.ProgramDetails.StartDate < endDateOnly)
                .ToListAsync();

            var faceToFace = advisoryServices.Sum(a => a.NoOfFaceToFaceDiscussions);
            var phoneCalls = advisoryServices.Sum(a => a.NoOfPhoneCalls);
            var whatsappQueries = advisoryServices.Sum(a => a.NoOfAnsweredWhatsappQueries);

            // Breakdown by gender (assuming 40% male, 60% female for advisory - adjust as needed)
            var faceToFaceMale = (int)(faceToFace * 0.4);
            var faceToFaceFemale = faceToFace - faceToFaceMale;
            var phoneCallsMale = (int)(phoneCalls * 0.4);
            var phoneCallsFemale = phoneCalls - phoneCallsMale;
            var whatsappMale = (int)(whatsappQueries * 0.4);
            var whatsappFemale = whatsappQueries - whatsappMale;

            // 3. Exposure Visits (Category = "Exposure visit")
            var exposureOrganized = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && p.Category.Name == "Exposure visit" &&
                            p.ProgramType != null && p.ProgramType.Name == "Organized")
                .ToListAsync();

            var exposureParticipated = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && p.Category.Name == "Exposure visit" &&
                            p.ProgramType != null && p.ProgramType.Name == "Participated")
                .ToListAsync();

            var exposureParticipatedMale = await _context.IbtvaParticipantDemographics
                .Where(d => exposureParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0));

            var exposureParticipatedFemale = await _context.IbtvaParticipantDemographics
                .Where(d => exposureParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));

            // 4. Important Days
            var importantDaysOrganized = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && p.Category.Name == "Important events/ days/ programs" &&
                            p.ProgramType != null && p.ProgramType.Name == "Organized")
                .ToListAsync();

            var importantDaysParticipated = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && p.Category.Name == "Important events/ days/ programs" &&
                            p.ProgramType != null && p.ProgramType.Name == "Participated")
                .ToListAsync();

            var importantDaysParticipatedMale = await _context.IbtvaParticipantDemographics
                .Where(d => importantDaysParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0));

            var importantDaysParticipatedFemale = await _context.IbtvaParticipantDemographics
                .Where(d => importantDaysParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));

            // 5. Important Events (same as Important Days but could be tracked separately)
            // Reusing the same data for now
                

            // 6. Melas/Exhibitions
            var melasOrganized = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && (p.Category.Name == "Exhibition" || p.Category.Name == "Mela's") &&
                            p.ProgramType != null && p.ProgramType.Name == "Organized")
                .ToListAsync();

            var melasParticipated = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && (p.Category.Name == "Exhibition" || p.Category.Name == "Mela's") &&
                            p.ProgramType != null && p.ProgramType.Name == "Participated")
                .ToListAsync();

            var melasParticipatedMale = await _context.IbtvaParticipantDemographics
                .Where(d => melasParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0));

            var melasParticipatedFemale = await _context.IbtvaParticipantDemographics
                .Where(d => melasParticipated.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));

            // 7. Visitors to Institute - ENHANCED HANDLING
            var visitors = await _context.Services
                .Include(s => s.VisitorDetails)
                .Where(s => s.CreatedById == trainerId &&
                            s.Component == "IBT&VA" &&
                            s.Date >= startDateOnly &&
                            s.Date < endDateOnly &&
                            s.Category.Name == "visitors")
                .SelectMany(s => s.VisitorDetails)
                .ToListAsync();

            // Calculate male count: use demographic breakdown if available, otherwise use Male_Total
            var visitorsMale = visitors.Sum(v =>
            {
                // If demographic breakdown exists (any category has value > 0)
                var demographicMale = v.Male_SC + v.Male_ST + v.Male_OBC + v.Male_GEN;
                if (demographicMale > 0)
                    return demographicMale;
                // Otherwise use Male_Total (if it has a value)
                return v.Male_Total;
            });

            // Calculate female count: use demographic breakdown if available, otherwise use Female_Total
            var visitorsFemale = visitors.Sum(v =>
            {
                // If demographic breakdown exists (any category has value > 0)
                var demographicFemale = v.Female_SC + v.Female_ST + v.Female_OBC + v.Female_GEN;
                if (demographicFemale > 0)
                    return demographicFemale;
                // Otherwise use Female_Total (if it has a value)
                return v.Female_Total;
            });

            var totalVisitors = visitorsMale + visitorsFemale;

            // 8. Meetings Participated
            var meetings = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Include(p => p.ProgramType)
                .Where(p => p.CreatedById == trainerId &&
                            p.StartDate >= startDateOnly &&
                            p.StartDate < endDateOnly &&
                            p.Category != null && p.Category.Name == "meeting" &&
                            p.ProgramType != null && p.ProgramType.Name == "Participated")
                .ToListAsync();

            var meetingsMale = await _context.IbtvaParticipantDemographics
                .Where(d => meetings.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0));

            var meetingsFemale = await _context.IbtvaParticipantDemographics
                .Where(d => meetings.Select(s => s.Id).Contains(d.IbtvaProgramDetailsId ?? 0))
                .SumAsync(d => (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0));

            // Build the response object
            return new
            {
                MethodSkillDemonstrations = new
                {
                    NoOfProgrammes = skillDemos.Count,
                    NoOfParticipants = new
                    {
                        Male = skillDemoMale,
                        Female = skillDemoFemale,
                        Total = skillDemoMale + skillDemoFemale
                    },
                    Note = $"({skillDemos.Count} products demonstrated)"
                },
                AdvisoryServices = new
                {
                    FaceToFaceContact = new
                    {
                        Male = faceToFaceMale,
                        Female = faceToFaceFemale,
                        Total = faceToFace
                    },
                    TelephoneCalls = new
                    {
                        Male = phoneCallsMale,
                        Female = phoneCallsFemale,
                        Total = phoneCalls
                    }
                },
                ExposureVisits = new
                {
                    Organized = new
                    {
                        NoOfProgrammes = exposureOrganized.Count,
                        Participants = new { Male = 0, Female = 0, Total = 0 }
                    },
                    Participated = new
                    {
                        NoOfProgrammes = exposureParticipated.Count,
                        Participants = new
                        {
                            Male = exposureParticipatedMale,
                            Female = exposureParticipatedFemale,
                            Total = exposureParticipatedMale + exposureParticipatedFemale
                        }
                    }
                },
                ImportantDays = new
                {
                    Organized = new
                    {
                        NoOfProgrammes = importantDaysOrganized.Count,
                        Participants = new { Male = 0, Female = 0, Total = 0 }
                    },
                    Participated = new
                    {
                        NoOfProgrammes = importantDaysParticipated.Count,
                        Participants = new
                        {
                            Male = importantDaysParticipatedMale,
                            Female = importantDaysParticipatedFemale,
                            Total = importantDaysParticipatedMale + importantDaysParticipatedFemale
                        }
                    }
                },
                ImportantEvents = new
                {
                    Organized = new
                    {
                        NoOfProgrammes = 0,
                        Participants = new { Male = 0, Female = 0, Total = 0 }
                    },
                    Participated = new
                    {
                        NoOfProgrammes = importantDaysParticipated.Count,
                        Participants = new
                        {
                            Male = importantDaysParticipatedMale,
                            Female = importantDaysParticipatedFemale,
                            Total = importantDaysParticipatedMale + importantDaysParticipatedFemale
                        }
                    }
                },
                MelasExhibitions = new
                {
                    Organized = new
                    {
                        NoOfProgrammes = melasOrganized.Count,
                        Participants = new { Male = 0, Female = 0, Total = 0 }
                    },
                    Participated = new
                    {
                        NoOfProgrammes = melasParticipated.Count,
                        Participants = new
                        {
                            Male = melasParticipatedMale,
                            Female = melasParticipatedFemale,
                            Total = melasParticipatedMale + melasParticipatedFemale
                        }
                    }
                },
                VisitorsToInstitute = new
                {
                    Male = visitorsMale,
                    Female = visitorsFemale,
                    Total = totalVisitors
                },
                WhatsappQueriesAnswered = new
                {
                    Male = whatsappMale,
                    Female = whatsappFemale,
                    Total = whatsappQueries
                },
                AnyOther = new
                {
                    MeetingParticipated = new
                    {
                        NoOfProgrammes = meetings.Count,
                        Participants = new
                        {
                            Male = meetingsMale,
                            Female = meetingsFemale,
                            Total = meetingsMale + meetingsFemale
                        }
                    }
                }
            };
        }
    }
}