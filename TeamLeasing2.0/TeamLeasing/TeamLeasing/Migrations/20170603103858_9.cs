using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLeasing.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Negotiation_Offers_OfferId",
                table: "Negotiation");

            migrationBuilder.DropIndex(
                name: "IX_Negotiation_OfferId",
                table: "Negotiation");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Negotiation");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Negotiation",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Negotiation_Offers_Id",
                table: "Negotiation",
                column: "Id",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Negotiation_Offers_Id",
                table: "Negotiation");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Negotiation",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Negotiation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Negotiation_OfferId",
                table: "Negotiation",
                column: "OfferId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Negotiation_Offers_OfferId",
                table: "Negotiation",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
