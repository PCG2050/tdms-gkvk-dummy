
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Interface;
using Infrastructure.Services;
using Application.Interface.Repository;
using Infrastructure.Repository;
using WebApi.Services;
using System.Text;
using Application.Interface.Repository.DataTables;
using Infrastructure.Repository.DataTables;
using Application.Interface.Services.DataTables;
using Infrastructure.Services.DataTables;
using Application.Interface.Services.Common;

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

            builder.Services.AddHttpContextAccessor();//For IHttpContextAccessor in infracture
            builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenService, AuthTokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}