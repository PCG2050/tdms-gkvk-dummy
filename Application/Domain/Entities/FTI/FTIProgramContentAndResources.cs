using Domain.Entities.FIU;
using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FTIProgramContentAndResources : AuditableBaseEntity
    {
        // FK to parent program
        [Required]
        public int? FTIProgramDetailsID { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation children
        public ICollection<FTIResourcePerson> ResourcePersons { get; set; }
        public ICollection<FTITopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<FTITeachingAidsDeveloped> TeachingAids { get; set; }
    }
}
