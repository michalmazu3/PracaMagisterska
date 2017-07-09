using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamLeasing.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Budget = table.Column<int>(nullable: false),
                    Descritpion = table.Column<string>(nullable: false),
                    EmployeeUserId = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    NumberOfDeveloperNeeded = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    VacanciesRemain = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_EmployeeUsers_EmployeeUserId",
                        column: x => x.EmployeeUserId,
                        principalTable: "EmployeeUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperInProject",
                columns: table => new
                {
                    DeveloperUserId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StatusForDeveloper = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperInProject", x => new { x.DeveloperUserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_DeveloperInProject_DeveloperUsers_DeveloperUserId",
                        column: x => x.DeveloperUserId,
                        principalTable: "DeveloperUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperInProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperInProject_ProjectId",
                table: "DeveloperInProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EmployeeUserId",
                table: "Project",
                column: "EmployeeUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperInProject");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
