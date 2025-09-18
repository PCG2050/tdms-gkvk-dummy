using Application.Interface;
using Domain.Entities;
using Domain.Entities.ASM;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.EEU;
using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.IBTVA;
using Domain.Entities.Junction;
using Domain.Entities.Publications;
using Domain.Entities.STU;
using Infrastructure.DbContext.Configuration;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.DbContext
{
    public class TdmsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public TdmsDbContext(DbContextOptions<TdmsDbContext> options, IHttpContextAccessor  httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
          
        }
        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Unit> Units { get; set; }


        public DbSet<FtiTrainingProgram> FtiTrainingPrograms { get; set; }
        public DbSet<FtiOtherActivity> FtiOtherActivities { get; set; }
        #region Publicaitons
        public DbSet<Category> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ExtensionLiterature> ExtensionLiteratures { get; set; }
        public DbSet<Publication> Publications { get; set; }
        #endregion
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

        // Get current user ID directly from HTTP context to avoid circular dependency
        private int? GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated != true)               
            return null;
             
            var userIdClaim = httpContext.User.FindFirst("UserId")?.Value
                           ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }

        //Override SaveChanges to automatically handle audit fields
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var currentUserId = GetCurrentUserId();
            var now = DateTimeOffset.UtcNow;
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedById = currentUserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        entry.Entity.UpdatedById = currentUserId;

                        //Prevent modification of CreatedAt and createdById
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Property(x => x.CreatedById).IsModified = false;

                        //mark updatedAt and UpdateId as modified
                        entry.Property(x => x.UpdatedAt).IsModified = true;
                        entry.Property(x => x.UpdatedById).IsModified = true;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        //prevent Modification of CreatedAt

                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Property(x => x.UpdatedAt).IsModified = true;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Publication configurations
            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configure relationships
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Publications)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.Mode)
                    .WithMany(m => m.Publications)
                    .HasForeignKey(p => p.ModeId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.Region)
                    .WithMany(r => r.Publications)
                    .HasForeignKey(p => p.RegionId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.Source)
                    .WithMany(s => s.Publications)
                    .HasForeignKey(p => p.SourceId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.ExtensionLiterature)
                    .WithMany(el => el.Publications)
                    .HasForeignKey(p => p.ExtensionLiteratureId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Master data configurations
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.CategoryName).IsUnique();
                entity.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.SourceName).IsUnique();
                entity.Property(e => e.SourceName).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Mode>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ModeName).IsUnique();
                entity.Property(e => e.ModeName).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RegionName).IsUnique();
                entity.Property(e => e.RegionName).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<TrainerAssignment>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainerAssignment>()
                .HasOne(t => t.UpdatedBy)
                .WithMany()
                .HasForeignKey(t => t.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UpdatedBy)
                .WithMany()
                .HasForeignKey(u => u.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            new OrganizationTypeConfiguration().Configure(modelBuilder.Entity<Organization>());
            new UserTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new AticSalesTypeConfiguration().Configure(modelBuilder.Entity<AticSales>());
            new DeuCourseTypeConfiguration().Configure(modelBuilder.Entity<DeuCourse>());

            //Configuring Defaults for createdAt only (UpdatedAt will be handled by savechanges override) 
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        .Where(t => typeof(AuditableBaseEntity).IsAssignableFrom(t.ClrType)
                    || typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(ReportEntryBaseEntity.CreatedAt))
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("SYSUTCDATETIME()"); // for SQL Server/SQLite
            
                //Dont Set for UpdateAt - it should only be set on updates
                //if(typeof(AuditableBaseEntity).IsAssignableFrom(entityType.ClrType) ||
                //        typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                //{
                //    modelBuilder.Entity(entityType.ClrType)
                //        .Property("UpdatedAt")
                //        .ValueGeneratedOnUpdate();
                //}
                
            }
        }
    }
}
