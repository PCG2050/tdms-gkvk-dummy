using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Publications
{
    /// <summary>
    /// Junction table for many-to-many relationship between Publication and EnglishNewsPaper
    /// </summary>
    public class PublicationEnglishNewsPaper
    {
        public int PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        public int EnglishNewsPaperId { get; set; }
        public EnglishNewsPaper EnglishNewsPaper { get; set; } = null!;
    }
}
