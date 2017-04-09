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
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DeveloperUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperUsers_UserId",
                table: "DeveloperUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_AspNetUsers_UserId",
                table: "DeveloperUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_AspNetUsers_UserId",
                table: "DeveloperUsers");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperUsers_UserId",
                table: "DeveloperUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeveloperUsers");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperUserId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
