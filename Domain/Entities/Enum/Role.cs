using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Enum
{
    public enum Role
    {
        UNDEFINED,
        TRAINER,
        ADMIN,
        SUPERADMIN
    }

    public static class RoleString
    {
        public const string Trainer = "TRAINER";
        public const string Admin = "ADMIN";
        public const string SuperAdmin = "SUPERADMIN";
    }
}
