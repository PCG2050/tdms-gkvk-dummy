using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplacePasswordResetTokenWithOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiresAt",
                table: "Users",
                newName: "PasswordResetOTPLastAttempt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Users",
                newName: "PasswordResetOTP");

            migrationBuilder.AddColumn<string>(
                name: "LastUsedPasswordResetOTP",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PasswordResetOTPAttempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PasswordResetOTPExpiresAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUsedPasswordResetOTP",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetOTPAttempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetOTPExpiresAt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordResetOTPLastAttempt",
                table: "Users",
                newName: "PasswordResetTokenExpiresAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetOTP",
                table: "Users",
                newName: "PasswordResetToken");
        }
    }
}
