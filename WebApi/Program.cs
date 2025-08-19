
using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Infrastructure.DbContext;
using Infrastructure.Repository;
using Infrastructure.Repository.DataTables;
using Infrastructure.Services;
using Infrastructure.Services.DataTables;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;
using WebApi.Services;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
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
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridMail"));
            builder.Services.AddTransient<IEmailService, SendGridEmailService>();

            builder.Services.AddHttpContextAccessor();//For IHttpContextAccessor in infracture
            builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenService, AuthTokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();           
            builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();
            builder.Services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();
            builder.Services.AddScoped<ITrainerAssignmentRepository, TrainerAssignmentRepository>();
            builder.Services.AddScoped<ITrainerAssignmentService, TrainerAssignmentService>();
            builder.Services.AddScoped<IEntityPermissionService, EntityPermissionService>();
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
            builder.Services.AddScoped<IAsmVisitRepository, AsmVisitRepository>();
            builder.Services.AddScoped<IDeuCourseRepository, DeuCourseRepository>();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}