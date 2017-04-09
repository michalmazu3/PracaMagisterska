using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperUserId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperUserId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
