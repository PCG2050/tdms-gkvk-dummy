using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Services.DataTables.FTI;
using Application.Mapper.DataTable.FTI;
using Infrastructure.Repository.DataTables.FTI;
using Infrastructure.Services.DataTables.FTI;
using Infrastructure.Services.DataTables.STU;

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
                     options.JsonSerializerOptions.Converters.Add(new WebApi.JsonConverters.NullableIntConverter());
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
            IServiceCollection serviceCollection = builder.Services.AddSingleton<NominationRewardMapper>();
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
       
                        
            //builder.Services.AddScoped<IAticSalesRepository, AticSalesRepository>();         
            //builder.Services.AddScoped<IAsmVisitRepository, AsmVisitRepository>();
                    

            // Mappers
            builder.Services.AddSingleton<IbtvaProgramMapper>();
            builder.Services.AddSingleton<AticProgramMapper>();
            builder.Services.AddSingleton<DeuProgramMapper>();
            builder.Services.AddSingleton<NaepProgramMapper>();
            builder.Services.AddSingleton<EeuProgramMapper>();
            builder.Services.AddSingleton<StuProgramMapper>();
            builder.Services.AddSingleton<KvkProgramMapper>();
            builder.Services.AddSingleton<FIUProgramActivityMapper>();
            builder.Services.AddSingleton<ASMVisitorDetailsMapper>();
            builder.Services.AddSingleton<FtiProgramMapper>();

            builder.Services.AddSingleton<ConsultingServiceMapper>();
            builder.Services.AddSingleton<PublicationMapper>();
            builder.Services.AddSingleton<TblServiceMapper>();


            #region FIU
           
            builder.Services.AddScoped<IFIUActivityRepository, FIUActivityRepository>();
            builder.Services.AddScoped<IFIUProgramActivityRepository, FIUProgramActivityRepository>();
            builder.Services.AddScoped<IFIUProgramActivityService, FIUProgramActivityService>();
            #endregion

            #region ASM
            builder.Services.AddScoped<IASMVisitorDetailsRepository, ASMVisitorDetailsRepository>();
            builder.Services.AddScoped<IASMVisitorDetailsService, ASMVisitorDetailsService>();
            #endregion

            #region FTI
            builder.Services.AddScoped<IFtiProgramDetailsRepository, FtiProgramDetailsRepository>();
            builder.Services.AddScoped<IFtiParticipantDemographicsRepository, FtiParticipantDemographicsRepository>();
            builder.Services.AddScoped<IFtiProgramContentRepository, FtiProgramContentRepository>();
            builder.Services.AddScoped<IFtiResourcePersonRepository, FtiResourcePersonRepository>();
            builder.Services.AddScoped<IFtiTopicsCoveredRepository, FtiTopicsCoveredRepository>();
            builder.Services.AddScoped<IFtiTeachingAidsRepository, FtiTeachingAidsRepository>();
            builder.Services.AddScoped<IFtiAdvisoryServicesRepository, FtiAdvisoryServicesRepository>();
            builder.Services.AddScoped<IFtiReportRepository, FtiReportRepository>();
            builder.Services.AddScoped<IFtiRecommendationRepository, FtiRecommendationRepository>();

            builder.Services.AddScoped<IFtiProgramService, FtiProgramService>();
            #endregion

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
            builder.Services.AddScoped<INaepTopicsCoveredRepository,NaepTopicsCoveredRepository>();
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
            #region KVK
            // KVK Repositories
            builder.Services.AddScoped<IKvkProgramDetailsRepository, KvkProgramDetailsRepository>();
            builder.Services.AddScoped<IKvkParticipantDemographicsRepository, KvkParticipantDemographicsRepository>();
            builder.Services.AddScoped<IKvkProgramContentRepository, KvkProgramContentRepository>();
            builder.Services.AddScoped<IKvkResourcePersonRepository, KvkResourcePersonRepository>();
            builder.Services.AddScoped<IKvkTopicsCoveredRepository, KvkTopicsCoveredRepository>();
            builder.Services.AddScoped<IKvkTeachingAidsRepository, KvkTeachingAidsRepository>();
            builder.Services.AddScoped<IKvkAdvisoryServicesRepository, KvkAdvisoryServicesRepository>();
            builder.Services.AddScoped<IKvkResultRepository, KvkResultRepository>();
            builder.Services.AddScoped<IKvkFldResultRepository, KvkFldResultRepository>();
            builder.Services.AddScoped<IKvkOftResultRepository, KvkOftResultRepository>();
            builder.Services.AddScoped<IKvkReportRepository, KvkReportRepository>();
            builder.Services.AddScoped<IKvkRecommendationRepository, KvkRecommendationRepository>();

            // KVK Services
            builder.Services.AddScoped<IKvkProgramService, KvkProgramService>();         
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

            builder.Services.AddScoped<IAdminReportService, AdminReportService>();

            // Success Story Service
            builder.Services.AddScoped<ISuccessStoryService, SuccessStoryService>();




            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o=>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:TokenKey"]!)),

                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"

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