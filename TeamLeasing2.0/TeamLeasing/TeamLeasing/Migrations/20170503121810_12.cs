using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusForDeveloper",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "StatusForDeveloper",
                table: "DeveloperUserJob",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusForDeveloper",
                table: "DeveloperUserJob");

            migrationBuilder.AddColumn<int>(
                name: "StatusForDeveloper",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
