namespace Domain.Entities
{
    public class Unit:AuditableBaseEntity
    {
        public required string Name { get; set; }
        /// <summary>
        /// For cases when a unit has sub tables, each subtable should be mapped to the main Unit
        /// </summary>
        public int? ParentUnitId {  get; set; }
        public Unit? ParentUnit { get; set; }
        
        [JsonIgnore]
        public ICollection<OrganizationUnitLocation> OrganizationUnits { get; set; } = [];
    }
}
