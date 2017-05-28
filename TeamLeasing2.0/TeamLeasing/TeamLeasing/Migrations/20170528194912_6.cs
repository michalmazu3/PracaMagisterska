using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Negotiation_NegotiationId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_NegotiationId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "NegotiationId",
                table: "Offers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "NegotiationId",
                table: "Offers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_NegotiationId",
                table: "Offers",
                column: "NegotiationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Negotiation_NegotiationId",
                table: "Offers",
                column: "NegotiationId",
                principalTable: "Negotiation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
