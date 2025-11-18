using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    public class KannadaNewsPaper : AuditableBaseEntity
    {
        public required string NewsPaperName { get; set; }
        public ICollection<PublicationKannadaNewsPaper> PublicationKannadaNewsPapers { get; set; } = [];
    }
}
