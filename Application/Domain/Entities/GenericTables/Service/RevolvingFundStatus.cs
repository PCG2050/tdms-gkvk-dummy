using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables.Service
{
    public class RevolvingFundStatus : AuditableBaseEntity
    {
      

        // Foreign key to Service
        public int ServiceId { get; set; }
        [JsonIgnore]      
        [ForeignKey(nameof(ServiceId))]
        public TblService? Service { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OpeningBalance { get; set; }

        public int Receipt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Expenditure { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public double ClosingBalance { get; set; }
    }
}
