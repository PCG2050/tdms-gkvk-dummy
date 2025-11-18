using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    public class KannadaMagazine : AuditableBaseEntity
    {
        public required string MagazineName { get; set; }
        public ICollection<PublicationKannadaMagazine> PublicationKannadaMagazines { get; set; } = [];
    }
}
