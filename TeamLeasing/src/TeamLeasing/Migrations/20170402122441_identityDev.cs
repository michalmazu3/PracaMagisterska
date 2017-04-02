using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class identityDev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeveloperId",
                table: "AspNetUsers",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Developers_DeveloperId",
                table: "AspNetUsers",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Developers_DeveloperId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeveloperId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "AspNetUsers");
        }
    }
}
