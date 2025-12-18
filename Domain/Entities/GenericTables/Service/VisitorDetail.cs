using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        // Male counts
        public int Male_SC { get; set; } = 0;
        public int Male_ST { get; set; } = 0;
        public int Male_OBC { get; set; } = 0;
        public int Male_GEN { get; set; } = 0;

        // Female counts
        public int Female_SC { get; set; } = 0;
        public int Female_ST { get; set; } = 0;
        public int Female_OBC { get; set; } = 0;
        public int Female_GEN { get; set; } = 0;

        public int Male_Total { get; set; } 
        public int Female_Total { get; set; } 
        public int Total { get; set; } 



    }
}
