using Application.Interface;
using Domain.Entities;
using Domain.Entities.ASM;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.EEU;
using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.GenericTables;
using Domain.Entities.GenericTables.ConsultingAndSocialMediaService;
using Domain.Entities.GenericTables.Service;
using Domain.Entities.IBTVA;
namespace Infrastructure.DbContext
{
    public class TdmsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
      

        public TdmsDbContext(DbContextOptions<TdmsDbContext> options) : base(options)
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
       
        public DbSet<DaesiProgramme> DaesiProgrammes { get; set; }
        public DbSet<DaesiOtherActivity> DaesiOtherActivities { get; set; }
        public DbSet<StuProgramDetails> StuProgramDetails { get; set; }
        public DbSet<StuParticipantDemographics> StuParticipantDemographics { get; set; }
        public DbSet<StuProgramContentAndResources> StuProgramContentAndResources { get; set; }
        public DbSet<StuResourcePerson> StuResourcePersons { get; set; }
        public DbSet<StuTopicsCoveredInClass> StuTopicsCoveredInClass { get; set; }
        public DbSet<StuTeachingAidsDeveloped> StuTeachingAidsDeveloped { get; set; }
        public DbSet<StuAdvisoryServices> StuAdvisoryServices { get; set; }
        public DbSet<StuReport> StuReports { get; set; }
        public DbSet<StuRecommendation> StuRecommendations { get; set; }

        #endregion
        #region FIU
        public DbSet<FiuProgrammeType> FiuProgrammeTypes {  get; set; }
        public DbSet<FiuProgramme> FiuProgrammes { get; set; }

        public DbSet<FIUProgramActivity> FIUProgramActivities { get; set; }

        public DbSet<FIUOtherActivity> FIUOtherActivities { get; set; }



        #endregion
        #region FTI
        public DbSet<FTIProgramDetails> FTIProgramDetails { get; set; }
        public DbSet<FTIParticipantDemographics> FTIParticipantDemographics { get; set; }
        public DbSet<FTIProgramContentAndResources> FTIProgramContentAndResources { get; set; }
        public DbSet<FTIResourcePerson> FTIResourcePersons { get; set; }
        public DbSet<FTITopicsCoveredInClass> FTITopicsCoveredInClass { get; set; }
        public DbSet<FTITeachingAidsDeveloped> FTITeachingAidsDeveloped { get; set; }
        public DbSet<FTIAdvisoryServices> FTIAdvisoryServices { get; set; }
        public DbSet<FTIReport> FTIReports { get; set; }
        public DbSet<FTIRecommendation> FTIRecommendations { get; set; }

        #endregion
        #region IBT&VA
       
        

        public DbSet<IbtvaProgramDetails> IbtvaProgramDetails { get; set; }
        public DbSet<IbtvaParticipantDemographics> IbtvaParticipantDemographics { get; set; }
        public DbSet<IbtvaProgramContentAndResources> IbtvaProgramContentAndResources { get; set; }
        public DbSet<IbtvaResourcePerson> IbtvaResourcePersons { get; set; }
        public DbSet<IbtvaTopicsCoveredInClass> IbtvaTopicsCoveredInClass { get; set; }
        public DbSet<IbtvaTeachingAidsDeveloped> IbtvaTeachingAidsDeveloped { get; set; }
        public DbSet<IbtvaAdvisoryServices> IbtvaAdvisoryServices { get; set; }
        public DbSet<IbtvaReport> IbtvaReports { get; set; }
        public DbSet<IbtvaRecommendation> IbtvaRecommendations { get; set; }
        #endregion
        #region ATIC
        public DbSet<AticProgramDetails> AticProgramDetails { get; set; }
        public DbSet<AticParticipantDemographics> AticParticipantDemographics { get; set; }
        public DbSet<AticProgramContentAndResources> AticProgramContentAndResources { get; set; }
        public DbSet<AticResourcePerson> AticResourcePersons { get; set; }
        public DbSet<AticTopicsCoveredInClass> AticTopicsCoveredInClass { get; set; }
        public DbSet<AticTeachingAidsDeveloped> AticTeachingAidsDeveloped { get; set; }
        public DbSet<AticAdvisoryServices> AticAdvisoryServices { get; set; }
        public DbSet<AticReport> AticReports { get; set; }
        public DbSet<AticRecommendation> AticRecommendations { get; set; }
        #endregion
        #region DEU
        public DbSet<DeuCourse> DeuCourses { get; set; }
        public DbSet<DeuOtherActivity> DeuOtherActivities { get; set; }

        public DbSet<DeuProgramDetails> DeuProgramDetails { get; set; }
        public DbSet<DeuParticipantDemographics> DeuParticipantDemographics { get; set; }
        public DbSet<DeuProgramContentAndResources> DeuProgramContentAndResources { get; set; }
        public DbSet<DeuResourcePerson> DeuResourcePersons { get; set; }
        public DbSet<DeuTopicsCoveredInClass> DeuTopicsCoveredInClass { get; set; }
        public DbSet<DeuTeachingAidsDeveloped> DeuTeachingAidsDeveloped { get; set; }
        public DbSet<DeuAdvisoryServices> DeuAdvisoryServices { get; set; }
        public DbSet<DeuReport> DeuReports { get; set; }
        public DbSet<DeuRecommendation> DeuRecommendations { get; set; }

        #endregion
        #region ASM
        public DbSet<ASMVisitorDetails> ASMVisitorDetails { get; set; }

        #endregion
        #region NAEP
        public DbSet<NaepProgramDetails> NaepProgramDetails { get; set; }
        public DbSet<NaepParticipantDemographics> NaepParticipantDemographics { get; set; }
        public DbSet<NaepProgramContentAndResources> NaepProgramContentAndResources { get; set; }
        public DbSet<NaepResourcePerson> NaepResourcePersons { get; set; }
        public DbSet<NaepTopicsCoveredInClass> NaepTopicsCoveredInClass { get; set; }
        public DbSet<NaepTeachingAidsDeveloped> NaepTeachingAidsDeveloped { get; set; }
        public DbSet<NaepAdvisoryServices> NaepAdvisoryServices { get; set; }
        public DbSet<NaepReport> NaepReports { get; set; }
        public DbSet<NaepRecommendation> NaepRecommendations { get; set; }
        #endregion
        #region EEU
        public DbSet<EeuOFT> EeuOFTs { get; set; }
        public DbSet<EeuFLD> EeuFLDs { get; set; }
        public DbSet<EeuTrainingProgramme> EeuTrainingProgrammes { get; set; }
        public DbSet<EeuOtherActivity> EeuOtherActivities { get; set; }

        public DbSet<EeuProgramDetails> EeuProgramDetails { get; set; }
        public DbSet<EeuParticipantDemographics> EeuParticipantDemographics { get; set; }
        public DbSet<EeuProgramContentAndResources> EeuProgramContentAndResources { get; set; }
        public DbSet<EeuResourcePerson> EeuResourcePersons { get; set; }
        public DbSet<EeuTopicsCoveredInClass> EeuTopicsCoveredInClass { get; set; }
        public DbSet<EeuTeachingAidsDeveloped> EeuTeachingAidsDeveloped { get; set; }
        public DbSet<EeuAdvisoryServices> EeuAdvisoryServices { get; set; }
        public DbSet<EeuReport> EeuReports { get; set; }
        public DbSet<EeuRecommendation> EeuRecommendations { get; set; }
        #endregion
        #region  KVK 
        public DbSet<KvkProgramDetails> KvkProgramDetails { get; set; }
        public DbSet<KvkParticipantDemographics> KvkParticipantDemographics { get; set; }
        public DbSet<KvkProgramContentAndResources> KvkProgramContentAndResources { get; set; }
        public DbSet<KvkResourcePerson> KvkResourcePersons { get; set; }
        public DbSet<KvkTopicsCoveredInClass> KvkTopicsCoveredInClass { get; set; }
        public DbSet<KvkTeachingAidsDeveloped> KvkTeachingAidsDeveloped { get; set; }
        public DbSet<KvkAdvisoryServices> KvkAdvisoryServices { get; set; }

        public DbSet<KvkResult> KvkResults { get; set; }

        public DbSet<KvkFldResult> KvkFLDResults { get; set; }
        public DbSet<KvkOftResult> KvkOFTResults { get; set; }
        public DbSet<KvkReport> KvkReports { get; set; }
        public DbSet<KvkRecommendation> KvkRecommendations { get; set; }

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

        public DbSet<ConsultancyServicesCategory> ConsultancyServicesCategories { get; set; }

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

        #region ConsultingAndSocialMediaService           

        public DbSet<ConsultingAndSocialMediaService> TableConsultingAndSocialMediaServices { get; set; }
        public DbSet<TableModeAndOutreach>? TableModeAndOutreaches { get; set; }

        #endregion

        #region tblService
        public DbSet<TblService> Services { get; set; }
        public DbSet<TableHostel> TableHostels { get; set; }
        public DbSet<RevolvingFundStatus> RevolvingFundStatuses { get; set; }
        public DbSet<VisitorDetail> VisitorDetails { get; set; }

        #endregion

        #region OtherActivity
        public DbSet<TableOtherActivity> OtherActivities { get; set; }
        #endregion

        #endregion

        



        // Get current user ID directly from HTTP context to avoid circular dependency
        //private int? GetCurrentUserId()
        //{
        //    var httpContext = _httpContextAccessor?.HttpContext;
        //    if (httpContext?.User?.Identity?.IsAuthenticated != true)               
        //    return null;

        //    var userIdClaim = httpContext.User.FindFirst("UserId")?.Value
        //                   ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    return int.TryParse(userIdClaim, out var userId) ? userId : null;
        //}

        //Override SaveChanges to automatically handle audit fields
        //public override int SaveChanges()
        //{
        //    UpdateAuditFields();
        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    UpdateAuditFields();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void UpdateAuditFields()
        //{
        //    var currentUserId = GetCurrentUserId();
        //    var now = DateTimeOffset.UtcNow;
        //    foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedAt = now;
        //                entry.Entity.CreatedById = currentUserId;
        //                break;

        //            case EntityState.Modified:
        //                entry.Entity.UpdatedAt = now;
        //                entry.Entity.UpdatedById = currentUserId;

        //                //Prevent modification of CreatedAt and createdById
        //                entry.Property(x => x.CreatedAt).IsModified = false;
        //                entry.Property(x => x.CreatedById).IsModified = false;

        //                //mark updatedAt and UpdateId as modified
        //                entry.Property(x => x.UpdatedAt).IsModified = true;
        //                entry.Property(x => x.UpdatedById).IsModified = true;
        //                break;
        //        }
        //    }
        //    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedAt = now;
        //                break;

        //            case EntityState.Modified:
        //                entry.Entity.UpdatedAt = now;
        //                //prevent Modification of CreatedAt

        //                entry.Property(x => x.CreatedAt).IsModified = false;
        //                entry.Property(x => x.UpdatedAt).IsModified = true;
        //                break;
        //        }
        //    }
        //}




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1️⃣ ProgramDetails ↔ Result (1:1)
            modelBuilder.Entity<KvkProgramDetails>()
                .HasOne(p => p.Results)
                .WithOne(r => r.ProgramDetails)
                .HasForeignKey<KvkResult>(r => r.KvkProgramDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2️⃣ ProgramDetails ↔ Report (1:1)
            modelBuilder.Entity<KvkProgramDetails>()
                .HasOne(p => p.Reports)
                .WithOne(r => r.ProgramDetails)
                .HasForeignKey<KvkReport>(r => r.KvkProgramDetailsId)
                .OnDelete(DeleteBehavior.Cascade);


            // 3️⃣ Result ↔ FLD Results (1:N)
            modelBuilder.Entity<KvkResult>()
                .HasMany(r => r.FldResults)
                .WithOne(f => f.KvkResult) // <-- FIX: Use navigation property to KvkResult, not ProgramDetails
                .HasForeignKey(f => f.KvkResultId) // <-- FIX: Use foreign key to KvkResult
                .OnDelete(DeleteBehavior.Cascade);

            // 4️⃣ Result ↔ OFT Results (1:N)
            modelBuilder.Entity<KvkResult>()
                .HasMany(r => r.OftResults)
                .WithOne(o => o.KvkResult) // <-- FIX: Use navigation property to KvkResult, not ProgramDetails
                .HasForeignKey(o => o.KvkResultId) // <-- FIX: Use foreign key to KvkResult
                .OnDelete(DeleteBehavior.Cascade);



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
