using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "StatusForDeveloper",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusForEmployee",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusForDeveloper",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "StatusForEmployee",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
