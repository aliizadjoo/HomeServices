using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class EditExpertHomeservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpertHomeServices",
                table: "ExpertHomeServices");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExpertHomeServices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExpertHomeServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ExpertHomeServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpertHomeServices",
                table: "ExpertHomeServices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertHomeServices_ExpertId",
                table: "ExpertHomeServices",
                column: "ExpertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpertHomeServices",
                table: "ExpertHomeServices");

            migrationBuilder.DropIndex(
                name: "IX_ExpertHomeServices_ExpertId",
                table: "ExpertHomeServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExpertHomeServices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExpertHomeServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ExpertHomeServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpertHomeServices",
                table: "ExpertHomeServices",
                columns: new[] { "ExpertId", "HomeServiceId" });
        }
    }
}
