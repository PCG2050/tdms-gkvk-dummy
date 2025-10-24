using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables.ASM
{
    public class ASMVisitorDetailsDto
    {
        public string InstituteName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string? Actions { get; set; }
        public int? StatusId { get; set; }
    }
}
