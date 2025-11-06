using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IBTVA
{
    public class IbtavOtherActivity:ReportEntryBaseEntity
    {
        public required string ActicityDetails { get; set; }
    }
}
