using Application.Interface;
using Domain.Entities;
using Domain.Entities.ASM;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.EEU;
using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.GenericTables;
using Domain.Entities.IBTVA;
using Domain.Entities.Junction;
using Domain.Entities.MasterData;
using Domain.Entities.STU;
using Infrastructure.DbContext.Configuration;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        #region MasterDataTables
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<ProgramCategory> ProgramCategories { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<EventName> EventNames { get; set; }
        public DbSet<InfoType>InfoTypes { get; set; }

        public DbSet<Collaborator> Collaborators { get; set; }

        public DbSet<CollaborativeProgramOption> CollaborativeProgramOptions { get; set; }
        public DbSet<VillageAdoptiveProgram> VillageAdoptivePrograms { get; set; }

        public DbSet<TargetFarmer> TargetFarmers { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<ThematicArea> ThematicAreas { get; set; }
        public DbSet<SponsoredOrganization> SponsoredOrganizations { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SourceOfFund> SourcesOfFunds { get; set; }
        public DbSet<Status> Statuses { get; set; }       
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipatedSource> ParticipatedSources { get; set; }

        public DbSet<ParticipantDealer> ParticipantDealer { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }

        public DbSet<TypeOfAid> TypeOfAids { get; set; }

        public DbSet<OFTResult> OFTResults { get; set; }

        public DbSet<FLDResult> FLDResults { get; set; }

        public DbSet<PublicationCategory> PublicationCategories { get; set; }

        public DbSet<KannadaMagazine> KannadaMagazines { get; set; }


        public DbSet<EnglishMagazine> EnglishMagazines { get; set; }
        public DbSet<KannadaNewsPaper> KannadaNewsPapers { get; set; }
        public  DbSet<EnglishNewsPaper> EnglishNewsPapers { get; set; }

        public DbSet<ExtensionWork> ExtensionWorks { get; set; }
        public DbSet<NominationType> NominationTypes { get; set; }
        public DbSet<NominationCategory> NominationCategories { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        public DbSet<ServicesCategory> ServicesCategories { get; set; }

        public DbSet<RelatedTo> RelatedTos { get; set; }
        public DbSet<Particular> Particulars { get; set; }

        public DbSet<ModeOutreach> ModeOutreaches { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        public DbSet<ServiceTheme> ServiceThemes { get; set; }

        public DbSet<QuantityUnit> QuantityUnits { get; set; }
        public  DbSet<Visitor> Visitors { get; set; }

        public DbSet<FIUActivity> FIUActivities { get; set; }

        #endregion
        #region GenericTables
        #region Publications
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublisherDetails> PublisherDetails { get; set; }
        public DbSet<ExtensionLiterature> ExtensionLiteratures { get; set; }
        #endregion

        #region NominationRewards
        public DbSet<NominationReward> NominationRewards { get; set; }
        public DbSet<NominationRewardIFSFarmer> NominationRewardIFSFarmers { get; set; }
        public DbSet<NominationRewardFarmerInnovation> NominationRewardFarmerInnovations { get; set; }
        public DbSet<NominationRewardOrganicFarmer> NominationRewardOrganicFarmers { get; set; }
        public DbSet<NominationRewardIFSEnterpreneur> NominationRewardIFSEntrepreneurs { get; set; }
        public DbSet<NominationRewardEntrepreneurInnovation> NominationRewardEntrepreneurInnovations { get; set; }
        public DbSet<NominationRewardOrganicEntrepreneur> NominationRewardOrganicEntrepreneurs { get; set; }
        #endregion
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

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasOne<PublisherDetails>()
                      .WithOne(p => p.Publication)
                      .HasForeignKey<PublisherDetails>(p => p.PublicationId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(p => p.ExtensionLiteratures)
                      .WithOne(e => e.Publication)
                      .HasForeignKey(e => e.PublicationId)
                      .OnDelete(DeleteBehavior.SetNull);
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
