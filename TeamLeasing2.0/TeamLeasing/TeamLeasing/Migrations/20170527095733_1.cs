using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLeasing.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Offers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DeveloperUserId",
                table: "Offers",
                column: "DeveloperUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_DeveloperUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Offers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                columns: new[] { "DeveloperUserId", "EmployeeUserId" });
        }
    }
}
