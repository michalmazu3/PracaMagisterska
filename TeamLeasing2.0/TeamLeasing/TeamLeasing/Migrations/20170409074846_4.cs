using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendingDate",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "TechnologyId",
                table: "DeveloperUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers");

            migrationBuilder.DropColumn(
                name: "SendingDate",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "TechnologyId",
                table: "DeveloperUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
