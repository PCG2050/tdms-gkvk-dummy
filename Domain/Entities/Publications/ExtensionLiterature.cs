using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
  
    public class ExtensionLiterature : AuditableBaseEntity
    {
        public DateOnly? ExtensionDate { get; set; }
        public string? PublicationName { get; set; }
        public int NumberSold { get; set; }
        public int TotalFarmers { get; set; }
        public ICollection<Publication> Publications { get; set; } = [];
    }
}
