using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FIUProgramActivity : AuditableBaseEntity
    {
        public int? FIUActivitiesId { get; set; }

        public FIUActivity? FIUActivities { get; set; } 

        public int? Number { get; set; }

        public string? UploadMediaUrl { get; set; }

    }
}
