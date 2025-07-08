using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
    public class DeuCourse:ReportEntryBaseEntity
    {
        public DeuCourseType Type { get; set; }
        public required string Name { get; set; }
        public int CandidateAdmittedCount { get; set; }
        public int CandidateAttendedExamCount { get; set; }
        public int CandidatePassedCount { get; set; }
    }
    public enum DeuCourseType
    {
        DIPLOMA,
        CERTIFICATE
    }
}
