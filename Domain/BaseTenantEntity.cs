using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class BaseTenantEntity:AuditableBaseEntity
    {
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
