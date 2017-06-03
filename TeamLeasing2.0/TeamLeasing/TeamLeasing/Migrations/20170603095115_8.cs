using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLeasing.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferStatus",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "StatusForDeveloper",
                table: "Negotiation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusForEmployee",
                table: "Negotiation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusForDeveloper",
                table: "Negotiation");

            migrationBuilder.DropColumn(
                name: "StatusForEmployee",
                table: "Negotiation");

            migrationBuilder.AddColumn<string>(
                name: "OfferStatus",
                table: "Offers",
                nullable: true);
        }
    }
}
