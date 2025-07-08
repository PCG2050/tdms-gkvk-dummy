using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UnitUser:AuditableBaseEntity
    {
        public int UnitId { get; set; }
        public int UserId { get; set; }
        public Unit Unit { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
