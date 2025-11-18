using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    /// <summary>
    /// Junction table for many-to-many relationship between Publication and KannadaMagazine
    /// </summary>
    public class PublicationKannadaMagazine
    {
        public int PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        public int KannadaMagazineId { get; set; }
        public KannadaMagazine KannadaMagazine { get; set; } = null!;
    }
}
