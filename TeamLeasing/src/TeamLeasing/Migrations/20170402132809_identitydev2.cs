using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLeasing.Migrations
{
    public partial class identitydev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Developers_DeveloperId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "AspNetUsers",
                newName: "DeveloperUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeveloperId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeveloperUserId");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperUserId",
                table: "Offers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeveloperUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Cv = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: false),
                    IsFinishedUniversity = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    TechnologyId = table.Column<int>(nullable: true),
                    University = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeveloperUser_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DeveloperUserId",
                table: "Offers",
                column: "DeveloperUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperUser_TechnologyId",
                table: "DeveloperUser",
                column: "TechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_DeveloperUser_DeveloperUserId",
                table: "Offers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeveloperUser_DeveloperUserId",
                table: "AspNetUsers",
                column: "DeveloperUserId",
                principalTable: "DeveloperUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_DeveloperUser_DeveloperUserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeveloperUser_DeveloperUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeveloperUser");

            migrationBuilder.DropIndex(
                name: "IX_Offers_DeveloperUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DeveloperUserId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "DeveloperUserId",
                table: "AspNetUsers",
                newName: "DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeveloperUserId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeveloperId");

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
