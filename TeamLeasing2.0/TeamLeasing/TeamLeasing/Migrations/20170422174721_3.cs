using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeUserId",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs",
                column: "EmployeeUserId",
                principalTable: "EmployeeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeUserId",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs",
                column: "EmployeeUserId",
                principalTable: "EmployeeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
