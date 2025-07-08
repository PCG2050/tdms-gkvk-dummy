using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FtiOtherActivity: ReportEntryBaseEntity
    {
        public required string ActivityDetails { get; set; }
    }
}
