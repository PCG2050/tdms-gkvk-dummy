using Domain.Entities;
using Domain.Entities.ASM;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.EEU;
using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.IBTVA;
using Domain.Entities.STU;
using Infrastructure.DbContext.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class TdmsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TdmsDbContext(DbContextOptions<TdmsDbContext> options):base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FtiTrainingProgram> FtiTrainingPrograms { get; set; }
        public DbSet<FtiOtherActivity> FtiOtherActivities { get; set; }
        #region STU
        public DbSet<StuTrainingProgramme> StuTrainingProgrammes { get; set; }
        public DbSet<StuSponseredTrainingProgramme> StuSponseredTrainingProgrammes { get; set; }
        public DbSet<StuOtherActivity> StuOtherActivities { get; set; }
        public DbSet<DaesiProgramme> DaesiProgrammes { get; set; }
        public DbSet<DaesiOtherActivity> DaesiOtherActivities { get; set; }
        #endregion
        #region FIU
        public DbSet<FiuProgrammeType> FiuProgrammeTypes {  get; set; }
        public DbSet<FiuProgramme> FiuProgrammes { get; set; }
        #endregion
        #region IBT&VA
        public DbSet<IbtvaProgramme> IbtvaProgrammes { get; set; }
        public DbSet<IbtavOtherActivity> IbtavOtherActivities { get; set; }
        #endregion
        #region ATIC
        public DbSet<AticSales> AticSales { get; set; }
        public DbSet<AticAdvisoryService> AticAdvisoryServices { get; set; }
        public DbSet<AticOtherActivity> AticOtherActivities { get; set; }
        #endregion
        #region DEU
        public DbSet<DeuCourse> DeuCourses { get; set; }
        public DbSet<DeuOtherActivity> DeuOtherActivities { get; set; }
        #endregion
        #region ASM
        public DbSet<AsmVisit> AsmVisits {  get; set; }
        #endregion
        #region EEU
        public DbSet<EeuOFT> EeuOFTs { get; set; }
        public DbSet<EeuFLD> EeuFLDs { get; set; }
        public DbSet<EeuTrainingProgramme> EeuTrainingProgrammes { get; set; }
        public DbSet<EeuOtherActivity> EeuOtherActivities { get; set; }
        #endregion
        public DbSet<UnitUser> UnitUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new OrganizationTypeConfiguration().Configure(modelBuilder.Entity<Organization>());
            new UserTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new AticSalesTypeConfiguration().Configure(modelBuilder.Entity<AticSales>());
            new DeuCourseTypeConfiguration().Configure(modelBuilder.Entity<DeuCourse>());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        .Where(t => typeof(AuditableBaseEntity).IsAssignableFrom(t.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property(nameof(ReportEntryBaseEntity.CreatedAt))
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("SYSUTCDATETIME()"); // for SQL Server/SQLite
            }
        }
    }
}
