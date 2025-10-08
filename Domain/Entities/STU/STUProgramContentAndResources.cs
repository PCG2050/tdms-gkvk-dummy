using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.STU
{
    public class STUProgramContentAndResources : AuditableBaseEntity
    {
        // FK to parent program
        [Required]
        public int STUProgramDetailsID { get; set; }

        [ForeignKey(nameof(STUProgramDetailsID))]
        public STUProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation children
        public ICollection<STUResourcePerson> ResourcePersons { get; set; }
        public ICollection<STUTopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<STUTeachingAidsDeveloped> TeachingAids { get; set; }
    }
}
