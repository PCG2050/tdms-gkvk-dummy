using Domain.Entities.DEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuProgramContentAndResources : AuditableBaseEntity
    {
        public int? EeuProgramDetailsId { get; set; }

        [ForeignKey(nameof(EeuProgramDetailsId))]
        public DeuProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        // Navigation children
        public ICollection<EeuResourcePerson> ResourcePersons { get; set; }
        public ICollection<EeuTopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<EeuTeachingAidsDeveloped> TeachingAids { get; set; }
    }
}
