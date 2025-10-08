using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FIUOtherActivity : AuditableBaseEntity
    {
        public string? Title {  get; set; }

        public string? Description { get; set; }

        public string? UploadMediaUrl { get; set; }

    }
}
