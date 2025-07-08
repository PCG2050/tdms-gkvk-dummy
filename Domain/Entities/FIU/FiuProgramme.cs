using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FiuProgramme:ReportEntryBaseEntity
    {
        public required FiuProgrammeType Type { get; set; }
        public bool IsOther { get; set; }
        public string? OtherName { get; set; }
        public int Count { get; set; }
    }
}
