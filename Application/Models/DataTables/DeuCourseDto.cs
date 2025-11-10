//using Domain.Entities.DEU;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Models.DataTables
//{
//    public class DeuCourseDto : DeuCourseCreateDto, IBaseEntryDto
//    {
//        public int Id { get; set; }
//        public DateTimeOffset CreatedAt { get; set; }
//        public DateTimeOffset? UpdatedAt { get; set; }
//    }

//    public class DeuCourseCreateDto : BaseEntryCreateDto
//    {
//        public DeuCourseType Type { get; set; }
//        public required string Name { get; set; }
//        public int CandidateAdmittedCount { get; set; }
//        public int CandidateAttendedExamCount { get; set; }
//        public int CandidatePassedCount { get; set; }
//    }

//    public class DeuCourseUpdateDto : BaseEntryUpdateDto 
//    {
//        public DeuCourseType? Type { get; set; }
//        public string? Name { get; set; }
//        public int? CandidateAdmittedCount { get; set; }
//        public int? CandidateAttendedExamCount { get; set; }
//        public int? CandidatePassedCount { get; set; }
//    }
//}
