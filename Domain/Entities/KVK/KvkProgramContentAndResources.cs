using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkProgramContentAndResources : AuditableBaseEntity
    {
       

        // FK to parent program
        [Required]
        public int KVKProgramDetailsId { get; set; }

        [ForeignKey(nameof(KVKProgramDetailsId))]
        public TableKVKProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation children
        public ICollection<KvkResourcePerson> ResourcePersons { get; set; }
        public ICollection<KvkTopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<KvkTeachingAidsDeveloped> TeachingAids { get; set; }

    }
}
