using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StartEndDateNDAttachementsForEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StuTrainingProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StuSponseredTrainingProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StuOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "IbtvaProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "IbtavOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "FtiTrainingPrograms",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "FtiOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "FiuProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EeuTrainingProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EeuOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EeuOFTs",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EeuFLDs",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DeuOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DeuCourses",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DaesiProgrammes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DaesiOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AticSales",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AticOtherActivities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AticAdvisoryServices",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AsmVisits",
                newName: "StartDate");

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "StuTrainingProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "StuTrainingProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "StuSponseredTrainingProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "StuSponseredTrainingProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "StuOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "StuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "IbtvaProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "IbtvaProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "IbtavOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "IbtavOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "FtiTrainingPrograms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FtiTrainingPrograms",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "FtiOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FtiOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "FiuProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FiuProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "EeuTrainingProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "EeuTrainingProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "EeuOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "EeuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "EeuOFTs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "EeuOFTs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "EeuFLDs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "EeuFLDs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "DeuOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "DeuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "DeuCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "DeuCourses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "DaesiProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "DaesiProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "DaesiOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "DaesiOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "AticSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "AticSales",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "AticOtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "AticOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "AticAdvisoryServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "AticAdvisoryServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "AsmVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "AsmVisits",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "StuOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "StuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "IbtvaProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "IbtvaProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "IbtavOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "IbtavOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "AsmVisits");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AsmVisits");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "StuTrainingProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "StuSponseredTrainingProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "StuOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "IbtvaProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "IbtavOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "FtiTrainingPrograms",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "FtiOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "FiuProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EeuTrainingProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EeuOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EeuOFTs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EeuFLDs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DeuOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DeuCourses",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DaesiProgrammes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DaesiOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AticSales",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AticOtherActivities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AticAdvisoryServices",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AsmVisits",
                newName: "Date");
        }
    }
}
