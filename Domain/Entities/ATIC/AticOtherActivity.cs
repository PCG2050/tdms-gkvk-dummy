using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ATIC
{
    public class AticOtherActivity:ReportEntryBaseEntity
    {
        public required string ActivityDetails { get; set; }
    }
}
