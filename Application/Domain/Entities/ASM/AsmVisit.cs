using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ASM
{
    public class AsmVisit:ReportEntryBaseEntity
    {
        public required string OrganizationName { get; set; }
        public int VisitorCount { get; set; }
    }
}
