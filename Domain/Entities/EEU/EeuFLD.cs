using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuFLD:ReportEntryBaseEntity
    {
        public required string Title { get; set; }
        public required string Crop { get; set; }
        public double Area { get; set; }
        public int TrialMaleScStCount { get; set; }
        public int TrialMaleGenCount { get; set; }
        public int TrialFemaleScStCount { get; set; }
        public int TrailFemalGenCount { get; set; }
        public double YieldDemo { get; set; }
        public double YieldCheck { get; set; }
        public double PercentIncreaseInYield { get; set; }
    }
}
