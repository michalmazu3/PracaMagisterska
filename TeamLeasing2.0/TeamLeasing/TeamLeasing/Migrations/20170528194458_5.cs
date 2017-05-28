using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLeasing.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegotiationId",
                table: "Offers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Negotiation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalInformation = table.Column<string>(nullable: true),
                    EmploymentType = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negotiation", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Negotiation_NegotiationId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Negotiation");

            migrationBuilder.DropIndex(
                name: "IX_Offers_NegotiationId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "NegotiationId",
                table: "Offers");
        }
    }
}
