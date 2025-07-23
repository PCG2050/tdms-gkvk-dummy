using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class State:AuditableBaseEntity
    {
        public required string Name { get; set; }
        public ICollection<District> Districts { get; set; } = [];
    }
}
