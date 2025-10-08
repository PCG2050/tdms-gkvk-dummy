using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables.Service
{
    public class VisitorDetail : AuditableBaseEntity
    {
        // Foreign key to Service
        public int ServiceId { get; set; }
        [JsonIgnore]
        public TblService? TblService { get; set; }

        public string? Name { get; set; }

        public int? MobileNo { get; set; }
        public DateOnly? Date { get; set; }
        public string? Location { get; set; }

        public string? Purpose { get; set; }

        public string? PurposeOfVisit { get; set; }

    }
}
