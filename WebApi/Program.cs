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

            // Configure Serilog from appsettings.json
            // All enrichers (MachineName, ThreadId, EnvironmentName) are configured in appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Log application startup
            Log.Information("Starting TDMS GKVK API...");
            Log.Information("Environment: {Environment}", builder.Environment.EnvironmentName);

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

            // CORS Configuration - Secure with allowed origins from configuration
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:3000", "http://localhost:4200" };

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins(allowedOrigins)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            // Add Health Checks
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<TdmsDbContext>("database");

            // Add Response Compression
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            // Add Rate Limiting (ASP.NET Core 9.0 built-in)
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.User.Identity?.Name ?? context.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 100,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(1)
                        }));

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
                };
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
            builder.Services.AddSingleton<IIndianTimeService, IndianTimeService>(); // IST Time Service
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
            builder.Services.AddScoped<IFtiProgramDetailsRepository, FtiProgramDetailsRepositoryGeneric>();
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
            builder.Services.AddScoped<IFinancialBudgetRepository, FinancialBudgetRepository>();
            builder.Services.AddScoped<IFinancialBudgetService, FinancialBudgetService>();

            builder.Services.AddScoped<IAdminReportService, AdminReportService>();
            builder.Services.AddScoped<IDynamicReportService, DynamicReportService>();
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

            // ========================================
            // HTTP REQUEST PIPELINE CONFIGURATION
            // ========================================

            // Enable Response Compression
            app.UseResponseCompression();

            // Enable Rate Limiting
            app.UseRateLimiter();

            // Add Request Logging Middleware (BEFORE other middleware)
            app.UseMiddleware<WebApi.Middleware.RequestLoggingMiddleware>();

            // Add Global Exception Handling Middleware
            app.UseMiddleware<WebApi.Middleware.GlobalExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline - FIXED: Only enable in Development
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference((options) =>
                {
                    options
                    .WithTitle("GKVK Api")
                    .WithTheme(ScalarTheme.DeepSpace);
                });
            }
            else
            {
                // Production: Use exception handler
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            // CORS - Must be before authentication/authorization
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Health check endpoint
            app.MapHealthChecks("/health");

            Log.Information("TDMS GKVK API started successfully");

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}