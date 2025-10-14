
using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Services;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Interface.Services.DataTables.IBTVA;
using Application.Mappers;
using Application.Mappers.IBTVA;
using Infrastructure.DbContext;
using Infrastructure.Repository;
using Infrastructure.Repository.DataTables;
using Infrastructure.Repository.DataTables.IBTVA;
using Infrastructure.Services;
using Infrastructure.Services.DataTables;
using Infrastructure.Services.DataTables.IBTVA;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Resend;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.File;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Services;
using Application.Mapper;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Infrastructure.Repository.DataTables.ConsultSocialMedia;
using Infrastructure.Repository.DataTables.Publication_Repo;
using Application.Interface.Services.DataTables.ConsultSocialMedia;
using Infrastructure.Services.DataTables.ConsultSocialMedia;
using Application.Interface.Repository.DataTables.TblService;

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

            // API Versioning Configuration
            //builder.Services.AddApiVersioning(options =>
            //{
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
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
            builder.Services.AddScoped<IAticAdvisoryServiceRepository, AticAdvisoryServiceRepository>();
            builder.Services.AddScoped<IAticSalesRepository, AticSalesRepository>();

         

            builder.Services.AddScoped<IDeuCourseRepository, DeuCourseRepository>();
            builder.Services.AddScoped<IAsmVisitRepository, AsmVisitRepository>();

            builder.Services.AddScoped<INaepDetailsRepository, NaepDetailsRepository>();

            builder.Services.AddScoped<IEeuOftRepository, EeuOftRepository>();
            builder.Services.AddScoped<IEeuFldRepository, EeuFldRepository>();
            builder.Services.AddScoped<IEeuTrainingProgrammeRepository, EeuTrainingProgrammeRepository>();

            builder.Services.AddScoped<IAticAdvisoryServiceService, AticAdvisoryServiceService>();
            builder.Services.AddScoped<IAticSalesService, AticSalesService>();
            builder.Services.AddScoped<IAsmVisitService, AsmVisitService>();
            builder.Services.AddScoped<IDeuCourseService, DeuCourseService>();
            builder.Services.AddScoped<INaepDetailsService, NaepDetailsService>();
            builder.Services.AddScoped<IEeuOftService, EeuOftService>();
            builder.Services.AddScoped<IEeuFldService, EeuFldService>();
            builder.Services.AddScoped<IEeuTrainingProgrammeService, EeuTrainingProgrammeService>();

            // Mappers
            builder.Services.AddSingleton<IbtvaProgramMapper>();
            builder.Services.AddSingleton<ConsultingServiceMapper>();
            builder.Services.AddSingleton<PublicationMapper>();
            builder.Services.AddSingleton<TblServiceMapper>();


            // Phase 1: Program
            builder.Services.AddScoped<IIbtvaProgramRepository, IbtvaProgramRepository>();
            builder.Services.AddScoped<IIbtvaProgramService, IbtvaProgramService>();

            // Phase 2: Demographics
            //builder.Services.AddScoped<IIbtvaDemographicsRepository, IbtvaDemographicsRepository>();
            //builder.Services.AddScoped<IIbtvaDemographicsService, IbtvaDemographicsService>();

            // Continue pattern for Phases 3-6...


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