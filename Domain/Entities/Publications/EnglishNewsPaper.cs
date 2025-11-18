using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    public class EnglishNewsPaper : AuditableBaseEntity
    {
        public required string NewsPaperName { get; set; }
        public ICollection<PublicationEnglishNewsPaper> PublicationEnglishNewsPapers { get; set; } = [];
    }
}
