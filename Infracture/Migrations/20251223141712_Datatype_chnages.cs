using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Datatype_chnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "KvkResourcePersons");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "KvkTopicsCoveredInClass",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "KvkResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "ExtensionLiteratures",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfCopies",
                table: "ExtensionLiteratures",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPerCopy",
                table: "ExtensionLiteratures",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KvkResourcePersons_ResourceTypeId",
                table: "KvkResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_ResourceTypes_ResourceTypeId",
                table: "KvkResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_ResourceTypes_ResourceTypeId",
                table: "KvkResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_KvkResourcePersons_ResourceTypeId",
                table: "KvkResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "KvkResourcePersons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "KvkTopicsCoveredInClass",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "KvkResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TotalAmount",
                table: "ExtensionLiteratures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumberOfCopies",
                table: "ExtensionLiteratures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AmountPerCopy",
                table: "ExtensionLiteratures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
