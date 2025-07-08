using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuOtherActivity:ReportEntryBaseEntity
    {
        public required string ActivityDetails { get; set; }
    }
}
