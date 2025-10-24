



using Application.Interface.Repository.DataTables.ATIC;
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Repository.DataTables.NAEP;
using Application.Interface.Repository.DataTables.STU;
using Application.Interface.Services.DataTables.ATIC;
using Application.Interface.Services.DataTables.DEU;
using Application.Interface.Services.DataTables.EEU;
using Application.Interface.Services.DataTables.NAEP;
using Application.Interface.Services.DataTables.STU;
using Application.Mapper.DataTable.ATIC;
using Application.Mapper.DataTable.DEU;
using Application.Mapper.DataTable.EEU;
using Application.Mapper.DataTable.NAEP;
using Application.Mapper.DataTable.STU;
using Infrastructure.Repository.DataTables.ATIC;
using Infrastructure.Repository.DataTables.DEU;
using Infrastructure.Repository.DataTables.EEU;
using Infrastructure.Repository.DataTables.NAEP;
using Infrastructure.Repository.DataTables.STU;
using Infrastructure.Services.DataTables.ATIC;
using Infrastructure.Services.DataTables.DEU;
using Infrastructure.Services.DataTables.EEU;
using Infrastructure.Services.DataTables.NAEP;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            builder.Host.UseSerilog();

            //Configure JSON options for better data handlingServer
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                     //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 });

            //API Versioning Configuration
            //builder.Services.AddApiVersioning(options =>
            //{                
            //    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.ReportApiVersions = true;
            //    options.ApiVersionReader = new UrlSegmentApiVersionReader();
            //});

            //builder.Services.AddVersionedApiExplorer(options =>
            //{
            //    options.GroupNameFormat = "'v'VVV";
            //    options.SubstituteApiVersionInUrl = true;
            //});

            // Mapperly Mapper - Singleton (stateless)
            builder.Services.AddSingleton<PublicationMapper>();
            builder.Services.AddSingleton<ConsultingServiceMapper>();
            builder.Services.AddSingleton<NominationRewardMapper>();
            builder.Services.AddSingleton<TableOtherActivityMapper>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openappi
            builder.Services.AddOpenApi();

            builder.Services.AddHttpContextAccessor();//For IHttpContextAccessor in infrastructure

            builder.Services.AddDbContext<TdmsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));
            builder.Services.AddScoped<IRouteService, RouteService>();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });
            //Email Configuration
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<ResendSettings>(builder.Configuration.GetSection("ResendMail"));
            // Configure Resend client - Updated to match documentation
            builder.Services.AddSingleton<IResend>(provider =>
            {
                var apiKey = builder.Configuration["ResendMail:ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                    throw new InvalidOperationException("ResendMail:ApiKey is not configured");

                return ResendClient.Create(apiKey);
            });
            builder.Services.AddTransient<IEmailService, ResendEmailService>();
          
                              
           
            builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();
            builder.Services.AddScoped<IOTPService, OTPService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenService, AuthTokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();           
            builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

            //Repository Layer
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>(); 
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();           
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();           
            builder.Services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();
            builder.Services.AddScoped<IUnitHeadAssignmentRepository, UnitHeadAssignmentRepository>();
            builder.Services.AddScoped<ITrainerAssignmentRepository, TrainerAssignmentRepository>();

            //Service layer
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();
            builder.Services.AddScoped<IUnitHeadAssignmentService, UnitHeadAssignmentService>();
            builder.Services.AddScoped<ITrainerAssignmentService, TrainerAssignmentService>();
            builder.Services.AddScoped<IEntityPermissionService, EntityPermissionService>();

            //Tables
            builder.Services.AddScoped<IFtiTrainingProgrammeRepository, FtiTrainingProgrammeRepository>();
            builder.Services.AddScoped<IFtiTrainingProgrammeService, FtiTrainingProgrammeService>();
            builder.Services.AddScoped<IFtiOtherActivitiesRepository, FtiOtherActivitiesRepository>();
            builder.Services.AddScoped<IFtiOtherActivityService, FtiOtherActivityService>();

            builder.Services.AddScoped<IStuTrainingProgrammeRepository, StuTrainingProgrammeRepository>();
            builder.Services.AddScoped<IStuTrainingProgrammeService, StuTrainingProgrammeService>();
            builder.Services.AddScoped<IStuSposoredTrainingProgrammeRepository, StuSponsoredTrainingProgrammeRepository>();
            builder.Services.AddScoped<IStuSponsoredTrainingProgrammeService, StuSponsoredTrainingProgrammeService>();

            builder.Services.AddScoped<IDaesiProgrammeRepository, DaesiProgramRepository>();
            builder.Services.AddScoped<IDaesiProgrammeService, DaesiProgrammeService>();

            builder.Services.AddScoped<IIbtvaProgrammeRepository, IbtvaProgrammeRepository>();
            builder.Services.AddScoped<IIbtvaProgrammeService, IbtvaProgrammeService>();         
            builder.Services.AddScoped<IAticSalesRepository, AticSalesRepository>();

         

            builder.Services.AddScoped<IDeuCourseRepository, DeuCourseRepository>();
            builder.Services.AddScoped<IAsmVisitRepository, AsmVisitRepository>();

            builder.Services.AddScoped<INaepDetailsRepository, NaepDetailsRepository>();

            builder.Services.AddScoped<IEeuOftRepository, EeuOftRepository>();
            builder.Services.AddScoped<IEeuFldRepository, EeuFldRepository>();
            builder.Services.AddScoped<IEeuTrainingProgrammeRepository, EeuTrainingProgrammeRepository>();

           
            builder.Services.AddScoped<IAticSalesService, AticSalesService>();
            builder.Services.AddScoped<IAsmVisitService, AsmVisitService>();
            builder.Services.AddScoped<IDeuCourseService, DeuCourseService>();
            builder.Services.AddScoped<INaepDetailsService, NaepDetailsService>();
            builder.Services.AddScoped<IEeuOftService, EeuOftService>();
            builder.Services.AddScoped<IEeuFldService, EeuFldService>();
            builder.Services.AddScoped<IEeuTrainingProgrammeService, EeuTrainingProgrammeService>();

            // Mappers
            builder.Services.AddSingleton<IbtvaProgramMapper>();
            builder.Services.AddSingleton<AticProgramMapper>();
            builder.Services.AddSingleton<DeuProgramMapper>();
            builder.Services.AddSingleton<NaepProgramMapper>();
            builder.Services.AddSingleton<EeuProgramMapper>();
            builder.Services.AddSingleton<StuProgramMapper>();
        
            builder.Services.AddSingleton<ConsultingServiceMapper>();
            builder.Services.AddSingleton<PublicationMapper>();
            builder.Services.AddSingleton<TblServiceMapper>();

            #region STU
            builder.Services.AddScoped<IStuProgramDetailsRepository, StuProgramDetailsRepository>();
            builder.Services.AddScoped<IStuParticipantDemographicsRepository, StuParticipantDemographicsRepository>();
            builder.Services.AddScoped<IStuProgramContentRepository, StuProgramContentRepository>();
            builder.Services.AddScoped<IStuResourcePersonRepository, StuResourcePersonRepository>();
            builder.Services.AddScoped<IStuTopicsCoveredRepository, StuTopicsCoveredRepository>();
            builder.Services.AddScoped<IStuTeachingAidsRepository, StuTeachingAidsRepository>();
            builder.Services.AddScoped<IStuAdvisoryServicesRepository, StuAdvisoryServicesRepository>();
            builder.Services.AddScoped<IStuReportRepository, StuReportRepository>();
            builder.Services.AddScoped<IStuRecommendationRepository, StuRecommendationRepository>();

            builder.Services.AddScoped<IStuProgramService, StuProgramService>();
            #endregion
            #region IBTVA
            // IBTVA Repositories
            builder.Services.AddScoped<IIbtvaProgramDetailsRepository, IbtvaProgramDetailsRepository>();
            builder.Services.AddScoped<IIbtvaParticipantDemographicsRepository, IbtvaParticipantDemographicsRepository>();
            builder.Services.AddScoped<IIbtvaProgramContentRepository, IbtvaProgramContentRepository>();
            builder.Services.AddScoped<IIbtvaResourcePersonRepository, IbtvaResourcePersonRepository>();
            builder.Services.AddScoped<IIbtvaTopicsCoveredRepository, IbtvaTopicsCoveredRepository>();
            builder.Services.AddScoped<IIbtvaTeachingAidsRepository, IbtvaTeachingAidsRepository>();
            builder.Services.AddScoped<IIbtvaAdvisoryServicesRepository, IbtvaAdvisoryServicesRepository>();
            builder.Services.AddScoped<IIbtvaReportRepository, IbtvaReportRepository>();
            builder.Services.AddScoped<IIbtvaRecommendationRepository, IbtvaRecommendationRepository>();

            // IBTVA Service
            builder.Services.AddScoped<IIbtvaProgramService, IbtvaProgramService>();
            #endregion
            #region ATIC
            builder.Services.AddScoped<IAticProgramDetailsRepository, AticProgramDetailsRepository>();
            builder.Services.AddScoped<IAticParticipantDemographicsRepository, AticParticipantDemographicsRepository>();
            builder.Services.AddScoped<IAticProgramContentRepository, AticProgramContentRepository>();
            builder.Services.AddScoped<IAticResourcePersonRepository, AticResourcePersonRepository>();
            builder.Services.AddScoped<IAticTopicsCoveredRepository, AticTopicsCoveredRepository>();
            builder.Services.AddScoped<IAticTeachingAidsRepository, AticTeachingAidsRepository>();
            builder.Services.AddScoped<IAticAdvisoryServicesRepository, AticAdvisoryServicesRepository>();
            builder.Services.AddScoped<IAticReportRepository, AticReportRepository>();
            builder.Services.AddScoped<IAticRecommendationRepository, AticRecommendationRepository>();

            builder.Services.AddScoped<IAticProgramService, AticProgramService>();
            #endregion
            #region DEU
            builder.Services.AddScoped<IDeuProgramDetailsRepository, DeuProgramDetailsRepository>();
            builder.Services.AddScoped<IDeuParticipantDemographicsRepository, DeuParticipantDemographicsRepository>();
            builder.Services.AddScoped<IDeuProgramContentRepository, DeuProgramContentRepository>();
            builder.Services.AddScoped<IDeuResourcePersonRepository, DeuResourcePersonRepository>();
            builder.Services.AddScoped<IDeuTopicsCoveredRepository, DeuTopicsCoveredRepository>();
            builder.Services.AddScoped<IDeuTeachingAidsRepository, DeuTeachingAidsRepository>();
            builder.Services.AddScoped<IDeuAdvisoryServicesRepository, DeuAdvisoryServicesRepository>();
            builder.Services.AddScoped<IDeuReportRepository, DeuReportRepository>();
            builder.Services.AddScoped<IDeuRecommendationRepository, DeuRecommendationRepository>();

            builder.Services.AddScoped<IDeuProgramService, DeuProgramService>();
            #endregion
            #region NAEP
            builder.Services.AddScoped<INaepProgramDetailsRepository, NaepProgramDetailsRepository>();
            builder.Services.AddScoped<INaepParticipantDemographicsRepository, NaepParticipantDemographicsRepository>();
            builder.Services.AddScoped<INaepProgramContentRepository, NaepProgramContentRepository>();
            builder.Services.AddScoped<INaepResourcePersonRepository, NaepResourcePersonRepository>();
            builder.Services.AddScoped<INaepTopicsCoveredRepository, NaepTopicsCoveredRepository>();
            builder.Services.AddScoped<INaepTeachingAidsRepository, NaepTeachingAidsRepository>();
            builder.Services.AddScoped<INaepAdvisoryServicesRepository, NaepAdvisoryServicesRepository>();
            builder.Services.AddScoped<INaepReportRepository, NaepReportRepository>();
            builder.Services.AddScoped<INaepRecommendationRepository, NaepRecommendationRepository>();

            builder.Services.AddScoped<INaepProgramService, NaepProgramService>();
            #endregion
            #region EEU
            builder.Services.AddScoped<IEeuProgramDetailsRepository, EeuProgramDetailsRepository>();
            builder.Services.AddScoped<IEeuParticipantDemographicsRepository, EeuParticipantDemographicsRepository>();
            builder.Services.AddScoped<IEeuProgramContentRepository, EeuProgramContentRepository>();
            builder.Services.AddScoped<IEeuResourcePersonRepository, EeuResourcePersonRepository>();
            builder.Services.AddScoped<IEeuTopicsCoveredRepository, EeuTopicsCoveredRepository>();
            builder.Services.AddScoped<IEeuTeachingAidsRepository, EeuTeachingAidsRepository>();
            builder.Services.AddScoped<IEeuAdvisoryServicesRepository, EeuAdvisoryServicesRepository>();
            builder.Services.AddScoped<IEeuReportRepository, EeuReportRepository>();
            builder.Services.AddScoped<IEeuRecommendationRepository, EeuRecommendationRepository>();

            builder.Services.AddScoped<IEeuProgramService, EeuProgramService>();
            #endregion



            //Generic Tables
            builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
            builder.Services.AddScoped<IPublisherDetailsRepository, PublisherDetailsRepository>();
            builder.Services.AddScoped<IExtensionLiteratureRepository, ExtensionLiteratureRepository>();
            builder.Services.AddScoped<IPublicationService, PublicationService>();

            builder.Services.AddScoped<IConsultingServiceRepository, ConsultingServiceRepository>();
            builder.Services.AddScoped<IModeAndOutreachRepository, ModeAndOutreachRepository>();
            builder.Services.AddScoped<IConsultingServiceService,ConsultingServiceService>();
            builder.Services.AddScoped<INominationRewardRepository,NominationRewardRepository>();
            builder.Services.AddScoped<INominationRewardService, NominationRewardService>();
            builder.Services.AddScoped<ITableOtherActivityRepository, TableOtherActivityRepository>();
            builder.Services.AddScoped<ITableOtherActivityService, TableOtherActivityService>();

            builder.Services.AddScoped<ITblServiceService, TblServiceService>();
            builder.Services.AddScoped<ITableServiceRepository, TableServiceRepository>();
            builder.Services.AddScoped<ITableHostelRepository, TableHostelRepository>();
            builder.Services.AddScoped<IRevolvingFundRepository, RevolvingFundRepository>();
            builder.Services.AddScoped<IVisitorDetailsRepository, VisitorDetailRepository>();





            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o=>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:TokenKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (true || app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference((options) =>
                {
                    options
                    .WithTitle("GKVK Api")
                    .WithTheme(ScalarTheme.DeepSpace);
                });
            }
        
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}