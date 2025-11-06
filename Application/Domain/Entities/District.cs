using System;


namespace Domain.Entities
{
    public class District:BaseEntity
    {
        public int StateId { get; set; }
        public required string Name { get; set; }
        public State State { get; set; } = null!;
    }
}
