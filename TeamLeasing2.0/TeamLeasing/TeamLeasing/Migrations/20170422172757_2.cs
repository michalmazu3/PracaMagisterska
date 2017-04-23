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
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Employees_EmployeeId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Offers",
                newName: "EmployeeUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_EmployeeId",
                table: "Offers",
                newName: "IX_Offers_EmployeeUserId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Jobs",
                newName: "EmployeeUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_EmployeeId",
                table: "Jobs",
                newName: "IX_Jobs_EmployeeUserId");

            migrationBuilder.CreateTable(
                name: "EmployeeUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Province = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUsers_UserId",
                table: "EmployeeUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs",
                column: "EmployeeUserId",
                principalTable: "EmployeeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_EmployeeUsers_EmployeeUserId",
                table: "Offers",
                column: "EmployeeUserId",
                principalTable: "EmployeeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmployeeUsers_EmployeeUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_EmployeeUsers_EmployeeUserId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "EmployeeUsers");

            migrationBuilder.RenameColumn(
                name: "EmployeeUserId",
                table: "Offers",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_EmployeeUserId",
                table: "Offers",
                newName: "IX_Offers_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "EmployeeUserId",
                table: "Jobs",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_EmployeeUserId",
                table: "Jobs",
                newName: "IX_Jobs_EmployeeId");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Employees_EmployeeId",
                table: "Offers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
