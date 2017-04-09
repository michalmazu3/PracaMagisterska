using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUser_Technologies_TechnologyId",
                table: "DeveloperUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_DeveloperUser_DeveloperUserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Developers_DeveloperId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser");

            migrationBuilder.RenameTable(
                name: "DeveloperUser",
                newName: "DeveloperUsers");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "AspNetUsers",
                newName: "DeveloperUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeveloperId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeveloperUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperUser_TechnologyId",
                table: "DeveloperUsers",
                newName: "IX_DeveloperUsers_TechnologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeveloperUsers",
                table: "DeveloperUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_DeveloperUsers_DeveloperUserId",
                table: "Offers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_Technologies_TechnologyId",
                table: "DeveloperUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_DeveloperUsers_DeveloperUserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeveloperUsers_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeveloperUsers",
                table: "DeveloperUsers");

            migrationBuilder.RenameTable(
                name: "DeveloperUsers",
                newName: "DeveloperUser");

            migrationBuilder.RenameColumn(
                name: "DeveloperUserId",
                table: "AspNetUsers",
                newName: "DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeveloperUserId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperUsers_TechnologyId",
                table: "DeveloperUser",
                newName: "IX_DeveloperUser_TechnologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUser_Technologies_TechnologyId",
                table: "DeveloperUser",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_DeveloperUser_DeveloperUserId",
                table: "Offers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Developers_DeveloperId",
                table: "AspNetUsers",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
