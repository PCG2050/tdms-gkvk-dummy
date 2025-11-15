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
        public int? FTIProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FTIProgramDetailsId))]
        public FTIProgramDetails? ProgramDetails { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<FTIResourcePerson>? ResourcePersons { get; set; }
        public ICollection<FTITopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<FTITeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
