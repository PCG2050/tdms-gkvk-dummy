using Domain.Entities.GenericTables;
using Domain.Entities.MasterData;
using System.Text.Json.Serialization;

namespace Domain.Entities.Junction
{
    /// <summary>
    /// Junction table for Publication and KannadaNewsPaper (many-to-many)
    /// </summary>
    public class PublicationKannadaNewsPaper : AuditableBaseEntity
    {
        public int PublicationId { get; set; }
        public int KannadaNewsPaperId { get; set; }

        [JsonIgnore]
        public Publication Publication { get; set; } = null!;

        [JsonIgnore]
        public KannadaNewsPaper KannadaNewsPaper { get; set; } = null!;
    }

    /// <summary>
    /// Junction table for Publication and EnglishNewsPaper (many-to-many)
    /// </summary>
    public class PublicationEnglishNewsPaper : AuditableBaseEntity
    {
        public int PublicationId { get; set; }
        public int EnglishNewsPaperId { get; set; }

        [JsonIgnore]
        public Publication Publication { get; set; } = null!;

        [JsonIgnore]
        public EnglishNewsPaper EnglishNewsPaper { get; set; } = null!;
    }

    /// <summary>
    /// Junction table for Publication and KannadaMagazine (many-to-many)
    /// </summary>
    public class PublicationKannadaMagazine : AuditableBaseEntity
    {
        public int PublicationId { get; set; }
        public int KannadaMagazineId { get; set; }

        [JsonIgnore]
        public Publication Publication { get; set; } = null!;

        [JsonIgnore]
        public KannadaMagazine KannadaMagazine { get; set; } = null!;
    }

    /// <summary>
    /// Junction table for Publication and EnglishMagazine (many-to-many)
    /// </summary>
    public class PublicationEnglishMagazine : AuditableBaseEntity
    {
        public int PublicationId { get; set; }
        public int EnglishMagazineId { get; set; }

        [JsonIgnore]
        public Publication Publication { get; set; } = null!;

        [JsonIgnore]
        public EnglishMagazine EnglishMagazine { get; set; } = null!;
    }
}
