using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    /// <summary>
    /// Junction table for many-to-many relationship between Publication and EnglishMagazine
    /// </summary>
    public class PublicationEnglishMagazine
    {
        public int PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        public int EnglishMagazineId { get; set; }
        public EnglishMagazine EnglishMagazine { get; set; } = null!;
    }
}
