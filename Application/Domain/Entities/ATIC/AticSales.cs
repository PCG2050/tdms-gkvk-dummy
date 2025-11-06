using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ATIC
{
    public class AticSales:ReportEntryBaseEntity
    {
        public required string Details { get; set; }
        public AticSalesQuantityType QuantityType { get; set; }
        public double Quantity { get; set; }
    }
    public enum AticSalesQuantityType
    {
        NUMBER,
        KILOGRAM,
        UNIT
    }
}
