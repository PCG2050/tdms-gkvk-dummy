using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
   
        public class DeuProgramContentAndResources : AuditableBaseEntity
        {
            // FK to parent program
           
            public int? DeuProgramDetailsId { get; set; }

            [ForeignKey(nameof(DeuProgramDetailsId))]
            public DeuProgramDetails ProgramDetails { get; set; }

            // Optionally add fields describing the content/resources record
            [MaxLength(250)]
            public string? Title { get; set; }

            [MaxLength(500)]
            public string? Description { get; set; }

            // Navigation children
            public ICollection<DeuResourcePerson> ResourcePersons { get; set; }
            public ICollection<DeuTopicsCoveredInClass> TopicsCovered { get; set; }
            public ICollection<DeuTeachingAidsDeveloped> TeachingAids { get; set; }
        }
    

}
