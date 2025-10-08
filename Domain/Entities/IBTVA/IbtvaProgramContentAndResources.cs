using Domain.Entities.DEU;
using Domain.Entities.EEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IBTVA
{
    public class IbtvaProgramContentAndResources : AuditableBaseEntity
    {
        public int? IbtvaProgramDetailsId { get; set; }

        [ForeignKey(nameof(IbtvaProgramDetailsId))]
        public IbtvaProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        // Navigation children
        public ICollection<IbtvaResourcePerson> ResourcePersons { get; set; }
        public ICollection<IbtvaTopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<IbtvaTeachingAidsDeveloped> TeachingAids { get; set; }
    }
}
