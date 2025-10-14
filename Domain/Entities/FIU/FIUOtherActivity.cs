using Domain.Entities.Junction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FIUOtherActivity : AuditableBaseEntity
    {
        [Required]
        public int UnitLocationId { get; set; }

        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation? UnitLocation { get; set; }  // Nullable

        [Required]
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }  // Nullable
        public string? Title {  get; set; }

        public string? Description { get; set; }

        public string? UploadMediaUrl { get; set; }

    }
}
