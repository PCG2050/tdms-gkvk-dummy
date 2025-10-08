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
        public TblService? TblService { get; set; }
        [Range(0, 100000000.00)]
        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expenditure { get; set; }
        public double ClosingBalance { get; set; }
    }
}
