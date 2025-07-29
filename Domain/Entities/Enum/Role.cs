namespace Domain.Entities.Enum
{
    public enum Role
    {
        UNDEFINED,
        TRAINER,
        UNITHEAD,
        ADMIN,
        SUPERADMIN
    }

    public static class RoleString
    {
        public const string Trainer = "TRAINER";
        public const string UnitHead = "UNITHEAD";
        public const string Admin = "ADMIN";
        public const string SuperAdmin = "SUPERADMIN";
    }
}
