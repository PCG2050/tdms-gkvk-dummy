using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ATIC
{
    public class AticAdvisoryService:ReportEntryBaseEntity
    {
        public required string ServiceType { get; set; }
        public int ServiceCount { get; set; }
        public int BeneficiaryCount { get; set; }
    }
}
