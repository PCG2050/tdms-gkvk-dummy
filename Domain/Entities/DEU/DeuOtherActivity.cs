using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
    public class DeuOtherActivity:ReportEntryBaseEntity
    {
        public required string ActivityDetails { get; set; }
    }
}
