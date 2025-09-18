using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    public class Region : AuditableBaseEntity
    {
        public required string RegionName { get; set; }
        public ICollection<Publication> Publications { get; set; } = [];
    }
}
