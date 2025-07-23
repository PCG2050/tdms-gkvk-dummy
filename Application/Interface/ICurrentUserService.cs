using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        Role Role { get; }
        int OrganizationId { get; }
    }
}
