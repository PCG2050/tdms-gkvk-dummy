using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.NAEP
{
    public class NaepDetails:ReportEntryBaseEntity
    {
        public required string Particulars { get; set; }
        public DateOnly Date { get; set; }
        public required string Place { get;set; }
        public int ProgrammeCount { get; set; }
        public int ParticipantCount { get; set; }
    }
}
