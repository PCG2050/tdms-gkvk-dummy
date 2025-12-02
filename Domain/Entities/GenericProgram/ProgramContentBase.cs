using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.GenericProgram
{
    /// <summary>
    /// Generic base entity for program content and resources
    /// Contains common fields and navigation properties
    /// </summary>
    public abstract class ProgramContentBase<TProgram, TResourcePerson, TTopicsCovered, TTeachingAids> : AuditableBaseEntity
        where TProgram : class
        where TResourcePerson : class
        where TTopicsCovered : class
        where TTeachingAids : class
    {
        public int? ProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ProgramDetailsId))]
        public TProgram? ProgramDetails { get; set; }

        public int? UnitLocationId { get; set; }
        public int? OrganizationId { get; set; }

        // Navigation to child entities
        public ICollection<TResourcePerson>? ResourcePersons { get; set; }
        public ICollection<TTopicsCovered>? TopicsCovered { get; set; }
        public ICollection<TTeachingAids>? TeachingAids { get; set; }
    }
}
