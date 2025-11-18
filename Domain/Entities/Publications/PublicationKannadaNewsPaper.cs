using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    /// <summary>
    /// Junction table for many-to-many relationship between Publication and KannadaNewsPaper
    /// </summary>
    public class PublicationKannadaNewsPaper
    {
        public int PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        public int KannadaNewsPaperId { get; set; }
        public KannadaNewsPaper KannadaNewsPaper { get; set; } = null!;
    }
}
