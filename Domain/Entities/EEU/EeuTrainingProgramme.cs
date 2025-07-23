using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuTrainingProgramme:ReportEntryBaseEntity
    {
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
    }
}
