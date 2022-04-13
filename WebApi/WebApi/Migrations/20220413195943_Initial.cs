using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    priorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.priorityId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    teamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teamName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    teamDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.teamId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    teamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Teams_teamId",
                        column: x => x.teamId,
                        principalTable: "Teams",
                        principalColumn: "teamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ticketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    priorityId = table.Column<int>(type: "int", nullable: false),
                    assignedEmployeeemployeeId = table.Column<int>(type: "int", nullable: true),
                    reporteremployeeId = table.Column<int>(type: "int", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Employees_assignedEmployeeemployeeId",
                        column: x => x.assignedEmployeeemployeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_Ticket_Employees_reporteremployeeId",
                        column: x => x.reporteremployeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_Ticket_Priority_priorityId",
                        column: x => x.priorityId,
                        principalTable: "Priority",
                        principalColumn: "priorityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_teamId",
                table: "Employees",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_assignedEmployeeemployeeId",
                table: "Ticket",
                column: "assignedEmployeeemployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_priorityId",
                table: "Ticket",
                column: "priorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_reporteremployeeId",
                table: "Ticket",
                column: "reporteremployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
