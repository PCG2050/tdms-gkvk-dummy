using Domain.Entities.Junction;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FIUProgramActivity : AuditableBaseEntity
    {
        [Required]
        public int UnitLocationId { get; set; }

        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation? UnitLocation { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }
        public int? FIUActivitiesId { get; set; }

        public FIUActivity? FIUActivities { get; set; } 

        public int? Number { get; set; }

        public string? UploadMediaUrl { get; set; }

    }
}
