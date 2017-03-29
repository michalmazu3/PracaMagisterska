using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class IsFinishedUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsFinishedUniversity",
                table: "Developers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinishedUniversity",
                table: "Developers");
        }
    }
}
