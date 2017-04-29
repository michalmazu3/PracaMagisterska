using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Technology",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Technology",
                table: "Offers",
                newName: "OfferStatus");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Offers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TechnologyId",
                table: "Offers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Jobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TechnologyId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Developers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TechnologyId",
                table: "Offers",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TechnologyId",
                table: "Jobs",
                column: "TechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Technologies_TechnologyId",
                table: "Jobs",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Technologies_TechnologyId",
                table: "Offers",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Technologies_TechnologyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Technologies_TechnologyId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_TechnologyId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_TechnologyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TechnologyId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TechnologyId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "OfferStatus",
                table: "Offers",
                newName: "Technology");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Offers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Technology",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Developers",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
