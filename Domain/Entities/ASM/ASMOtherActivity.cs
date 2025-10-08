using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ASM
{
    public class AsmOtherActivity : AuditableBaseEntity
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string?  UploadMediaUrl { get; set; }
    }
}
