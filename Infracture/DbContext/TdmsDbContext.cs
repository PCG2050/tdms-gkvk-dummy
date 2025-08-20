using Domain.Entities;
using Domain.Entities.ASM;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.EEU;
using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.IBTVA;
using Domain.Entities.Junction;
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
        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Unit> Units { get; set; }
        
        public DbSet<FtiTrainingProgram> FtiTrainingPrograms { get; set; }
        public DbSet<FtiOtherActivity> FtiOtherActivities { get; set; }
        #region STU
        public DbSet<StuTrainingProgramme> StuTrainingProgrammes { get; set; }
        public DbSet<StuSponsoredTrainingProgramme> StuSponsoredTrainingProgrammes { get; set; }
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
        #region Juntions
        public DbSet<OrganizationUnitLocation> OrganizationUnitLocations { get; set; }
        public DbSet<TrainerAssignment> UnitTrainers { get; set; }
        public DbSet<UnitHeadAssignment> UnitHeadAssignments { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            new OrganizationTypeConfiguration().Configure(modelBuilder.Entity<Organization>());
            new UserTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new AticSalesTypeConfiguration().Configure(modelBuilder.Entity<AticSales>());
            new DeuCourseTypeConfiguration().Configure(modelBuilder.Entity<DeuCourse>());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        .Where(t => typeof(AuditableBaseEntity).IsAssignableFrom(t.ClrType)
                    || typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property(nameof(ReportEntryBaseEntity.CreatedAt))
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("SYSUTCDATETIME()"); // for SQL Server/SQLite 
            }

            //configure UnitHeadAssignment and assign them to location with 
            modelBuilder.Entity<UnitHeadAssignment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.UnitHead)
                    .WithMany(u => u.UnitHeadAssignments)
                    .HasForeignKey(e => e.UnitHeadId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UnitLocation)
                    .WithMany(ul => ul.UnitHeadAssignments)
                    .HasForeignKey(e => e.UnitLocationId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ensure a UnitHead can only be assigned once per UnitLocation
                entity.HasIndex(e => new { e.UnitHeadId, e.UnitLocationId })
                    .IsUnique()
                    .HasDatabaseName("IX_UnitHeadAssignment_UnitHead_UnitLocation");
            });

           

            modelBuilder.Entity<TrainerAssignment>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_TrainerAssignment_ValidSupervisor", 
                 "([TrainerId] IS NULL OR EXISTS (SELECT 1 FROM Users u WHERE u.Id = [TrainerId] AND (u.SupervisorId IS NULL OR EXISTS (SELECT 1 FROM Users s WHERE s.Id = u.SupervisorId AND s.Role = 'UNITHEAD'))))"));
            });

        }
    }
}
