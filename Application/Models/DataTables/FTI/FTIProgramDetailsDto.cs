using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables.FTI
{
    public class FTIProgramDetailsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Duration { get; set; }

        public List<FTIParticipantDemographicsDto>? ParticipantDemographics { get; set; }
        public List<FTIProgramContentAndResourcesDto>? ProgramContentAndResources { get; set; }
        public List<FTIAdvisoryServicesDto>? AdvisoryServices { get; set; }
        public List<FTIReportDto>? Reports { get; set; }
        public List<FTIRecommendationDto>? Recommendations { get; set; }
    }

    public class FTIParticipantDemographicsDto
    {
        public int Id { get; set; }
        public int FTIProgramDetailsID { get; set; }
        public int? Participant { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? Total { get; set; }
    }

    public class FTIProgramContentAndResourcesDto
    {
        public int Id { get; set; }
        public int FTIProgramDetailsID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<FTIResourcePersonDto>? ResourcePersons { get; set; }
        public List<FTITopicsCoveredInClassDto>? TopicsCovered { get; set; }
        public List<FTITeachingAidsDevelopedDto>? TeachingAids { get; set; }
    }

    public class FTIResourcePersonDto
    {
        public int Id { get; set; }
        public int FTIProgramContentAndResourcesID { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class FTITopicsCoveredInClassDto
    {
        public int Id { get; set; }
        public int FTIProgramContentAndResourcesID { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class FTITeachingAidsDevelopedDto
    {
        public int Id { get; set; }
        public int FTIProgramContentAndResourcesID { get; set; }
        public string? TypeOfAidDeveloped { get; set; }
        public string? Other { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class FTIAdvisoryServicesDto
    {
        public int Id { get; set; }
        public int FTIProgramDetailsID { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }

    public class FTIReportDto
    {
        public int Id { get; set; }
        public int FTIProgramDetailsID { get; set; }
        public string? ReportingYear { get; set; }
        public string? UploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
    }

    public class FTIRecommendationDto
    {
        public int Id { get; set; }
        public int FTIProgramDetailsID { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }
}
