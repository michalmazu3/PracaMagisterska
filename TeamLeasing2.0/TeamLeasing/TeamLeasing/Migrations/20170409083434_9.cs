using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers");

            migrationBuilder.RenameColumn(
                name: "TechnologyId",
                table: "Technologies",
                newName: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Technologies",
                newName: "TechnologyId");

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
                principalColumn: "TechnologyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
